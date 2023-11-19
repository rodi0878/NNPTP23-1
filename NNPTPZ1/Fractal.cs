using System;
using System.Collections.Generic;
using System.Drawing;

using Mathematics;

namespace App
{
    class Fractal
    {
        internal readonly int NUMBER_OF_ITERATIONS = 30;
        internal readonly double DIFFERENCE_TOLERANCE = 0.5;
        internal readonly double ROOT_TOLERANCE = 0.01;

        internal List<ComplexNumber> roots = new List<ComplexNumber>();
        internal Polynomial polynomial, derivatedPolynomial;

        internal Bitmap bitmap;
        internal double xmin, ymin, xstep, ystep;
        internal string outputPath;
        internal string[] args;

        public Fractal(string[] args)
        {
            this.args = args;
        }

        public void Create()
        {
            InitializeParameters();
            InitializePolynomials();
            RenderPixels();
            SaveResult();
        }

        internal void InitializeParameters()
        {
            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);

            bitmap = new Bitmap(width, height);

            xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            xstep = (xmax - xmin) / width;
            ystep = (ymax - ymin) / height;

            if (args.Length > 6)
            {
                outputPath = args[6];
            }
        }

        internal void InitializePolynomials()
        {
            polynomial = new Polynomial()
            {
                Coefficients = new List<ComplexNumber>(){
                    new ComplexNumber() { RealNumber = 1 },
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                    new ComplexNumber() { RealNumber = 1 }
                }
            };
            derivatedPolynomial = polynomial.Derive();
        }

        internal void RenderPixels()
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    RenderPixelAt(i, j);
                }
            }
        }

        internal void RenderPixelAt(int i, int j)
        {
            ComplexNumber currentComplexNumber = FindWorldCoordinationsOf(i, j);
            int iterations = FindSolutionByNewtonIteration(ref currentComplexNumber);
            int position = FindSolutionForRoots(currentComplexNumber);
            ColorizePixelAt(i, j, position, iterations);
        }

        internal ComplexNumber FindWorldCoordinationsOf(int i, int j)
        {
            double y = ymin + i * ystep;
            double x = xmin + j * xstep;

            return new ComplexNumber()
            {
                RealNumber = x,
                ImaginaryUnit = y
            };
        }

        internal int FindSolutionByNewtonIteration(ref ComplexNumber currentComplexNumber)
        {
            int iterations = 0;
            for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
            {
                ComplexNumber difference = polynomial.EvaluateAt(currentComplexNumber).Divide(derivatedPolynomial.EvaluateAt(currentComplexNumber));
                currentComplexNumber = currentComplexNumber.Subtract(difference);
                if (IsConverged(difference))
                {
                    i--;
                }
                iterations++;
            }
            return iterations;
        }

        internal bool IsConverged(ComplexNumber difference)
        {
            return Math.Pow(difference.RealNumber, 2) + Math.Pow(difference.ImaginaryUnit, 2) >= DIFFERENCE_TOLERANCE;
        }

        internal int FindSolutionForRoots(ComplexNumber currentComplexNumber)
        {
            int position = 0;
            bool knownRoot = false;
            for (int i = 0; i < roots.Count; i++)
            {
                if (IsRootInTolerance(currentComplexNumber, roots[i]))
                {
                    knownRoot = true;
                    position = i;
                    break;
                }
            }
            if (!knownRoot)
            {
                roots.Add(currentComplexNumber);
                position = roots.Count;
            }
            return position;
        }

        internal bool IsRootInTolerance(ComplexNumber currentComplexNumber, ComplexNumber root)
        {
            return Math.Pow(currentComplexNumber.RealNumber - root.RealNumber, 2) + Math.Pow(currentComplexNumber.ImaginaryUnit - root.ImaginaryUnit, 2) <= ROOT_TOLERANCE;
        }

        internal int CalculateColorPart(int colorPart, int iterations)
        {
            return Math.Min(Math.Max(0, colorPart - iterations * 2), 255);
        }

        internal void ColorizePixelAt(int i, int j, int position, int iterations)
        {
            Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta };
            Color color = colors[position % colors.Length];
            color = Color.FromArgb(CalculateColorPart(color.R, iterations), CalculateColorPart(color.G, iterations), CalculateColorPart(color.B, iterations));
            bitmap.SetPixel(j, i, color);
        }

        internal void SaveResult()
        {
            bitmap.Save(outputPath ?? "../../../out.png");
        }
    }
}