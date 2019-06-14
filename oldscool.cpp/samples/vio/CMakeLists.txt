cmake_minimum_required(VERSION 3.14)
project(vio)

include_directories(${CMAKE_SOURCE_DIR}/../../oldscool)
LINK_DIRECTORIES(/usr/lib64)
link_directories(${CMAKE_SOURCE_DIR}/../../lib)

set(CMAKE_CXX_STANDARD 14)

set(CURSES_NEED_NCURSES TRUE)
set(CURSES_NEED_WIDE TRUE)
include(FindCurses)
find_package(Curses REQUIRED CURSES_NEED_WIDE)

message(STATUS "CURSES_LIBRARIES: ${CURSES_LIBRARIES}")

# find_package(Curses REQUIRED)
include_directories(${CURSES_INCLUDE_DIRS})
# target_link_libraries( ${CURSES_LIBRARIES} )

add_executable(vio main.cpp)
target_link_libraries(vio oldscool ncursesw formw ) # ${CURSES_LIBRARIES}  )
#target_link_libraries(vio oldscool ${CURSES_LIBRARIES}  )
