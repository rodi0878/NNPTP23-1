using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
         static readonly Color[] colors = new Color[]
            {
                Color.Red,
                Color.Blue,
                Color.Green,
                Color.Yellow,
                Color.Orange,
                Color.Fuchsia,
                Color.Gold,
                Color.Cyan,
                Color.Magenta
            };

        static int width;
        static int height;
        static string output;
        static Bitmap bitmap;
        static double xMin;
        static double xMax;
        static double yMin;
        static double yMax;
        static double xStep;
        static double yStep;

        static void Main(string[] args)
        {
            VariablesInitialization(args);

            List<ComplexNumber> roots = new List<ComplexNumber>();
            Polynomial polynomial = InitializePolynomial();
            Polynomial derivedPolynomial = polynomial.Derive();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // find "world" coordinates of pixel
                    ComplexNumber ox = FindCoordinates(xMin, yMin, xStep, yStep, x, y);

                    // find solution of equation using newton'startingComplexNumber iteration
                    float it = CalculationOfNewtonIteration(polynomial, derivedPolynomial, ref ox);

                    // find solution root number
                    int id = FindRootSolution(roots, ox);

                    // colorize pixel according to root number
                    ColorizePixel(bitmap, x, y, it, id);
                }
            }
            bitmap.Save(output ?? "../../../out.png");
        }

        private static void VariablesInitialization(string[] args)
        {
            width = int.Parse(args[0]);
            height = int.Parse(args[1]);
            output = args[6];
            bitmap = new Bitmap(width, height);
            xMin = double.Parse(args[2]);
            xMax = double.Parse(args[3]);
            yMin = double.Parse(args[4]);
            yMax = double.Parse(args[5]);
            xStep = (xMax - xMin) / width;
            yStep = (yMax - yMin) / height;
        }

        private static Polynomial InitializePolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            return polynomial;
        }

        private static ComplexNumber FindCoordinates(double xMin, double yMin, double xStep, double yStep, int y, int x)
        {
            double xPart = xMin + x * xStep;
            double yPart = yMin + y * yStep;

            ComplexNumber coordinates = new ComplexNumber()
            {
                RealPart = xPart,
                ImaginaryPart = yPart
            };

            if (coordinates.RealPart == 0)
                coordinates.RealPart = 0.0001;
            if (coordinates.ImaginaryPart == 0)
                coordinates.ImaginaryPart = 0.0001f;

            return coordinates;
        }

        private static int FindRootSolution(List<ComplexNumber> roots, ComplexNumber root)
        {
            bool knownRoot = false;
            var rootId = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(root.RealPart - roots[i].RealPart, 2) + Math.Pow(root.ImaginaryPart - roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    knownRoot = true;
                    rootId = i;
                }
            }
            if (!knownRoot)
            {
                roots.Add(root);
                rootId = roots.Count;
            }

            return rootId;
        }

        private static void ColorizePixel(Bitmap bitmap, int x, int y, float it, int rootId)
        {
            Color selectedColor = colors[rootId % colors.Length];
            selectedColor = Color.FromArgb(selectedColor.R, selectedColor.G, selectedColor.B);
            selectedColor = Color.FromArgb(Math.Min(Math.Max(0, selectedColor.R - (int)it * 2), 255),
                                           Math.Min(Math.Max(0, selectedColor.G - (int)it * 2), 255),
                                           Math.Min(Math.Max(0, selectedColor.B - (int)it * 2), 255));
            bitmap.SetPixel(x, y, selectedColor);
        }

        private static int CalculationOfNewtonIteration(Polynomial polynomial, Polynomial derivedPolynomial, ref ComplexNumber root)
        {
            int iteration = 0;
            for (int i = 0; i < 30; i++)
            {
                ComplexNumber difference = polynomial.Eval(root).Divide(derivedPolynomial.Eval(root));
                root = root.Subtract(difference);

                if (Math.Pow(difference.RealPart, 2) + Math.Pow(difference.ImaginaryPart, 2) >= 0.5)
                {
                    i--;
                }
                iteration++;
            }
            return iteration;
        }
    }
}
