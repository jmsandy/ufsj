#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""Testing for sudoku backtracking.

Example:
    $ python sudoku_test.py

"""

from backtracking.sudoku import Sudoku

__author__ = "José Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "José Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


def main():
    """Set the test for sudoku resolution.

        :return: None
    """
    instance = Sudoku()

    # Line 1
    instance.set_item(0, 0, 4)
    instance.set_item(0, 1, 8)
    instance.set_item(0, 3, 6)
    instance.set_item(0, 4, 5)
    instance.set_item(0, 7, 7)

    # Line 2
    instance.set_item(1, 1, 6)
    instance.set_item(1, 3, 9)
    instance.set_item(1, 6, 8)
    instance.set_item(1, 7, 5)

    # Line 3
    instance.set_item(2, 1, 1)
    instance.set_item(2, 7, 6)
    instance.set_item(2, 8, 3)

    # Line 4
    instance.set_item(3, 0, 1)
    instance.set_item(3, 5, 9)

    # Line 5
    instance.set_item(4, 4, 6)

    # Line 6
    instance.set_item(5, 3, 8)
    instance.set_item(5, 8, 6)

    # Line 7
    instance.set_item(6, 0, 5)
    instance.set_item(6, 1, 4)
    instance.set_item(6, 7, 3)

    # Line 8
    instance.set_item(7, 1, 9)
    instance.set_item(7, 2, 8)
    instance.set_item(7, 5, 6)
    instance.set_item(7, 7, 1)

    # Line 9
    instance.set_item(8, 1, 7)
    instance.set_item(8, 4, 3)
    instance.set_item(8, 5, 2)
    instance.set_item(8, 7, 4)
    instance.set_item(8, 8, 8)

    print "Before Solve"
    instance.print_items()
    instance.solve()

    print "After Solve"
    instance.print_items()


if __name__ == '__main__':
    main()

