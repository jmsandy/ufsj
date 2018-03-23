#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""Tests for sorting algorithms.

Responsible for triggering sorting algorithms: SelectionSort, HeapSort, QuickSort, ShellSort,
MergeSort and CooktailSort,  measuring the amount of exchanges, number of comparisons and
time spent for collections with 25, 500, 10000, 200000, and 1000000 elements.

Example:
    $ python sort_test.py

"""

from sorts.heap_sort import HeapSort
from sorts.merge_sort import MergeSort
from sorts.quick_sort import QuickSort
from sorts.shell_sort import ShellSort
from sorts.cooktail_sort import CocktailSort
from sorts.insertion_sort import InsertionSort

__author__ = "José Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "José Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


def sort_algorithm_test(sort_algorithm, max_repetition, max_size):
    """Performs the call for sorting tests for the received algorithm.

    Args:
        param sort_algorithm (BaseSort): sorting algorithm.
        param max_repetition (int): number of repetitions to be performed.
        param max_size (int): maximum size of the collection to be sorted.

    """
    for size in [25, 500, 10000, 200000, 1000000]:
        if size < max_size:
            print "{} {} Elements {}".format("*" * 20, size, "*" * 20)

            for repetition in range(0, max_repetition):
                print "Repetion {}".format(repetition + 1)
                sort_algorithm.test(size)

        else:
            print "This algorithm will not be executed for {} elements".format(size)


if __name__ == '__main__':
    heap_sort = HeapSort()
    merge_sort = MergeSort()
    quick_sort = QuickSort()
    shell_sort = ShellSort()
    cook_tail_sort = CocktailSort()
    insertion_sort = InsertionSort()

    print "InsertionSort"
    sort_algorithm_test(insertion_sort, 1, 200001)

    print "ShellSort"
    sort_algorithm_test(shell_sort, 10, 1000000)

    print "QuickSort"
    sort_algorithm_test(quick_sort, 10, 1000000)

    print "HeapSort"
    sort_algorithm_test(heap_sort, 10, 1000000)

    print "MergeSort"
    sort_algorithm_test(merge_sort, 10, 1000000)

    print "CookTailSort"
    sort_algorithm_test(cook_tail_sort, 2, 200001)
