cmake_minimum_required(VERSION 3.14)
project(02c_acs)

LINK_DIRECTORIES(/usr/lib64)

set(CMAKE_CXX_STANDARD 14)
set(CURSES_NEED_NCURSES TRUE)
set(CURSES_NEED_WIDE TRUE)
include(FindCurses)
find_package(Curses REQUIRED CURSES_NEED_WIDE)

include_directories(${CURSES_INCLUDE_DIRS})

add_executable(02c_acs main.cpp)
target_link_libraries(02c_acs ncursesw formw ) # ${CURSES_LIBRARIES}  )
