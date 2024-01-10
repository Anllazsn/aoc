using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static c_2023.day3.Day3;

namespace c_2023.day3;

public class Day3
{
    public class Engine
    {
        public int number;
        public int row;
        public int pos_start;
        public int pos_end;
    }

    public struct Symbol
    {
        public char value;
        public int row;
        public int index;
    }

    List<Engine> engines;
    List<Symbol> symbols; // The index of symbols

    public Day3()
    {
        engines = new List<Engine>();
        symbols = new List<Symbol>();

        var rawEngineSchematics = File.ReadAllLines("day3/input3.txt");
        //var rawEngineSchematics = new string[]
        //{
        //    "467..114..",
        //    "...*......",
        //    "..35..633.",
        //    "......#...",
        //    "617*......",
        //    ".....+.58.",
        //    "..592.....",
        //    "......755.",
        //    "...$.*....",
        //    ".664.598.."
        //};

        ParseEngineSchematic(rawEngineSchematics);
    }

    public void Step1()
    {
        var sum = 0;

        foreach (var engine in engines)
        {
            var nearRow = symbols.Where(x => engine.row == x.row ||
                                             engine.row + 1 == x.row ||
                                             engine.row - 1 == x.row);

            if (nearRow.Any())
            {
                var start = engine.pos_start - 1;
                var end = engine.pos_end + 1;

                if (start < 0)
                    start = 0;

                var nearIndex = nearRow.Any(x => start <= x.index &&
                                                 end >= x.index);

                if (nearIndex)
                {
                    sum += engine.number;
                }
            }
        }

        Console.WriteLine($"Day 3 Step 1 {sum}");
    }

    public void Step2()
    {
        var sum = 0;

        foreach (var s in symbols)
        {
            if (s.value != '*')
                continue;

            var nearRow = engines.Where(x => x.row == s.row ||
                                             x.row + 1 == s.row ||
                                             x.row - 1 == s.row);

            if (nearRow.Any())
            {
                var enginesNearPower = nearRow.Where(x => x.pos_start - 1 <= s.index &&
                                                          x.pos_end + 1 >= s.index);

                if (enginesNearPower.Count() > 1)
                {
                    int powered = 1;
                    foreach (var e in enginesNearPower)
                    {
                        powered *= e.number;
                    }

                    if (powered > 1)
                    {
                        sum += powered;
                    }
                }
            }
        }

        Console.WriteLine($"Day 3 Step 2 {sum}");
    }

    private void ParseEngineSchematic(string[] raw)
    {
        for (int row = 0; row < raw.Length; row++)
        {
            string s = raw[row];
            for (int i = 0; i < s.Length; i++)
            {
                if (IsNumber(s[i]))
                {
                    int j = i + 1;
                    while (j < s.Length && IsNumber(s[j]))
                    {
                        j++;
                    }

                    var engine = new Engine();
                    engine.number = Convert.ToInt32(s[i..j]);
                    engine.row = row;
                    engine.pos_start = i;
                    engine.pos_end = j - 1;
                    engines.Add(engine);
                    i = j - 1;
                }
                else if (IsSymbol(s[i]))
                {
                    symbols.Add(new Symbol
                    {
                        value = s[i],
                        row = row,
                        index = i
                    });
                }
                else
                {
                    continue;
                }
            }
        }
    }

    private bool IsNumber(char c)
    {
        switch (c)
        {
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                return true;
        }

        return false;
    }

    private bool IsSymbol(char c)
    {
        switch (c)
        {
            case '.':
                return false;
        }

        return true;
    }
}
