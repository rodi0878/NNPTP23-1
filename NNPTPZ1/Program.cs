
using NNPTPZ1.Mathematics;
using System.Collections.Generic;
using System.Drawing;
using System;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            FractalSetup fractal = new FractalSetup(args);
            fractal.CreateImage();
        }
    }

    public class FractalSetup
    {
        private const double ALMOST_ZERO = 0.0001;
        private const int MAX_ITERATIONS = 40;
        private const double NEWTONS_METHOD_TOLERANCE = 0.7;
        private const double SQUARE_ROOT_TOLERANCE = 0.1;
        private const string DEFAULT_FILENAME = "../../../Newton_fractal.png";

        public int Width { get; set; }
        public int Height { get; set; }
        public double XAxisMinimum { get; set; }
        public double XAxisMaximum { get; set; }
        public double YAxisMinimum { get; set; }
        public double YAxisMaximum { get; set; }
        public string FileName { get; set; }
        public Bitmap Image { get; set; }
        public double XStep { get; set; }
        public double YStep { get; set; }
        public List<ComplexNumber> Roots { get; set; } = new List<ComplexNumber>();
        public Polynomial Polynomial { get; set; } = new Polynomial();
        public Polynomial DerivedPolynomial { get; set; }
        public Color[] Colors { get; } = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        public FractalSetup(string[] arguments)
        {
            Width = int.Parse(arguments[0]);
            Height = int.Parse(arguments[1]);
            XAxisMinimum = double.Parse(arguments[2]);
            XAxisMaximum = double.Parse(arguments[3]);
            YAxisMinimum = double.Parse(arguments[4]);
            YAxisMaximum = double.Parse(arguments[5]);
            FileName = arguments.Length > 6 ? arguments[6] : DEFAULT_FILENAME;

            Image = new Bitmap(Width, Height);

            XStep = (XAxisMaximum - XAxisMinimum) / Width;
            YStep = (YAxisMaximum - YAxisMinimum) / Height;
        }

        public void CreateImage()
        {
            SetPolynomials();

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    ComplexNumber complexRoot = FindWorldCoordinates(i, j);
                    Color pixelColor = GetPixelColor(complexRoot);

                    Image.SetPixel(i, j, pixelColor);
                }
            }

            Image.Save(FileName);
        }

        private Color GetPixelColor(ComplexNumber complexRoot)
        {
            int iterationsCount = NewtonsMethod(ref complexRoot);
            int rootIndex = FindRootIndex(complexRoot);

            Color pixelColor = Colors[rootIndex % Colors.Length];
            pixelColor = Color.FromArgb(Math.Min(Math.Max(0, pixelColor.R - iterationsCount * 2), 255), Math.Min(Math.Max(0, pixelColor.G - iterationsCount * 2), 255), Math.Min(Math.Max(0, pixelColor.B - iterationsCount * 2), 255));

            return pixelColor;
        }

        private int FindRootIndex(ComplexNumber complexRoot)
        {
            bool known = false;
            int rootIndex = 0;
            for (int i = 0; i < Roots.Count; i++)
            {
                if (complexRoot.Subtract(Roots[i]).GetAbsoluteValue() <= SQUARE_ROOT_TOLERANCE)
                {
                    known = true;
                    rootIndex = i;
                }
            }
            if (!known)
            {
                Roots.Add(complexRoot);
                rootIndex = Roots.Count;
            }

            return rootIndex;
        }

        private int NewtonsMethod(ref ComplexNumber complexRoot)
        {
            int iterationsCount = 0;
            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                ComplexNumber difference = Polynomial.Eval(complexRoot).Divide(DerivedPolynomial.Eval(complexRoot));
                complexRoot = complexRoot.Subtract(difference);

                i = difference.GetAbsoluteValue() >= NEWTONS_METHOD_TOLERANCE ? i - 1 : i;

                iterationsCount++;
            }

            return iterationsCount;
        }

        private ComplexNumber FindWorldCoordinates(int i, int j)
        {
            double y = YAxisMinimum + i * YStep;
            double x = XAxisMinimum + j * XStep;

            return new ComplexNumber()
            {
                RealPart = x == 0 ? ALMOST_ZERO : x,
                ImaginaryPart = y == 0 ? ALMOST_ZERO : y
            };
        }

        private void SetPolynomials()
        {
            Polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            Polynomial.Coefficients.Add(ComplexNumber.Zero);
            Polynomial.Coefficients.Add(ComplexNumber.Zero);
            Polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            DerivedPolynomial = Polynomial.Derive();

            Console.WriteLine(Polynomial);
            Console.WriteLine(DerivedPolynomial);
        }
    }
}
