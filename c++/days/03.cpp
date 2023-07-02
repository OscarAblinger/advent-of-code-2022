#include <bitset>
#include <algorithm>
#include <numeric>
#include <ranges>
#include <sstream>

#include "03.h"

namespace aoc {
  namespace day03 {
	using std::string;
	using std::to_string;
	using std::vector;
	using std::bitset;
	using std::tuple;
	using std::make_tuple;
	using std::views::transform;
	using std::views::common;
	using std::accumulate;

	tuple<string, string> split_in_half(string line) {
	  auto middle = line.length() / 2;
	  return make_tuple(line.substr(0, middle), line.substr(middle));
	}

	bitset<'z' + 1> to_bitset(string str) {
	  bitset<'z' + 1> chCount; // unnecessarily large, but simpler than having to transform the characters to their values first
	  for (char ch : str) {
		chCount.set(ch);
	  }
	  return chCount;
	}

	char odd_one_out(tuple<string, string> pair) {
	  auto& [left, right] = pair;

	  auto chCount = to_bitset(left);

	  for (char ch : right) {
		if (chCount.test(ch)) {
		  return ch;
		}
	  }

	  printf("Could not find any common characters between <%s> and <%s>\n", left.c_str(), right.c_str());
	  exit(1);
	}

	int char_to_value(char ch) {
	  if (ch <= 'Z') {
		return ch - 'A' + 27;
	  }
	  else
	  {
		return ch - 'a' + 1;
	  }
	}

	string part_a(vector<string> input) {
	  auto charValues = input
		| transform(split_in_half)
		| transform(odd_one_out)
		| transform(char_to_value)
		| common;

	  int score = accumulate(charValues.begin(), charValues.end(), 0);

	  return to_string(score);
	}

	string part_b(vector<string> input) {
	  int badgeSum = 0;
	  for (int i = 0; i < input.size(); i += 3) {
		auto commonChars = to_bitset(input[i])
		  & to_bitset(input[i + 1])
		  & to_bitset(input[i + 2]);

		char ch = 'A';
		while (!commonChars.test(ch)) {
		  ++ch;
		}

		badgeSum += char_to_value(ch);
	  }

	  return to_string(badgeSum);
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
