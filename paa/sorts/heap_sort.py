#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""Algoritmo de ordenação HeapSort.
"""

from sorts.base_sort import BaseSort
__author__ = "José Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "José Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


class HeapSort(BaseSort):
    """Algoritmo de ordenação HeapSort."""

    def __init__(self):
        BaseSort.__init__(self, "HeapSort")

    def heapify(self, collection, index, heap_size):
        """Performs the ordering of the received part.

        Args:
            collection (list): collection to order.
            index (int): initial index of the heap.
            heap_size (int): size of the heap.

        Returns:
            collection orderly.
        """
        largest = index
        left_index = 2 * index + 1
        right_index = 2 * index + 2

        if left_index < heap_size and collection[left_index] > collection[largest]:
            largest = left_index
            self._count_compare += 1

        if right_index < heap_size and collection[right_index] > collection[largest]:
            largest = right_index
            self._count_compare += 1

        if largest != index:
            collection[largest], collection[index] = collection[index], collection[largest]
            self._count_change += 2
            self.heapify(collection, largest, heap_size)

    def sort(self, collection):
        """Performs collection ordering.

        Args:
            collection (list): collection to order.

        Returns:
            collection orderly.
        """
        n = len(collection)
        for i in range(n // 2 - 1, -1, -1):
            self.heapify(collection, i, n)

        for i in range(n - 1, 0, -1):
            collection[0], collection[i] = collection[i], collection[0]
            self.heapify(collection, 0, i)

        return collection

