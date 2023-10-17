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
    class NewtonFractal
    {
        private const double POWER = 2;
        private const double POLYNOMIAL_QUOTIENT_TOLERANCE = 0.5;
        private const double ROOT_TOLERANCE = 0.01;
        private const int MAX_NEWTONS_ITERATIONS = 30;
        private const double INITIAL_COORDINATES = 0.0001;
        private const string DEFAULT_FILENAME = "../../../out.png";

        private static int ResultPictureWidth, ResultPictureHeight;
        private static double XMin, YMin, XMax, YMax;
        private static string FileName;
        private static List<ComplexNumber> Roots = new List<ComplexNumber>();
        private static Polynomial Polynomial, PolynomialDerivation;
        private static readonly Color[] Colors = new Color[]
           {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
           };


        static void Main(string[] args)
        {
            ParseArguments(args);

            SetPolynomials();

            Console.WriteLine("Processing the Newton fractal...");
            CreatePicture();

            Console.WriteLine("Result picture successfully saved.");
            Console.WriteLine("Press any key to leave");
            Console.ReadKey();
        }

        private static void ParseArguments(string[] arguments)
        {
            ResultPictureWidth = int.Parse(arguments[0]);
            ResultPictureHeight = int.Parse(arguments[1]);
            XMin = double.Parse(arguments[2]);
            XMax = double.Parse(arguments[3]);
            YMin = double.Parse(arguments[4]);
            YMax = double.Parse(arguments[5]);
            FileName = arguments[6];
        }

        private static void CreatePicture()
        {
            Bitmap bitmap = new Bitmap(ResultPictureWidth, ResultPictureHeight);

            // for every pixel in image...
            for (int i = 0; i < ResultPictureWidth; i++)
            {
                for (int j = 0; j < ResultPictureHeight; j++)
                {
                    // find "world" coordinates of pixel
                    ComplexNumber root = FindPixelCoordinates(i, j);

                    Color pixelColor = FindPixelColor(ref root);
                    bitmap.SetPixel(j, i, pixelColor);
                }
            }

            bitmap.Save(FileName ?? DEFAULT_FILENAME);
        }

        private static Polynomial CreatePolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.AddCoefficient(new ComplexNumber() { RealPart = 1 });
            polynomial.AddCoefficient(ComplexNumber.Zero);
            polynomial.AddCoefficient(ComplexNumber.Zero);
            polynomial.AddCoefficient(new ComplexNumber() { RealPart = 1 });
            return polynomial;

        }

        private static void SetPolynomials()
        {
            Polynomial = CreatePolynomial();
            Console.WriteLine("Polynimial: " + Polynomial);
            PolynomialDerivation = Polynomial.Derive();
            Console.WriteLine("Polynomial derivation: " + PolynomialDerivation);
        }

        private static int SolveEquation(ref ComplexNumber root)
        {
            int iteration = 0;
            for (int i = 0; i < MAX_NEWTONS_ITERATIONS; i++)
            {
                var quotient = Polynomial.Evaluate(root).Divide(PolynomialDerivation.Evaluate(root));
                root = root.Subtract(quotient);

                if (Math.Pow(quotient.RealPart, POWER) + Math.Pow(quotient.ImaginaryPart, POWER) >= POLYNOMIAL_QUOTIENT_TOLERANCE)
                {
                    i--;
                }
                iteration++;
            }
            return iteration;
        }

        private static int FindRoots(ComplexNumber root, ref List<ComplexNumber> roots)
        {
            var known = false;
            var rootIndex = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(root.RealPart - roots[i].RealPart, POWER) + Math.Pow(root.ImaginaryPart - roots[i].ImaginaryPart, POWER) <= ROOT_TOLERANCE)
                {
                    known = true;
                    rootIndex = i;
                }
            }
            if (!known)
            {
                roots.Add(root);
                rootIndex = roots.Count;
            }
            return rootIndex;
        }

        private static ComplexNumber FindPixelCoordinates(int column, int row)
        {
            double xStep = (XMax - XMin) / ResultPictureWidth;
            double yStep = (YMax - YMin) / ResultPictureHeight;
            double y = YMin + column * yStep;
            double x = XMin + row * xStep;

            ComplexNumber root = new ComplexNumber()
            {
                RealPart = x,
                ImaginaryPart = y
            };

            if (root.RealPart == 0)
                root.RealPart = INITIAL_COORDINATES;
            if (root.ImaginaryPart == 0)
                root.ImaginaryPart = INITIAL_COORDINATES;

            return root;
        }

        private static Color FindPixelColor(ref ComplexNumber root)
        {
            // find solution of equation using newton's iteration
            int iteration = SolveEquation(ref root);

            // find solution root number
            int rootIndex = FindRoots(root, ref Roots);

            // colorize pixel according to root number
            Color pixelColor = Colors[rootIndex % Colors.Length];
            pixelColor = Color.FromArgb(Math.Min(Math.Max(0, pixelColor.R - (int)iteration * 2), 255), Math.Min(Math.Max(0, pixelColor.G - (int)iteration * 2), 255), Math.Min(Math.Max(0, pixelColor.B - (int)iteration * 2), 255));

            return pixelColor;
        }
    }
}