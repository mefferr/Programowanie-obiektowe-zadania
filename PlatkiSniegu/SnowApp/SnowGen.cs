using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace SnowApp
{
    class SnowGen
    {
        static Random random = new Random();

        ConsoleColor[] colors;
        bool infinite;
        int rows;
        int interval;
        double minChance;
        double maxChance;

        public SnowGen(bool infinite = true, double minChance = 0.3, double maxChance = 0.5, int rows = 99999, int interval = 500)
        {
            this.infinite = infinite;
            this.rows = rows;
            this.interval = interval;
            this.minChance = minChance;
            this.maxChance = maxChance;
            this.colors = new ConsoleColor[] { ConsoleColor.Blue, ConsoleColor.White, ConsoleColor.Cyan, ConsoleColor.DarkBlue };
        }

        public ConsoleColor GetColor()
        {
            return colors[random.Next(0, colors.Count() - 1)];
        }

        public char GetFlake(double chance)
        {
            double randomDouble = random.NextDouble();

            if (randomDouble < chance)
            {
                return '*';
            }

            return ' ';
        }

        public List<ConsoleColor> GetColors(int characters)
        {
            List<ConsoleColor> colors = new List<ConsoleColor>();

            for (int i = 0; i < characters; i++)
            {
                colors.Add(GetColor());
            }

            return colors;
        }

        public string GetRow(int characters)
        {
            double chance = random.NextDouble() * (maxChance - minChance) + minChance;
            string flakes = "";

            for (int i = 0; i < characters; i++)
            {
                flakes += GetFlake(chance);
            }

            return flakes += '\n';
        }

        void PrintScreen(List<string> screen, List<List<ConsoleColor>> colors)
        {
            Console.SetCursorPosition(0, 0);

            var linesAndColorLists = screen.Zip(colors, (line, colorList) => new { Line = line, ColorList = colorList });
            foreach (var lineAndColorList in linesAndColorLists)
            {
                var charsAndColors = lineAndColorList.Line.Zip(lineAndColorList.ColorList, (character, color) => new { Character = character, Color = color });
                foreach (var charAndColor in charsAndColors)
                {
                    Console.ForegroundColor = charAndColor.Color;
                    Console.Write(charAndColor.Character);
                }
            }
        }

        public void Run()
        {
            int currentRow = 0;

            List<string> screen = new List<string>();
            List<List<ConsoleColor>> screenColors = new List<List<ConsoleColor>>();

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            while (infinite || currentRow++ < rows)
            {
                if (screen.Count >= height)
                {
                    screen.RemoveAt(screen.Count - 1);
                    screenColors.RemoveAt(screenColors.Count - 1);
                }

                screen.Insert(0, GetRow(width));
                screenColors.Insert(0, GetColors(width));

                PrintScreen(screen, screenColors);

                Thread.Sleep(interval);
            }
        }
    }
}
