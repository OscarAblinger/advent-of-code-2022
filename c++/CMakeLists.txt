# CMakeList.txt : CMake project for aoc2022, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)

# Enable Hot Reload for MSVC compilers if supported.
if (POLICY CMP0141)
  cmake_policy(SET CMP0141 NEW)
  set(CMAKE_MSVC_DEBUG_INFORMATION_FORMAT "$<IF:$<AND:$<C_COMPILER_ID:MSVC>,$<CXX_COMPILER_ID:MSVC>>,$<$<CONFIG:Debug,RelWithDebInfo>:EditAndContinue>,$<$<CONFIG:Debug,RelWithDebInfo>:ProgramDatabase>>")
endif()

project ("aoc2022")

# Add source to this project's executable.
add_executable (aoc2022 "aoc2022.cpp" "aoc2022.h" "days/01.h" "days/01.cpp" "days.h" "days/02.h" "days/02.cpp" "days/03.h" "days/03.cpp" "days/05.h" "days/05.cpp")

if (CMAKE_VERSION VERSION_GREATER 3.12)
  set_property(TARGET aoc2022 PROPERTY CXX_STANDARD 20)
endif()

# TODO: Add tests and install targets if needed.
