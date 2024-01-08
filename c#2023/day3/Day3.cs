using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace c_2023.day3;

public class Day3
{
    public class Engine
    {
        public int number;
        public int pos_start;
        public int pos_end;
    }
    List<Engine> engines;

    public Day3()
    {
        //var rawEngineSchematics = File.ReadAllLines("day3/input3.txt");
        var rawEngineSchematics = new string[]
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };

        ParseEngineSchematic(rawEngineSchematics);
    }

    public void Step1()
    {
        var sum = 0;
        //foreach (string s in rawEngineSchematics)
        //{
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        if (IsSymbol(s[i]))
        //        {
        //            // Save pos
        //        }
        //    }
        //}
        Console.WriteLine($"Day 3 Step 1 {sum}");
    }

    public void Step2()
    {
        var sum = 0;
        Console.WriteLine($"Day 3 Step 2 {sum}");
    }

    private void ParseEngineSchematic(string[] raw)
    {
        engines = new List<Engine>();
        var engine = new Engine();

        foreach (string s in raw)
        {
            int start_token = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (IsNumber(s[i]))
                {
                    start_token = i;
                }
                else if (IsSymbol(s[i]))
                {

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
