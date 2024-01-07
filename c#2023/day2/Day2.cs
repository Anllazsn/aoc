using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_2023.day2;

internal class Day2
{
    class Game
    {
        public int id = 0;
        public List<GameSet> GameSets = new List<GameSet>();
    }

    class GameSet
    {
        public List<Cube> Cubes= new List<Cube>();
    }

    [DebuggerDisplay("{quantity} {color}")]
    class Cube
    {
        public int quantity = 0;
        public string color = "";
    }

    enum Token
    {
        None,
        Quantity,
        Color,
        EndCube,
        StartSet,
        EndSet
    }

    string[] rawGames;
    List<Game> games;
    public Day2()
    {
        var rawGames = File.ReadAllLines("day2/input2.txt");
        //var rawGames = new List<string>
        //{
        //    "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
        //    "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
        //    "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
        //    "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
        //    "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        //};
        games = new List<Game>();
        foreach (var rg in rawGames)
        {
            games.Add(ParseGame(rg));
        }
    }

    public void Step1()
    {
        int sum = 0;
        foreach (var g in games)
        {
            bool isValid = true;
            foreach (var set in g.GameSets)
            {
                var blue = set.Cubes.FirstOrDefault(x => x.color == "blue")?.quantity ?? 0;
                var red = set.Cubes.FirstOrDefault(x => x.color == "red")?.quantity ?? 0;
                var green = set.Cubes.FirstOrDefault(x => x.color == "green")?.quantity ?? 0;

                if (red > 12 ||
                    green > 13 ||
                    blue > 14)
                {
                    isValid = false;
                }
            }

            if (isValid)
                sum += g.id;
        }
        Console.WriteLine($"Day 2 Step 1 {sum}");
    }

    public void Step2()
    {
        int sum = 0;
        foreach (var g in games)
        {
            int min_blue = 1;
            int min_red = 1;
            int min_green = 1;
            foreach (var set in g.GameSets)
            {
                var blue = set.Cubes.FirstOrDefault(x => x.color == "blue")?.quantity ?? 0;
                var red = set.Cubes.FirstOrDefault(x => x.color == "red")?.quantity ?? 0;
                var green = set.Cubes.FirstOrDefault(x => x.color == "green")?.quantity ?? 0;

                if (min_blue == 1 && blue > 0)
                    min_blue = blue;
                else if (blue > min_blue)
                    min_blue = blue;

                if (min_red == 1 && red > 0)
                    min_red = red;
                else if (red > min_red)
                    min_red = red;

                if (min_green == 1 && green > 0)
                    min_green = green;
                else if (green > min_green)
                    min_green = green;
            }

            sum += min_blue * min_red * min_green;
        }
        Console.WriteLine($"Day 2 Step 2 {sum}");
    }

    private Game ParseGame(string rawGame)
    {
        var game = new Game();
        int start = 5;
        int end = 0;
        for (int i = start; i < rawGame.Length; i++)
        {
            if (rawGame[i] == ':')
            {
                end = i;
                break;
            }
        }
        
        // Trim
        game.id = Convert.ToInt32(rawGame[start..end]);
        start = end + 1;
        for (int i = start; i < rawGame.Length; i++)
        {
            if (rawGame[i] != ' ')
            {
                start = i;
                break;
            }
        }

        // Parse cubes
        GameSet gameSet = new GameSet();
        Cube cube = new Cube();
        int tokenStart = start;
        int tokenEnd = 0;
        for (int i = tokenStart; i < rawGame.Length; i++)
        {
            if (rawGame[i] == ',') // End cube
            {
                tokenEnd = i;
                cube = ParseCube(rawGame[tokenStart..tokenEnd]);
                gameSet.Cubes.Add(cube);
                i++;
                while (rawGame[i] == ' ')
                {
                    i++;
                }
                tokenStart = i;
            }
            else if (rawGame[i] == ';') // End game set
            {
                tokenEnd = i;
                cube = ParseCube(rawGame[tokenStart..tokenEnd]);
                gameSet.Cubes.Add(cube);
                game.GameSets.Add(gameSet);
                gameSet = new GameSet();
                i++;
                while (rawGame[i] == ' ')
                {
                    i++;
                }
                tokenStart = i;
            }
        }
        tokenEnd = rawGame.Length;
        cube = ParseCube(rawGame[tokenStart..tokenEnd]);
        gameSet.Cubes.Add(cube);
        game.GameSets.Add(gameSet);

        return game;
    }

    private Cube ParseCube(string s)
    {
        var tokens = s.Split(' ');

        var cube = new Cube();
        cube.quantity = Convert.ToInt32(tokens[0]);
        cube.color = tokens[1];

        return cube;
    }
}
