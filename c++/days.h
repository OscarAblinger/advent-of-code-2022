#include <functional>
#include <string>

namespace aoc {
  enum part_t {
	partA,
	partB,
  };

  using AOCFunction = std::function<std::string (part_t, std::vector<std::string>)>;
}
