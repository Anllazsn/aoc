package aoc2024

import "core:fmt"
import "core:os"
import "core:strings"
import "core:strconv"
import "core:slice"

main :: proc() {
    fmt.println("Advent of Code 2024")
    //day1()
    day2()
}

read_file :: proc(filename: string) -> string {
    data, ok := os.read_entire_file_from_filename(filename)
    if !ok {
        fmt.panicf("Error reading file %s", filename)
    }
    return string(data)
}

day1 :: proc() {
    fmt.println(">>Day 1")
    input := read_file("days/day1.txt")
    defer delete(input)
    day1_part1(input)
    day1_part2(input)
}

day1_part1_test :: proc() {
    input := `3   4
4   3
2   5
1   3
3   9
3   3
`
    day1_part1(input, 1)
}

day1_part2_test :: proc() {
    input := `3   4
4   3
2   5
1   3
3   9
3   3
`
    day1_part2(input, 1)
}

day1_part1 :: proc(input: string, n_size := 5) {
    left_list: [dynamic]int
    right_list: [dynamic]int
    defer delete(left_list)
    defer delete(right_list)

    for s, i in strings.split(input, "\n") {
        if s == "" {
            break
        }

        x := s[:n_size]
        y := s[n_size+3:]
        append(&left_list, strconv.atoi(x))
        append(&right_list, strconv.atoi(y))
    }

    slice.sort(left_list[:])
    slice.sort(right_list[:])

    sum := 0

    for i := 0; i < len(left_list); i += 1 {
        diff := left_list[i] - right_list[i]
        sum += abs(diff)
    }

    //2580760 Correct
    fmt.printf("Day 1 part 1 >>> %d\n", sum)
}

day1_part2 :: proc(input: string, n_size := 5) {
    left_list: [dynamic]int
    right_list: [dynamic]int
    defer delete(left_list)
    defer delete(right_list)

    for s, i in strings.split(input, "\n") {
        if s == "" {
            break
        }

        x := s[:n_size]
        y := s[n_size+3:]
        append(&left_list, strconv.atoi(x))
        append(&right_list, strconv.atoi(y))
    }

    sum := 0

    for i := 0; i < len(left_list); i += 1 {
        count := 0
        for j := 0; j < len(right_list); j += 1 {
            if left_list[i] == right_list[j] {
                count += 1
            }
        }
        similarity := left_list[i] * count
        sum += abs(similarity)
    }

    //25358365 Correct
    fmt.printf("Day 1 part 2 >>> %d\n", sum)
}

day2 :: proc() {
    fmt.println(">>Day 2")
    input := read_file("days/day2.txt")
    defer delete(input)
    day2_part1(input)
    // day2_part2(input)
}

day2_part1 :: proc(input: string) {

}