package aoc2024

import "core:fmt"
import "core:os"
import "core:strings"
import "core:strconv"
import "core:slice"

main :: proc() {
    fmt.println("Advent of Code 2024")
    // day1_input := read_file("days/day1.txt")
    // day1(day1_input)
    // delete(day1_input)

    day2_input := read_file("days/day2.txt")
    day2(day2_input)
    delete(day2_input)
}

read_file :: proc(filename: string) -> string {
    data, ok := os.read_entire_file_from_filename(filename)
    if !ok {
        fmt.panicf("Error reading file %s", filename)
    }
    return string(data)
}

day1 :: proc(input: string) {
    fmt.println(">>Day 1")
    day1_part1(input)
    day1_part2(input)
}

day1_test_data :: proc() -> string {
    input := `3   4
4   3
2   5
1   3
3   9
3   3
`
    return input
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

day2 :: proc(input: string) {
    fmt.println(">>Day 2")
    
    day2_part1(input)
    day2_part2(input)
}

day2_test_data :: proc() -> string {
    input := `7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9
`
    return input
}

day2_part1 :: proc(input: string) {
    sum_safe_report := 0
    is_safe_report := 0
    for row, i in strings.split(input, "\n") {
        if (row == "") {
            break
        }

        reports: [dynamic]int
        for col, j in strings.split(row, " ") {
            append(&reports, strconv.atoi(col))
        }

        sum_safe_report += day2_is_safe_report(reports[:])
        is_safe_report = 0
    }

    //549 Correct
    fmt.printf("Day 2 part 1 >>> %d\n", sum_safe_report)
}

day2_part2 :: proc(input: string) {
    sum_safe_report := 0
    is_safe_report := 0
    for row, i in strings.split(input, "\n") {
        if (row == "") {
            break
        }

        reports: [dynamic]int
        for col, j in strings.split(row, " ") {
            append(&reports, strconv.atoi(col))
        }

        is_safe_report = day2_is_safe_report(reports[:])
        if is_safe_report == 0 {
            //brute force removing 1
            //TODO: refactor
            for tries := 0; tries < len(reports); tries += 1 {
                reports2: [dynamic]int
                for copy, c in reports {
                    if c == tries {
                        //skip
                        continue
                    }
                    append(&reports2, copy)
                }

                is_safe_report = day2_is_safe_report(reports2[:])
                delete(reports2)
                if is_safe_report == 1 {
                    break
                }
            }
        }

        delete(reports)
        sum_safe_report += is_safe_report
    }

    // Correct 589
    fmt.printf("Day 2 part 2 >>> %d\n", sum_safe_report)
}

day2_is_safe_report :: proc(reports: []int) -> int {
    prev_report := -1
    is_safe_report := 0
    direction := 0 //-1 decreasing / 0 not set / 1 increasing
    for report in reports {
        if prev_report == -1 {
            prev_report = report
            continue
        }

        //find direction
        if prev_report < report { //asc
            if direction == -1 { //if direction was desc it's unsafe
                is_safe_report = 0
                break
            }
            direction = 1
        } else if prev_report > report { //desc
            if direction == 1 { //if direction was asc it's unsafe
                is_safe_report = 0
                break
            }
            direction = -1
        } else {
            is_safe_report = 0
            break
        }

        if direction == 1 {
            diff := report - prev_report
            if diff >= 1 && diff <= 3 {
                is_safe_report = 1
                prev_report = report
                continue
            } else {
                is_safe_report = 0
                break
            }
        } else {
            diff := prev_report - report
            if diff >= 1 && diff <= 3 {
                is_safe_report = 1
                prev_report = report
                continue
            } else {
                is_safe_report = 0
                break
            }
        }
    }

    return is_safe_report
}