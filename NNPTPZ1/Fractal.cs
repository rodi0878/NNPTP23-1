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
        internal readonly Color[] COLORS = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta };
        internal int iterations = 0, position = 0;
        internal Polynomial polynomial, derivatedPolynomial;
        internal ComplexNumber complexNumber;
        internal List<ComplexNumber> roots = new List<ComplexNumber>();
        internal Bitmap bitmap;
        internal int width, height;
        internal double xmin, xmax, ymin, ymax, xstep, ystep;
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
            width = int.Parse(args[0]);
            height = int.Parse(args[1]);
            bitmap = new Bitmap(width, height);

            xmin = double.Parse(args[2]);
            xmax = double.Parse(args[3]);
            ymin = double.Parse(args[4]);
            ymax = double.Parse(args[5]);
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
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    FindWorldCoordinationsOf(i, j);
                    FindSolutionByNewtonIteration();
                    FindSolutionForRoots();
                    ColorizePixelAt(i, j);
                }
            }
        }

        internal void FindWorldCoordinationsOf(int i, int j)
        {
            double y = ymin + i * ystep;
            double x = xmin + j * xstep;

            complexNumber = new ComplexNumber()
            {
                RealNumber = x,
                ImaginaryUnit = y
            };
        }

        internal void FindSolutionByNewtonIteration()
        {
            iterations = 0;
            for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
            {
                ComplexNumber difference = polynomial.EvaluateAt(complexNumber).Divide(derivatedPolynomial.EvaluateAt(complexNumber));
                complexNumber = complexNumber.Subtract(difference);
                if (IsConverged(difference))
                {
                    i--;
                }
                iterations++;
            }
        }

        internal bool IsConverged(ComplexNumber difference)
        {
            return Math.Pow(difference.RealNumber, 2) + Math.Pow(difference.ImaginaryUnit, 2) >= DIFFERENCE_TOLERANCE;
        }

        internal void FindSolutionForRoots()
        {
            bool known = false;
            for (int i = 0; i < roots.Count; i++)
            {
                if (IsRootInTolerance(roots[i]))
                {
                    known = true;
                    position = i;
                    break;
                }
            }
            if (!known)
            {
                roots.Add(complexNumber);
                position = roots.Count;
            }
        }

        internal bool IsRootInTolerance(ComplexNumber root)
        {
            return Math.Pow(complexNumber.RealNumber - root.RealNumber, 2) + Math.Pow(complexNumber.ImaginaryUnit - root.ImaginaryUnit, 2) <= ROOT_TOLERANCE;
        }

        internal int CalculateColorPart(int colorPart)
        {
            return Math.Min(Math.Max(0, colorPart - iterations * 2), 255);
        }

        internal void ColorizePixelAt(int i, int j)
        {
            var color = COLORS[position % COLORS.Length];
            color = Color.FromArgb(CalculateColorPart(color.R), CalculateColorPart(color.G), CalculateColorPart(color.B));
            bitmap.SetPixel(j, i, color);
        }

        internal void SaveResult()
        {
            bitmap.Save(outputPath ?? "../../../out.png");
        }
    }
}