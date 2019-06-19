cmake_minimum_required(VERSION 3.14)
project(04_forms)

LINK_DIRECTORIES(/usr/lib64)

set(CMAKE_CXX_STANDARD 14)

set(CURSES_NEED_WIDE TRUE)
set(CURSES_NEED_NCURSES TRUE)
include(FindCurses)
find_package(Curses REQUIRED CURSES_NEED_WIDE)

include_directories(${CURSES_INCLUDE_DIRS})

add_executable(04_forms main.cpp)
target_link_libraries(04_forms ncursesw formw ) # ${CURSES_LIBRARIES}  )
