#include <algorithm>
#include <map>
#include <numeric>

#include "05.h"

namespace aoc {
  namespace day05 {
	using std::string;
	using std::to_string;
	using std::vector;
	using std::tuple;
	using std::make_tuple;
	using std::accumulate;

	// using vector instead of stack for day 2
	using CrateStacks = vector<vector<char>>;
	using MoveInstruction = tuple<int, int, int>;

	bool is_digit(char ch) {
	  return ch >= '0' && ch <= '9';
	}

	CrateStacks parse_crates(vector<string> file, int& position) {
	  string line = file[position];
	  CrateStacks crateStacks;

	  while (line != "") {
		for (int i = 1; i < line.length(); i += 4) {
		  int pos = i / 4;
		  if (crateStacks.size() <= pos) {
			crateStacks.push_back(vector<char>());
		  }
		  if (line[i] != ' ' && !is_digit(line[i])) {
			crateStacks[pos].push_back(line[i]);
		  }
		}

		line = file[++position];
	  }

	  for (vector<char>& stack : crateStacks) {
		std::reverse(stack.begin(), stack.end());
	  }

	  return crateStacks;
	}

	MoveInstruction parse_instruction(string line) {
	  constexpr int moveLength = 5; // "move ".length()
	  constexpr int fromLength = 6; // " from ".length()
	  constexpr int toLength = 4;   // " to ".length()

	  int amountIntLength = 1;
	  while (line[moveLength + amountIntLength] != ' ') {
		++amountIntLength;
	  }

	  int amount = stoi(line.substr(moveLength, amountIntLength));

	  return make_tuple(
		amount,
		line[moveLength + amountIntLength + fromLength] - '0',
		line[moveLength + amountIntLength + fromLength + 1 + toLength] - '0'
	  );
	}

	void move_one(CrateStacks& crateStacks, int from, int to) {

	  if (crateStacks[from - 1].empty())
	  {
		printf("Could not move from %d to %d because the former had no crates", from, to);
		exit(1);
	  }
	  char crate = crateStacks[from - 1].back();
	  crateStacks[from - 1].pop_back();
	  crateStacks[to - 1].push_back(crate);
	}

	void execute_instruction_a(CrateStacks& crateStacks, MoveInstruction instruction) {
	  auto& [amount, from, to] = instruction;

	  for (int i = 0; i < amount; ++i)
	  {
		move_one(crateStacks, from, to);
	  }
	}
	
	void execute_instruction_b(CrateStacks& crateStacks, MoveInstruction instruction) {
	  auto& [amount, from, to] = instruction;

	  int fromSize = crateStacks[from - 1].size();
	  for (int i = amount; i > 0; --i)
	  {
		char crate = crateStacks[from - 1][fromSize - i];
		crateStacks[to - 1].push_back(crate);
	  }
	  crateStacks[from - 1].resize(fromSize - amount);
	}

	string get_top_crates(CrateStacks& crateStacks) {
	  return accumulate(
		std::next(crateStacks.begin()),
		crateStacks.end(),
		string(1, crateStacks[0].back()),
		[](string str, vector<char> crates) { return str + crates.back(); }
	  );
	}

	string part_a(vector<string> input) {
	  int pos = 0;
	  CrateStacks crateStacks = parse_crates(input, pos);
	  ++pos; // skip emty line

	  while (pos < input.size()) {
		MoveInstruction instruction = parse_instruction(input[pos]);
		execute_instruction_a(crateStacks, instruction);
		++pos;
	  }

	  return get_top_crates(crateStacks);
	}

	string part_b(vector<string> input) {
	  int pos = 0;
	  CrateStacks crateStacks = parse_crates(input, pos);
	  ++pos; // skip emty line

	  while (pos < input.size()) {
		MoveInstruction instruction = parse_instruction(input[pos]);
		execute_instruction_b(crateStacks, instruction);
		++pos;
	  }

	  return get_top_crates(crateStacks);
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