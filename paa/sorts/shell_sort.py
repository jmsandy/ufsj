#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""Algoritmo de ordenação ShellSort.
"""

from sorts.base_sort import BaseSort

__author__ = "José Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "José Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


class ShellSort(BaseSort):
    """Algoritmo de ordenação ShellSort."""

    def __init__(self):
        BaseSort.__init__(self, "ShellSort")

    def sort(self, collection):
        """Performs collection ordering.

        Args:
            collection (list): collection to order.

        Returns:
            collection orderly.
        """
        # Marcin Ciura's gap sequence
        gaps = [701, 301, 132, 57, 23, 10, 4, 1]

        for gap in gaps:
            i = gap
            while i < len(collection):
                j = i
                temp = collection[i]
                current_compares = 0

                while j >= gap and collection[j - gap] > temp:
                    collection[j] = collection[j - gap]
                    j -= gap

                    current_compares += 1
                    self._count_change += 1

                i += 1
                collection[j] = temp
                self._count_change += 1
                self._count_compare += (current_compares, 1)[current_compares == 0]

        return collection
