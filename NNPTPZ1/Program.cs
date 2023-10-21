using NNPTPZ1.Fractal;
using NNPTPZ1.Mathematics;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        public const string DefaultOutputPath = "../../../out.png";
        static void Main(string[] args)
        {
            CommandLineArguments commandLineArguments = CommandLineArguments.Parse(args);
            FractalParameters fractalParameters = FractalParameters.FromArguments(commandLineArguments);
            Bitmap fractal = FractalGenerator.GenerateFractal(fractalParameters);
            fractal.Save(commandLineArguments.OutputPath ?? DefaultOutputPath);
        }
    }
}
