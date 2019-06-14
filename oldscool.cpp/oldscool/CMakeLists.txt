cmake_minimum_required(VERSION 3.14)
project(oldscool)

set(CMAKE_CXX_STANDARD 14)
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/../../lib)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/../../lib)

set(CURSES_NEED_NCURSES TRUE)
set(CURSES_NEED_WIDE TRUE)
include(FindCurses)
find_package(Curses REQUIRED CURSES_NEED_WIDE)

include_directories(${CURSES_INCLUDE_DIRS})
# target_link_libraries( ${CURSES_LIBRARIES} )
message(STATUS "CURSES_LIBRARIES: ${CURSES_INCLUDE_DIRS}")

add_library(oldscool library.cpp library.h Vio.cpp Vio.h Win.cpp Win.h Palette.cpp Palette.h)