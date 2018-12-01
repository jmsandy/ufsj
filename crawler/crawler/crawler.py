import re
import logging
import asyncio
import mimetypes
import concurrent.futures
from models.news import News
from bs4 import BeautifulSoup
from urllib.request import urlopen, Request
from urllib.parse import urlparse, urlunparse

__author__ = "Jose Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "Jose Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


class IllegalArgumentError(ValueError):
    pass


class Crawler(object):
    __LINK_REGEX = re.compile(b'<a [^>]*href=[\'|"](.*?)[\'"][^>]*?>')

    __domain = None
    __num_workers = None
    __urls_to_crawl = set([])
    __crawled_or_crawling = set([])

    __scheme = None
    __target_domain = None

    def __init__(self, num_workers=1, domain=None):
        logging.basicConfig(level=logging.DEBUG)

        if num_workers <= 0:
            raise IllegalArgumentError("Number or workers must be positive")

        self.__domain = domain
        self.__num_crawled = 0
        self.__num_workers = num_workers
        self.__urls_to_crawl = {Crawler.__clean_link(domain)}

        try:
            url_parsed = urlparse(domain)
            self.__scheme = url_parsed.scheme
            self.__target_domain = url_parsed.netloc

        except:
            logging.error("Invalid domain")
            raise IllegalArgumentError("Invalid domain")

    @staticmethod
    def __clean_link(link):
        url = urlparse(link)
        url_part = list(url)
        url_part[2] = url_part[2].replace("./", "/")
        url_part[2] = url_part[2].replace("//", "/")
        return urlunparse(url_part)

    def run(self):
        logging.info("Start the crawling process")

        if self.__num_workers == 1:
            while len(self.__urls_to_crawl) != 0:
                current_url = self.__urls_to_crawl.pop()
                self.__crawled_or_crawling.add(current_url)
                self.__crawl(current_url)
        else:
            event_loop = asyncio.get_event_loop()
            try:
                while len(self.__urls_to_crawl) != 0:
                    executor = concurrent.futures.ThreadPoolExecutor(max_workers=self.__num_workers)
                    event_loop.run_until_complete(self.__crawl_all_pending_urls(executor))
            finally:
                event_loop.close()

        logging.info("Crawling has reached end of all found links")


    async def __crawl_all_pending_urls(self, executor):
        event_loop = asyncio.get_event_loop()

        crawl_tasks = []
        for url in self.__urls_to_crawl:
            self.__crawled_or_crawling.add(url)
            task = event_loop.run_in_executor(executor, self.__crawl, url)
            crawl_tasks.append(task)

        self.__urls_to_crawl = set()

        logging.debug('waiting on all crawl tasks to complete')

        await asyncio.wait(crawl_tasks)
        logging.debug('all crawl tasks have completed nicely')

        return

    @staticmethod
    def __convert_html_to_text(response):
        soup = BeautifulSoup(response)

        # kill all script and style elements
        for script in soup(["script", "style"]):
            script.decompose()  # rip it out

        # get text
        text = soup.get_text()

        # break into lines and remove leading and trailing space on each
        lines = (line.strip() for line in text.splitlines())
        # break multi-headlines into a line each
        chunks = (phrase.strip() for line in lines for phrase in line.split("  "))
        # drop blank lines
        text = '\n'.join(chunk for chunk in chunks if chunk)

        return text

    @staticmethod
    def __get_response(current_url):
        request = Request(current_url, headers={
            "User-Agent": "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:63.0) Gecko/20100101 Firefox/63.0"})

        try:
            response = urlopen(request)

            if response is not None:
                try:
                    content = response.read()
                    response.close()

                    return content
                except Exception as e:
                    logging.debug("{1} ===> {0}".format(e, current_url))
                    return None

        except Exception as e:
            logging.debug("{1} ==> {0}".format(e, current_url))

        return None

    def __crawl(self, current_url):
        url = urlparse(current_url)
        logging.info("Crawling #{}: {}".format(self.__num_crawled, url.geturl()))
        self.__num_crawled += 1

        # Ignore ressources listed in the not_parseable_resources
        # Its avoid dowloading file like pdf… etc
        content = Crawler.__get_response(current_url)

        if content is None:
            return None

        News(domain=self.__domain, url=current_url, content=Crawler.__convert_html_to_text(content)).save()

        # Found links
        links = self.__LINK_REGEX.findall(content)
        for link in links:
            link = link.decode("utf-8", errors="ignore")
            link = self.__clean_link(link)
            logging.debug("Found : {0}".format(link))

            if link.startswith('/'):
                link = url.scheme + '://' + url[1] + link

            elif link.startswith('#'):
                link = url.scheme + '://' + url[1] + url[2] + link

            elif link.startswith(("mailto", "tel")):
                continue

            elif not link.startswith(('http', "https")):
                link = url.scheme + '://' + url[1] + '/' + link

            if "#" in link:
                link = link[:link.index('#')]

            # Parse the url to get domain and file extension
            parsed_link = urlparse(link)
            domain_link = parsed_link.netloc

            if link in self.__crawled_or_crawling:
                continue

            elif link in self.__urls_to_crawl:
                continue

            elif domain_link != self.__target_domain:
                continue

            elif parsed_link.path in ["", "/"]:
                continue

            elif "javascript" in link:
                continue

            elif self.__is_image(parsed_link.path):
                continue

            elif parsed_link.path.startswith("data:"):
                continue

            self.__urls_to_crawl.add(link)

    @staticmethod
    def __is_image(path):
        mt, me = mimetypes.guess_type(path)
        return mt is not None and mt.startswith("image/")