#include <ranges>
#include <sstream>

#include "02.h"

namespace aoc {
  namespace day02 {
	using std::string;
	using std::to_string;
	using std::vector;
	using std::tuple;
	using std::make_tuple;
	using std::stringstream;
	using std::views::transform;

	vector<char> xyz_to_abc { 'A', 'B', 'C' };
	vector<char> abc_to_xyz { 'X', 'Y', 'Z' };

	int xyz_to_i(char mine) {
	  return mine - 'X';
	}

	int abc_to_i(char opponent) {
	  return opponent - 'A';
	}

	tuple<char, char> split(string line) {
	  stringstream ss(line);
	  char opponent;
	  char mine;
	  ss >> opponent >> mine;
	  return make_tuple(opponent, mine);
	}

	string part_a(vector<string> input) {
	  int score = 0;
	  for (auto [opponent, mine] : transform(input, split))
	  {
		int mineInt = xyz_to_i(mine);
		int oppInt = abc_to_i(opponent);

		score += mineInt + 1; // shape I selected
		if (mine == abc_to_xyz[oppInt]) // draw
		{
		  score += 3;
		}
		else if (mine == abc_to_xyz[(oppInt + 1) % 3]) // win
		{
		  score += 6;
		}
	  }

	  return to_string(score);
	}

	string part_b(vector<string> input) {
	  int score = 0;
	  for (auto [opponent, mine] : transform(input, split))
	  {
		int mineInt = xyz_to_i(mine);
		int oppInt = abc_to_i(opponent);

		score += mineInt * 3; // whether I won
		
		switch (mine) {
		case 'X': // lose
		  score += ((oppInt + 2) % 3) + 1;
		  break;
		case 'Y': // draw
		  score += oppInt + 1;
		  break;
		case 'Z': // win
		  score += ((oppInt + 1) % 3) + 1;
		  break;
		}
	  }

	  return to_string(score);
	}

	string run(part_t part, vector<string> input) {
	  switch (part) {
	  case partA:
		return part_a(input);
		break;
	  case partB:
		return part_b(input);
		break;
	  default:
		exit(1);
	  }
	}
  }
}