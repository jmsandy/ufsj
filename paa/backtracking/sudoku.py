#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""Sudoku backtracking algorithm.
"""

import math
from sudoku_item import SudokuItem

__author__ = "José Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "José Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


class Sudoku(object):
    """Sudoku backtracking algorithm."""

    __range = xrange(0, 9)
    __items = []

    def __init__(self):
        self.__generate()

    def __generate(self):
        """Generates the sudoku array for resolution."""
        for line in self.__range:
            for column in self.__range:
                self.__items.append(SudokuItem(line, column))

    def __is_valid_line(self, value, line):
        """Checks if the line is valid.

        :param value: value to check.
        :param line: line to check.
        :return: status of the verification.
        """
        return next((item for item in self.__items if item.line == line and item.value == value), None) is None

    def __is_valid_column(self, value, column):
        """Checks if the column is valid.

        :param value: value to check.
        :param column: column to check.
        :return: status of the verification.
        """
        return next((item for item in self.__items if item.column == column and item.value == value), None) is None

    def __is_valid_grid(self, value, line, column):
        """Checks if the internal grid is valid.

        :param value: value to check.
        :param line: line to check.
        :param column: column to check.
        :return: status of the verification.
        """
        for current_line in xrange(int(math.floor((line / 3)) * 3), int(math.floor(line / 3) * 3 + 3)):
            for current_column in xrange(int(math.floor((column / 3)) * 3), int(math.floor(column / 3) * 3 + 3)):
                if next((item for item in self.__items if item.line == current_line and
                                                          item.column == current_column and
                                                          item.value == value), None) is not None:
                    return False

        return True

    def __is_valid(self, value, line, column):
        """Checks if the value can be entered in the received row and column.

        :param value: value to check.
        :param line: line to check.
        :param column: column to check.
        :return: status of the verification.
        """
        return self.__is_valid_line(value, line) and self.__is_valid_column(value, column) and \
               self.__is_valid_grid(value, line, column)

    def __get_next_empty_item(self):
        """Gets the next item with no value."""
        for line in self.__range:
            for column in self.__range:
                item = self.get_item(line, column)

                if item.value == 0:
                    return item

        return None

    def __solve(self):
        """Solve the sudoku."""
        item = self.__get_next_empty_item()

        if item is None:
            return True

        for value in xrange(1, 10):
            if self.__is_valid(value, item.line, item.column):
                item.value = value

                if self.__solve():
                    return True

                item.value = 0

        return False

    def get_item(self, line, column):
        """Gets the item based on the received coordinates.

        :param line: line to check.
        :param column: column to check.
        :return: item obtained.
        """
        if line in self.__range and column in self.__range:
            return next((item for item in self.__items if item.line == line and item.column == column), None)

        else:
            raise Exception("Invalid line or column")

    def set_item(self, line, column, value):
        """Sets the value based on the coordinates received.

        :param line: line to set.
        :param column: column to set.
        :param value: value to set.
        :return: none
        """
        item = self.get_item(line, column)
        item.value = value

        return None

    def solve(self):
        """Solve the sudoku."""
        self.__solve()

    def print_items(self):
        """Print all items."""
        print "{:^37}".format("SUDOKU")

        for line in self.__range:
            print "+---+---+---+---+---+---+---+---+---+"
            print "|",
            for column in self.__range:
                print str(self.get_item(line, column).value) + " |",

            print ""

        print "+---+---+---+---+---+---+---+---+---+"
