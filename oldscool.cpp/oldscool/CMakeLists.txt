cmake_minimum_required(VERSION 3.14)
project(oldscool)

set(CMAKE_CXX_STANDARD 14)

add_library(oldscool library.cpp library.h Vio.cpp Vio.h Win.cpp Win.h)