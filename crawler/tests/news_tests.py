import unittest
from models.news import News

__author__ = "Jose Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "Jose Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


class NewsTests(unittest.TestCase):

    def test_add(self):
        News("globo.com", "url", "conteudo").save()

        self.assertIsNone(None, "Failed to adds news")


