using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {

        static Color[] Colors = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta };
        static void Main(string[] args)
        {
            int imageWidth;
            int imageHeigth;
            string imageOutputFile;
            Bitmap resultedBitmap;
            ParseCliArguments(
                in args,
                out imageWidth,
                out imageHeigth,
                out imageOutputFile,
                out resultedBitmap,
                out double xmin,
                out double ymin,
                out double xstep,
                out double ystep
                );

            List<ComplexNumber> roots = new List<ComplexNumber>();

            Polynomial polynomial = new Polynomial(
                new ComplexNumber[4] {
                    new ComplexNumber() { RealPart = 1 },
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                    new ComplexNumber() { RealPart = 1 }
                }
              );
            Polynomial polynomDerivate = polynomial.Derive();

            for (int i = 0; i < imageWidth; i++)
            {
                for (int j = 0; j < imageHeigth; j++)
                {
                    // find "world" coordinates of pixel
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;

                    ComplexNumber pixelWorldCoordinates = new ComplexNumber()
                    {
                        RealPart = x,
                        ImaginaryPart = (float)y
                    };

                    int iterationNumber = FindNewtonEquationSolution(polynomial, polynomDerivate, ref pixelWorldCoordinates);

                    int rootNumber = FindRootNumber(roots, pixelWorldCoordinates);

                    ColorizePixelAccordingToRootNumber(resultedBitmap, i, j, iterationNumber, rootNumber);
                }
            }

            resultedBitmap.Save(imageOutputFile ?? "../../../out.png");
        }

        private static void ParseCliArguments(in string[] args, out int imageWidth, out int imageHeigth, out string imageOutputFile, out Bitmap initializedBitmap, out double xmin, out double ymin, out double xstep, out double ystep)
        {
            imageWidth = int.Parse(args[0]);
            imageHeigth = int.Parse(args[1]);
            imageOutputFile = args[6];
            initializedBitmap = new Bitmap(imageWidth, imageHeigth);
            xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);

            xstep = (xmax - xmin) / imageWidth;
            ystep = (ymax - ymin) / imageHeigth;
        }

        private static void ColorizePixelAccordingToRootNumber(Bitmap resultedBitmap, int x, int y, int iterationNumber, int rootNumber)
        {
            var pixelColor = Colors[rootNumber % Colors.Length];
            pixelColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
            pixelColor = Color.FromArgb(Math.Min(Math.Max(0, pixelColor.R - (int)iterationNumber * 2), 255), Math.Min(Math.Max(0, pixelColor.G - iterationNumber * 2), 255), Math.Min(Math.Max(0, pixelColor.B - iterationNumber * 2), 255));
            resultedBitmap.SetPixel(y, x, pixelColor);
        }

        private static int FindNewtonEquationSolution(Polynomial polynomial, Polynomial polynomDerivate, ref ComplexNumber pixelWorldCoordinates)
        {
            if (pixelWorldCoordinates.RealPart == 0)
                pixelWorldCoordinates.RealPart = 0.0001;
            if (pixelWorldCoordinates.ImaginaryPart == 0)
                pixelWorldCoordinates.ImaginaryPart = 0.0001f;

            int iterationNumber = 0;
            for (int q = 0; q < 30; q++)
            {
                var difference = polynomial.Evaluate(pixelWorldCoordinates).Divide(polynomDerivate.Evaluate(pixelWorldCoordinates));
                pixelWorldCoordinates = pixelWorldCoordinates.Subtract(difference);

                if (Math.Pow(difference.RealPart, 2) + Math.Pow(difference.ImaginaryPart, 2) >= 0.5)
                {
                    q--;
                }
                iterationNumber++;
            }

            return iterationNumber;
        }

        private static int FindRootNumber(List<ComplexNumber> roots, ComplexNumber pixelWorldCoordinates)
        {
            var known = false;
            var id = 0;
            for (int w = 0; w < roots.Count; w++)
            {
                if (Math.Pow(pixelWorldCoordinates.RealPart - roots[w].RealPart, 2) + Math.Pow(pixelWorldCoordinates.ImaginaryPart - roots[w].ImaginaryPart, 2) <= 0.01)
                {
                    known = true;
                    id = w;
                }
            }
            if (!known)
            {
                roots.Add(pixelWorldCoordinates);
                id = roots.Count;
            }

            return id;
        }
    }
}
