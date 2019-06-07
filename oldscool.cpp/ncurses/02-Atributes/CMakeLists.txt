cmake_minimum_required(VERSION 3.14)
project(02_Atributes)

set(CMAKE_CXX_STANDARD 14)

set(CURSES_NEED_NCURSES TRUE)
set(CURSES_NEED_WIDE TRUE)

include(FindCurses)

include_directories(${CURSES_INCLUDE_DIRS})

add_executable(02_Atributes main.cpp)
target_link_libraries(02_Atributes ${CURSES_LIBRARIES}  )