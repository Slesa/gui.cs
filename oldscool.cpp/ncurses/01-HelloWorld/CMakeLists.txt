cmake_minimum_required(VERSION 3.14)
project(01_HelloWorld)

set(CMAKE_CXX_STANDARD 14)

set(CURSES_NEED_NCURSES TRUE)
set(CURSES_NEED_WIDE TRUE)

include(FindCurses)
include_directories(${CURSES_INCLUDE_DIRS})

add_executable(01_HelloWorld main.cpp)
target_link_libraries(01_HelloWorld ${CURSES_LIBRARIES}  )