#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""Sort algorithm QuickSort.
"""

import random
from sorts.base_sort import BaseSort

__author__ = "José Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "José Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


class QuickSort(BaseSort):
    """Sort algorithm QuickSort."""

    def __init__(self):
        BaseSort.__init__(self, "QuickSort")

    def __quick_sort(self, collection, start, end):
        """Performs collection ordering based on received parts.

        Args:
            collection (list): collection to order.
            start (int): starting position.
            end (int): end position.
        """
        if start < end:
            pivot = random.randint(start, end)
            temp = collection[end]
            collection[end] = collection[pivot]
            collection[pivot] = temp

            self._count_change += 2

            p = self.__partition(collection, start, end)
            self.__quick_sort(collection, start, p - 1)
            self.__quick_sort(collection, p + 1, end)

    def __partition(self, collection, start, end):
        """Performs collection ordering based on received parts.

        Args:
            collection (list): collection to order.
            start (int): starting position.
            end (int): end position.
        """
        pivot = random.randint(start, end)
        temp = collection[end]
        collection[end] = collection[pivot]
        collection[pivot] = temp
        new_pivot_index = start - 1

        for index in xrange(start, end):
            self._count_compare += 1

            if collection[index] < collection[end]:  # check if current val is less than pivot value
                new_pivot_index += 1
                temp = collection[new_pivot_index]
                collection[new_pivot_index] = collection[index]
                collection[index] = temp

                self._count_change += 2

        temp = collection[new_pivot_index + 1]
        collection[new_pivot_index + 1] = collection[end]
        collection[end] = temp

        self._count_change += 2

        return new_pivot_index + 1

    def sort(self, collection):
        """Performs collection ordering.

        Args:
            collection (list): collection to order.

        Returns:
            collection orderly.
        """
        return self.__quick_sort(collection, 0, len(collection) - 1)

