#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""Sort algorithm CocktailSort.
"""

from sorts.base_sort import BaseSort

__author__ = "José Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "José Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"

class CocktailSort(BaseSort):
    """Sort algorithm CocktailSort."""

    def __init__(self):
        BaseSort.__init__(self, "CocktailSort")

    def sort(self, collection):
        """Performs collection ordering.

        Args:
            collection (list): collection to order.

        Returns:
            collection orderly.
        """
        for i in range(len(collection) - 1, 0, -1):
            swapped = False

            for j in range(i, 0, -1):
                self._count_compare += 1

                if collection[j] < collection[j - 1]:
                    collection[j], collection[j - 1] = collection[j - 1], collection[j]
                    swapped = True
                    self._count_change += 2

            for j in range(i):
                self._count_compare += 1

                if collection[j] > collection[j + 1]:
                    collection[j], collection[j + 1] = collection[j + 1], collection[j]
                    swapped = True
                    self._count_change += 2

            if not swapped:
                return collection
