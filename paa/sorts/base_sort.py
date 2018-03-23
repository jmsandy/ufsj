#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""Base class for sorting algorithms.

Abstract class responsible for assisting ordering algorithms.

"""

import random
from datetime import datetime
from abc import ABCMeta, abstractmethod

__author__ = "José Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "José Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


class BaseSort(object):
    """
    Abstract class responsible for assisting ordering algorithms.

    Args:
        name (str): name of the sorting algorithm.
    """

    __metaclass__ = ABCMeta
    _count_change = 0
    _count_compare = 0
    _elements = 0
    __initial_time = None
    __final_time = None
    __name = None

    def __init__(self, name):
        self.__name = name

    @property
    def count_compare(self):
        """int: comparison counter."""
        return self._count_compare

    @property
    def count_change(self):
        """int: swap counter."""
        return self._count_change

    def __start_test(self, collection):
        """Performs the initialization of counters.

        Args:
            collection (list): collection for sorting.

        """
        self._count_change = 0
        self._count_compare = 0
        self._elements = len(collection)

        self.__initial_time = datetime.now()

    def __end_test(self):
        """Ends the sorting test counters."""
        self.__final_time = datetime.now()

    def __diff_time(self):
        """Calculates the time spent during sorting."""
        return (self.__final_time - self.__initial_time).total_seconds()

    def __generate_collection(self, size, asc, simple):
        """Creates a collection based on received arguments.

        Args:
            size (int): size of the collection.
            asc (bool): order of creation.
            simple (bool): indicates whether it will be a simple (only key) or complex collection.

        Returns:
            collection created.
        """
        collection = []
        record = "PAAST" * 10

        for i in range((size * -1, 0)[asc], (0, size)[asc]):
            if simple:
                collection.append((i, i * -1)[i < 0])

            else:
                collection.append([(i, i * -1)[i < 0], record])

        return collection

    def trace(self):
        """Prints the tracking information in the console."""
        print "{} -> {} elements -> Changes -> {}"\
            .format(self.__name, self._elements, self.count_change, self.__diff_time())
        print "{} -> {} elements -> Comparisons -> {}"\
            .format(self.__name, self._elements, self.count_compare, self.__diff_time())
        print "{} -> {} elements -> Spend Time -> {:.4f}"\
            .format(self.__name, self._elements, self.__diff_time())

    def test(self, size):
        """Apply sorting tests.

        Performs the sorting tests with collections: increasing, decreasing, and random.
        In addition, tests are performed with simple and complex collections.

        Args:
            size (int): size of the collection.

        """
        print "{} -> Generating a simple collection of {} elements in ascending order".format(self.__name, size)
        collection = self.__generate_collection(size, True, True)

        # print collection
        self.__start_test(collection)
        self.sort(collection)
        self.__end_test()
        # print collection
        self.trace()

        print "{} -> Generating a simple collection of {} elements in descending order".format(self.__name, size)
        collection = self.__generate_collection(size, False, True)
        # print collection
        self.__start_test(collection)
        self.sort(collection)
        self.__end_test()
        # print collection
        self.trace()

        print "{} -> Generating a simple collection of {} elements randomly".format(self.__name, size)
        random.shuffle(collection)
        # print collection
        self.__start_test(collection)
        self.sort(collection)
        self.__end_test()
        # print collection
        self.trace()

        print "{} -> Generating a complex collection of {} elements in ascending order".format(self.__name, size)
        collection = self.__generate_collection(size, True, False)

        # print collection
        self.__start_test(collection)
        self.sort(collection)
        self.__end_test()
        # print collection
        self.trace()

        print "{} -> Generating a complex collection of {} elements in descending order".format(self.__name, size)
        collection = self.__generate_collection(size, False, False)
        # print collection
        self.__start_test(collection)
        self.sort(collection)
        self.__end_test()
        # print collection
        self.trace()

        print "{} -> Generating a complex collection of {} elements randomly".format(self.__name, size)
        random.shuffle(collection)
        # print collection
        self.__start_test(collection)
        self.sort(collection)
        self.__end_test()
        # print collection
        self.trace()

    @abstractmethod
    def sort(self, collection):
        """Performs collection ordering."""
        return None

