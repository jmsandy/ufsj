from crawler.crawler import Crawler

DOMAIN = None
NUM_WORKERS = 1


def main():
    crawler = Crawler(num_workers=NUM_WORKERS, domain=DOMAIN)
    crawler.run()


if __name__ == '__main__':
    main()

