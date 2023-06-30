#include <algorithm>
#include <numeric>

#include "01.h"

namespace aoc {
  namespace day01 {
	using std::string;
	using std::vector;


	string part_a(vector<string> input) {
	  int currentCalories = 0;
	  int maxCalories = 0;

	  for (auto line : input) {
		if (line == "") {
		  if (currentCalories > maxCalories) {
			maxCalories = currentCalories;
		  }
		  currentCalories = 0;
		}
		else
		{
		  currentCalories += stoi(line);
		}
	  }
	  if (currentCalories > maxCalories) {
		maxCalories = currentCalories;
	  }
	  
	  return std::to_string(maxCalories);
	}

	string part_b(vector<string> input) {
	  int currentCalories = 0;
	  vector<int> caloriesSums;
	  for (auto line : input) {
		if (line == "") {
		  caloriesSums.push_back(currentCalories);
		  currentCalories = 0;
		}
		else
		{
		  currentCalories += stoi(line);
		}
	  }
	  caloriesSums.push_back(currentCalories);

	  vector<int> top3(3);
	  std::partial_sort_copy(
		caloriesSums.begin(), caloriesSums.end(),
		top3.begin(), top3.end(),
		std::greater<int>()
	  );
	  return std::to_string(std::accumulate(top3.begin(), top3.end(), 0));
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