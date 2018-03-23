#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""Sort algorithm MergeSort.
"""

from sorts.base_sort import BaseSort

__author__ = "José Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "José Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


class MergeSort(BaseSort):
    """Sort algorithm MergeSort."""

    def __init__(self):
        BaseSort.__init__(self, "MergeSort")

    def sort(self, collection):
        """Performs collection ordering.

        Args:
            collection (list): collection to order.

        Returns:
            collection orderly.
        """
        length = len(collection)
        if length > 1:
            midpoint = length // 2
            left_half = self.sort(collection[:midpoint])
            right_half = self.sort(collection[midpoint:])
            i = 0
            j = 0
            k = 0
            left_length = len(left_half)
            right_length = len(right_half)

            while i < left_length and j < right_length:
                self._count_change += 1
                self._count_compare += 1

                if left_half[i] < right_half[j]:
                    collection[k] = left_half[i]
                    i += 1

                else:
                    collection[k] = right_half[j]
                    j += 1

                k += 1

            while i < left_length:
                collection[k] = left_half[i]
                i += 1
                k += 1

                self._count_change += 1
                self._count_compare += 1

            while j < right_length:
                collection[k] = right_half[j]
                j += 1
                k += 1

                self._count_change += 1
                self._count_compare += 1

        return collection

