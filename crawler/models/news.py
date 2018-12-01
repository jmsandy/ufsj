#!/usr/bin/env python
# -*- coding: utf-8 -*-
""" News Model.
"""
from pymongo.write_concern import WriteConcern
from pymodm import MongoModel, fields, connect

__author__ = "Jose Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "Jose Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


# Establish a connection to the database.
connect('mongodb://localhost:27017/crawler_db')


class News(MongoModel):

    __url = fields.CharField(mongo_name="url", required=True, blank=False)
    __domain = fields.CharField(mongo_name="domain", required=True, blank=False)
    __content = fields.CharField(mongo_name="content", required=True, blank=False)

    class Meta:
        final = True
        write_concern = WriteConcern(j=True)

    def __init__(self, domain=None, url=None, content=None):
        MongoModel.__init__(self, domain, url, content)
        self.__url = url
        self.__domain = domain
        self.__content = content

    @property
    def url(self):
        return self.__url

    @url.setter
    def url(self, url):
        self.__url = url

    @property
    def domain(self):
        return self.__domain

    @domain.setter
    def domain(self, domain):
        self.__domain = domain

    @property
    def content(self):
        return self.__content

    @content.setter
    def domain(self, content):
        self.__content = content

