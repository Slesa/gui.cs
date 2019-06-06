cmake_minimum_required(VERSION 3.14)
project(win)

include_directories(${CMAKE_SOURCE_DIR}/../../oldscool)
link_directories(${CMAKE_SOURCE_DIR}/../../lib)

set(CMAKE_CXX_STANDARD 14)

set(CURSES_NEED_NCURSES TRUE)
set(CURSES_NEED_WIDE TRUE)

include(FindCurses)

include_directories(${CURSES_INCLUDE_DIRS})

add_executable(win main.cpp)
target_link_libraries(win oldscool ${CURSES_LIBRARIES}  )