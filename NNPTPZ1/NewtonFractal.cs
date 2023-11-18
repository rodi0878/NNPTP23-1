using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    public class NewtonFractal
    {
        private const double CLOSE_TO_ZERO = 0.0001;
        private const int MAX_ITERATIONS = 30;
        private const double NEWTONS_METHOD_TOLERANCE = 0.5;
        private const double ROOT_TOLERANCE = 0.01;
        private const string DEFAULT_FILENAME = "../../../out.png";

        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
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

        public NewtonFractal(string[] arguments)
        {
            ImageWidth = int.Parse(arguments[0]);
            ImageHeight = int.Parse(arguments[1]);
            XMin = double.Parse(arguments[2]);
            XMax = double.Parse(arguments[3]);
            YMin = double.Parse(arguments[4]);
            YMax = double.Parse(arguments[5]);
            FileName = arguments.Length > 6 ? arguments[6] : DEFAULT_FILENAME;

            Image = new Bitmap(ImageWidth, ImageHeight);

            XStep = (XMax - XMin) / ImageWidth;
            YStep = (YMax - YMin) / ImageHeight;
        }

        public void CreateImage()
        {
            SetPolynomials();

            for (int i = 0; i < ImageWidth; i++)
            {
                for (int j = 0; j < ImageHeight; j++)
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
                if (complexRoot.Subtract(Roots[i]).GetAbsoluteValueSquare() <= ROOT_TOLERANCE)
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

                i = difference.GetAbsoluteValueSquare() >= NEWTONS_METHOD_TOLERANCE ? i - 1 : i;

                iterationsCount++;
            }

            return iterationsCount;
        }

        private ComplexNumber FindWorldCoordinates(int i, int j)
        {
            double y = YMin + i * YStep;
            double x = XMin + j * XStep;

            return new ComplexNumber()
            {
                RealPart = x == 0 ? CLOSE_TO_ZERO : x,
                ImaginaryPart = y == 0 ? CLOSE_TO_ZERO : y
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