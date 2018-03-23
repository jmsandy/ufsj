#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""Algoritmo de ordenação InsertionSort.
"""

from sorts.base_sort import BaseSort

__author__ = "José Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "José Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


class InsertionSort(BaseSort):
    """Algoritmo de ordenação InsertionSort."""

    def __init__(self):
        BaseSort.__init__(self, "InsertionSort")

    def sort(self, collection):
        """Performs collection ordering.

        Args:
            collection (list): collection to order.

        Returns:
            collection orderly.
        """
        for index in range(1, len(collection)):
            current_compares = 0

            while 0 < index and collection[index] < collection[index - 1]:
                collection[index], collection[index - 1] = collection[index - 1], collection[index]
                index -= 1

                current_compares += 1
                self._count_change += 2

            self._count_compare += (current_compares, 1)[current_compares == 0]

        return collection

