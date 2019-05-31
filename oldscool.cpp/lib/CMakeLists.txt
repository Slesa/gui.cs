cmake_minimum_required(VERSION 3.14)
project(lib)

set(CMAKE_CXX_STANDARD 14)

add_library(lib library.cpp library.h TColors.cpp TColors.h)