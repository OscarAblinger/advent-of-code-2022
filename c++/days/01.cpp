#include "01.h"

#include <iostream>

namespace aoc {
  namespace day01 {
	using std::string;
	using std::vector;

	string run(part_t part, vector<string> input) {

	  std::cout << "input:" << std::endl;
	  for (auto line : input)
		std::cout << line << std::endl;

	  return input[0];
	}
  }
}