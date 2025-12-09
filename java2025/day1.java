import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Arrays;
import java.util.List;

public class Main {
	static String example = """
		L68
		L30
		R48
		L5
		R60
		L55
		L1
		L99
		R14
		L82
		""";

	public static void main(String[] args) {
		try {
			List<String> test = Arrays.asList(example.split("\\r?\\n"));
			Path path = Paths.get("inputs/day1.txt");
			List<String> input = Files.readAllLines(path);
			//part1(input);
			part2(input);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void part1(List<String> input) {
		int dial = 50;
		int count = 0;
		for (String line : input) {
			String direction = line.substring(0, 1);
			int value = Integer.valueOf(line.substring(1));
			if ("L".equals(direction)) {
				// Left
				dial -= value;
				while (dial < 0) {
					dial = 100 - Math.abs(dial);
				}
			} else {
				// Right
				dial += value;
				while (dial > 99) {
					dial = Math.abs(dial) - 100;
				}
			}

			// 1150

			if (dial == 0) {
				count++;
			}

			//System.out.println(String.format("DIR %s VAL %d - %d", direction, value, dial));
		}

		System.out.println("Part1: " + count);
	}

	public static void part2(List<String> input) {
		int dial = 50;
		int count = 0;
		for (String line : input) {
			String direction = line.substring(0, 1);
			int value = Integer.valueOf(line.substring(1));
			if ("L".equals(direction)) {
				// Left
				dial -= value;
				while (dial < 0) {
					dial = 100 - Math.abs(dial);
					count++;
				}
			} else {
				// Right
				dial += value;
				while (dial > 99) {
					dial = Math.abs(dial) - 100;
					count++;
				}
			}

			if (dial == 0) {
			//	count++;
			}

			//System.out.println(String.format("DIR %s VAL %d - %d", direction, value, dial));
		}

		System.out.println("Part2: " + count);

	}
}
