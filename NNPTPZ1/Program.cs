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
        private static readonly Color[] systemColors = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        static void Main(string[] arguments)
        {
            int width = int.Parse(arguments[0]);
            int height = int.Parse(arguments[1]);
            Bitmap image = new Bitmap(width, height);

            double xmin = double.Parse(arguments[2]);
            double xmax = double.Parse(arguments[3]);
            double ymin = double.Parse(arguments[4]);
            double ymax = double.Parse(arguments[5]);
            string output = arguments[6];

            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            List<Complex> roots = new List<Complex>();
            Polynomial polynomial = new Polynomial(new Complex() { RealPart = 1 }, Complex.Zero, Complex.Zero, new Complex() { RealPart = 1 });
            Polynomial derivedPolynomial = polynomial.Derive();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Complex worldCoordinates = findWorldCoordinatesOfPixel(xmin, ymin, xstep, ystep, i, j);

                    float newtonIteration = findSolutionToNewtonEquation(polynomial, derivedPolynomial, ref worldCoordinates);

                    int colorIndex = findSolutionRootNumber(roots, worldCoordinates);

                    colorizePixelByRootNumber(image, i, j, newtonIteration, colorIndex);
                }
            }

            image.Save(output ?? "../../../out.png");
        }

        private static Complex findWorldCoordinatesOfPixel(double xmin, double ymin, double xstep, double ystep, int i, int j)
        {
            double y = ymin + i * ystep;
            double x = xmin + j * xstep;

            Complex worldCoordinates = new Complex()
            {
                RealPart = x,
                ImaginaryPart = (float)(y)
            };

            if (worldCoordinates.RealPart == 0)
                worldCoordinates.RealPart = 0.0001;
            if (worldCoordinates.ImaginaryPart == 0)
                worldCoordinates.ImaginaryPart = 0.0001f;
            return worldCoordinates;
        }

        private static float findSolutionToNewtonEquation(Polynomial polynomial, Polynomial derivedPolynomial, ref Complex worldCoordinates)
        {
            float newtonIteration = 0;
            for (int i = 0; i < 30; i++)
            {
                var difference = polynomial.Evaluate(worldCoordinates).Divide(derivedPolynomial.Evaluate(worldCoordinates));
                worldCoordinates = worldCoordinates.Subtract(difference);

                if (Math.Pow(difference.RealPart, 2) + Math.Pow(difference.ImaginaryPart, 2) >= 0.5)
                {
                    i--;
                }
                newtonIteration++;
            }

            return newtonIteration;
        }

        private static int findSolutionRootNumber(List<Complex> roots, Complex worldCoordinates)
        {
            var known = false;
            var colorIndex = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(worldCoordinates.RealPart - roots[i].RealPart, 2) + Math.Pow(worldCoordinates.ImaginaryPart - roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    known = true;
                    colorIndex = i;
                }
            }
            if (!known)
            {
                roots.Add(worldCoordinates);
                colorIndex = roots.Count;
            }

            return colorIndex;
        }

        private static void colorizePixelByRootNumber(Bitmap image, int i, int j, float newtonIteration, int colorIndex)
        {
            var chosenColor = systemColors[colorIndex % systemColors.Length];
            chosenColor = Color.FromArgb(chosenColor.R, chosenColor.G, chosenColor.B);
            chosenColor = Color.FromArgb(Math.Min(Math.Max(0, chosenColor.R - (int)newtonIteration * 2), 255), Math.Min(Math.Max(0, chosenColor.G - (int)newtonIteration * 2), 255), Math.Min(Math.Max(0, chosenColor.B - (int)newtonIteration * 2), 255));
            image.SetPixel(j, i, chosenColor);
        }
    }
}
