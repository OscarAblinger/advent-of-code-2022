#include <string>
#include <iostream>
#include <iomanip>
#include <fstream>
#include <sstream>

#include "aoc2022.h"
#include "days/01.h"
#include "days/02.h"
#include "days/03.h"
#include "days/05.h"

using std::cin;
using std::cout;
using std::endl;
using std::make_tuple;
using std::string;
using std::tuple;
using std::vector;
using std::stringstream;
using std::ifstream;

using aoc::AOCFunction;
using aoc::part_t;

enum source_t {
  testA,
  testB,
  real,
};

vector<string> tokenize_parameters(string params) {
  stringstream ss(params);
  vector<string> args;
  
  for (string param; ss >> param;)
	args.push_back(param);

  return args;
}

tuple<int, part_t, source_t> parse_parameters(vector<string> params) {
  int day = std::stoi(params[0]);

  if (day <= 0) {
	cout << "Day must be at least 1. Given: " << day << endl;
	exit(1);
  }

  part_t part;

  if (params[1] == "b") {
	part = aoc::partB;
  }
  else
  {
	part = aoc::partA;
  }

  source_t sourceType;
  if (params.size() <= 2 || params[2] == "r" || params[2] == "real") {
	sourceType = real;
  }
  else if (params[2] == "a" || params[2] == "t" || params[2] == "test")
  {
	sourceType = testA;
  }
  else if (params[2] == "b")
  {
	sourceType = testB;
  }

  return make_tuple(day, part, sourceType);
}

tuple<int, part_t, source_t> get_parameters(int argc, char* argv[]) {
  vector<string> params;

  if (argc > 1) {
	for (int i = 1; i < argc; ++i) {
	  params.push_back(string(argv[i]));
	}
  }
  else
  {
	string params_as_string("");

	cout << "Please add parameters: ";
	std::getline(cin, params_as_string);
	cout << endl;

	params = tokenize_parameters(params_as_string);
  }
  return parse_parameters(params);
}

vector<string> read_file(int day, string suffix) {
  stringstream ss;
  ss << "../../../days/day" << std::setw(2) << std::setfill('0') << day << suffix;
  ifstream file(ss.str());

  vector<string> content;
  if (!file.is_open())
  {
	cout << "couldn't open file";
	exit(1);
  }
  else
  {
	string line;
	while (std::getline(file, line)) {
	  content.push_back(line);
	}
	file.close();
  }
  return content;
}

int main(int argc, char* argv[])
{
  auto [day, part, inputSrc] = get_parameters(argc, argv);

  AOCFunction days[] = {
	aoc::day01::run,
	aoc::day02::run,
	aoc::day03::run,
	[](auto part, auto input) -> string { printf("day 4 not implemented"); exit(1); },
	aoc::day05::run,
  };

  vector<string> input;
  switch (inputSrc) {
  case testA:
	input = read_file(day, "_test.txt");
	break;
  case testB:
	input = read_file(day, "_test_b.txt");
	break;
  case real:
	input = read_file(day, ".txt");
	break;
  }

  string result = days[day - 1](part, input);

  cout << result << endl;

  return 0;
}
