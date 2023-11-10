using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    public class FractalGenerator
    {
        private int[] intArguments = new int[2];
        private double[] doubleArguments = new double[4];
        private Bitmap bitmapImage;
        private string output;
        private double minX;
        private double maxX;
        private double minY;
        private double maxY;
        private double xStep;
        private double yStep;
        private List<ComplexNumber> roots = new List<ComplexNumber>();
        private Parser parser;
        private Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta };

        public FractalGenerator(string[] args)
        {
            parser = new Parser(args);
            this.intArguments = parser.ParseIntArguments();
            this.doubleArguments = parser.ParseDoubleArguments();
            output = args[6];

            Initialize();
        }

        private void Initialize()
        {
            bitmapImage = new Bitmap(intArguments[0], intArguments[1]);
            minX = doubleArguments[0];
            maxX = doubleArguments[1];
            minY = doubleArguments[2];
            maxY = doubleArguments[3];

            xStep = (maxX - minX) / intArguments[0];
            yStep = (maxY - minY) / intArguments[1];
        }

        public void GenerateFractal()
        {
            Polynomial polynomial = CreatePolynomial();
            Polynomial polynomialDerivative = polynomial.Derivative();

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomialDerivative);

            CalculateAndColorizePixels(polynomial, polynomialDerivative);
        }

        private Polynomial CreatePolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            return polynomial;
        }

        private void CalculateAndColorizePixels(Polynomial polynomial, Polynomial polynomialDerivative)
        {
            var computedRoots = new Dictionary<ComplexNumber, int>();

            for (int i = 0; i < intArguments[0]; i++)
            {
                for (int j = 0; j < intArguments[1]; j++)
                {
                    ComplexNumber currentComplex = CalculateWorldCoordinates(i, j);
                    float iterations = NewtonIteration(polynomial, polynomialDerivative, currentComplex);

                    int rootId = FindRootNumber(currentComplex);

                    Color color = ColorizePixel(rootId, (int)iterations);
                    SetPixelColor(j, i, color);
                }
            }
        }

        private ComplexNumber CalculateWorldCoordinates(int i, int j)
        {
            double currentY = minY + i * yStep;
            double currentX = minX + j * xStep;
            return new ComplexNumber()
            {
                Real = currentX,
                Imaginary = (float)currentY
            };
        }

        private float NewtonIteration(Polynomial polynomial, Polynomial polynomialDerivative, ComplexNumber currentComplex)
        {
            float iterations = 0;
            for (int q = 0; q < 30; q++)
            {
                var difference = polynomial.Evaluate(currentComplex).Divide(polynomialDerivative.Evaluate(currentComplex));
                currentComplex = currentComplex.Subtract(difference);
                if (IsConverged(difference))
                {
                    q--;
                }
                iterations++;
            }
            return iterations;
        }

        static bool IsConverged(ComplexNumber difference)
        {
            return Math.Pow(difference.Real, 2) + Math.Pow(difference.Imaginary, 2) >= 0.5;
        }

        private int FindRootNumber(ComplexNumber currentComplex)
        {
            Boolean isKnownRoot = false;
            int rootId = 0;
            for (int w = 0; w < roots.Count; w++)
            {
                if (IsCloseToRoot(currentComplex, roots[w]))
                {
                    isKnownRoot = true;
                    rootId = w;
                }
            }
            if (!isKnownRoot)
            {
                roots.Add(currentComplex);
                rootId = roots.Count;
            }
            return rootId;
        }

        private bool IsCloseToRoot(ComplexNumber currentComplex, ComplexNumber root)
        {
            return Math.Pow(currentComplex.Real - root.Real, 2) + Math.Pow(currentComplex.Imaginary - root.Imaginary, 2) <= 0.01;
        }

        private Color ColorizePixel(int id, int iterations)
        {
            var color = colors[id % colors.Length];
            color = Color.FromArgb(color.R, color.G, color.B);
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - iterations * 2), 255), Math.Min(Math.Max(0, color.G - iterations * 2), 255), Math.Min(Math.Max(0, color.B - iterations * 2), 255));
            return color;
        }

        private void SetPixelColor(int i, int j, Color color)
        {
            bitmapImage.SetPixel(j, i, color);
        }

 
        public void SaveImage()
        {
            bitmapImage.Save(output ?? "../../../out.png");
        }
    }
}
