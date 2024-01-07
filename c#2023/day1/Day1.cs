using System.Text.RegularExpressions;

namespace c_2023.day1;

public class Day1
{
    string[] cals;
    public Day1()
    {
        cals = File.ReadAllLines("day1/input1.txt");
    }

    public void Step1()
    {
        var sum = 0;
        foreach (var c in cals)
        {
            var first = Convert.ToInt32(c.First(x => Regex.IsMatch(x.ToString(), "[0-9]")).ToString());
            var last = Convert.ToInt32(c.Last(x => Regex.IsMatch(x.ToString(), "[0-9]")).ToString());
            sum += first * 10 + last;
        }
        Console.WriteLine($"Day 1 Step 1 {sum}");
    }

    public void Step2()
    {
        var sum = 0;
        foreach (var c in cals)
        {
            int cal = 0;
            int firstCalibration = 0;
            int secondCalibration = 0;
            int tempVal = 0;
            //first calibration
            for (var i = 0; i < c.Length; i++)
            {
                if (firstCalibration == 0)
                {
                    if (IsNumber(c[i], out tempVal))
                    {
                        firstCalibration = tempVal * 10;
                        break;
                    }
                    else
                    {
                        for (int j = i + 2; j < c.Length + 1; j++)
                        {
                            string s = c[i..j];
                            if (IsNumber(s, out tempVal))
                            {
                                firstCalibration = tempVal * 10;
                                i = j - 1;
                                break;
                            }
                        }
                    }
                }
            }

            //last calibration
            for (var i = c.Length - 1; i > -1; i--)
            {
                if (IsNumber(c[i], out tempVal))
                {
                    secondCalibration = tempVal;
                    break;
                }
                else
                {
                    for (int j = i - 1; j > -1; j--)
                    {
                        var len = i + 1;
                        string s = c[j..len];
                        if (IsNumber(s, out tempVal))
                        {
                            secondCalibration = tempVal;
                            i = -1;
                            break;
                        }
                    }
                }
            }
            cal = firstCalibration + secondCalibration;
            sum += cal;
        }
        Console.WriteLine($"Day 1 Step 2 {sum}");
    }

    private bool IsNumber(char c, out int n)
    {
        if (c == '1')
        {
            n = 1;
            return true;
        }
        else if (c == '2')
        {
            n = 2;
            return true;
        }
        else if (c == '3')
        {
            n = 3;
            return true;
        }
        else if (c == '4')
        {
            n = 4;
            return true;
        }
        else if (c == '5')
        {
            n = 5;
            return true;
        }
        else if (c == '6')
        {
            n = 6;
            return true;
        }
        else if (c == '7')
        {
            n = 7;
            return true;
        }
        else if (c == '8')
        {
            n = 8;
            return true;
        }
        else if (c == '9')
        {
            n = 9;
            return true;
        }

        n = 0;
        return false;
    }

    private bool IsNumber(string s, out int n)
    {
        if (s == "one")
        {
            n = 1;
            return true;
        }
        else if (s == "two")
        {
            n = 2;
            return true;
        }
        else if (s == "three")
        {
            n = 3;
            return true;
        }
        else if (s == "four")
        {
            n = 4;
            return true;
        }
        else if (s == "five")
        {
            n = 5;
            return true;
        }
        else if (s == "six")
        {
            n = 6;
            return true;
        }
        else if (s == "seven")
        {
            n = 7;
            return true;
        }
        else if (s == "eight")
        {
            n = 8;
            return true;
        }
        else if (s == "nine")
        {
            n = 9;
            return true;
        }
        n = 0;
        return false;
    }
}
