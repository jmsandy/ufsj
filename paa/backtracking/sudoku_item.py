#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""Item sudoku with coordinates and value.
"""

__author__ = "José Mauro da Silva Sandy"
__copyright__ = "Copyleft 2018"
__license__ = "GPL"
__version__ = "0.1"
__maintainer__ = "José Mauro da Silva Sandy"
__email__ = "jmsandy@gmail.com"
__status__ = "Test"


class SudokuItem(object):
    """Item sudoku with coordinates and value.

    Args:
        line (int): sudoku line.
        column (int): sudoku column.
        value (int): sudoku value.
    """

    __value = 0
    __line = None
    __column = None

    def __init__(self, line, column, value=0):
        self.__line = line
        self.__column = column
        self.__value = value

    @property
    def value(self):
        return self.__value

    @value.setter
    def value(self, value):
        self.__value = value

    @property
    def line(self):
        return self.__line

    @property
    def column(self):
        return self.__column

    def __repr__(self):
        return "Line: {} - Column: {} - Value: {}".format(self.__line, self.__column, self.value)

