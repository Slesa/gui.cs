cmake_minimum_required(VERSION 3.14)
project(03_Menu)

set(CMAKE_CXX_STANDARD 14)

set(CURSES_NEED_NCURSES TRUE)
set(CURSES_NEED_WIDE TRUE)
find_library(MENU_LIBRARY menu)

include(FindCurses)
include_directories(${CURSES_INCLUDE_DIRS})

add_executable(03_Menu main.cpp)
target_link_libraries(03_Menu ${CURSES_LIBRARIES} ${MENU_LIBRARY} )