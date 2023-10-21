using System;
using System.Linq;

namespace NNPTPZ1
{
    public sealed class CommandLineArguments
    {
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception> 
        /// <exception cref="OverflowException"></exception> 
        public static CommandLineArguments Parse(string input, char delimiter = ' ')
        {
            return Parse(input?.Split(delimiter));
        }
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception> 
        /// <exception cref="OverflowException"></exception> 
        public static CommandLineArguments Parse(string[] args)
        {
            if (args == null || args.Length < 6)
            {
                throw new ArgumentException("Expected format: <width> <height> <minX> <maxX> <minY> <maxY> [<outputPath>]");
            }
            return new CommandLineArguments()
            {
                Width = int.Parse(args[0]),
                Height = int.Parse(args[1]),
                MinimumX = double.Parse(args[2]),
                MaximumX = double.Parse(args[3]),
                MinimumY = double.Parse(args[4]),
                MaximumY = double.Parse(args[5]),
                OutputPath = args.ElementAtOrDefault(6) 
            };
        }
        private CommandLineArguments() { }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public double MinimumX { get; private set; }
        public double MaximumX { get; private set; }
        public double MinimumY { get; private set; }
        public double MaximumY { get; private set; }
        public string OutputPath { get; private set; }
    }
}
