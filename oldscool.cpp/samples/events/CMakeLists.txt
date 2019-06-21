cmake_minimum_required(VERSION 3.14)
project(events)

include_directories(${CMAKE_SOURCE_DIR}/../../oldscool)
LINK_DIRECTORIES(/usr/lib64)
link_directories(${CMAKE_SOURCE_DIR}/../../lib)

set(CMAKE_CXX_STANDARD 14)

set(CURSES_NEED_WIDE TRUE)
set(CURSES_NEED_NCURSES TRUE)
include(FindCurses)
find_package(Curses REQUIRED CURSES_NEED_WIDE)

include_directories(${CURSES_INCLUDE_DIRS})

add_executable(events main.cpp)
target_link_libraries(events oldscool ncursesw formw ) # ${CURSES_LIBRARIES}  )
