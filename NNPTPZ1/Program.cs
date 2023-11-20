using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    class Program
    {
        private static string fileName;
        private static int width, height;
        private static double xmin, xmax, ymin, ymax, xstep, ystep;
        private static Bitmap bitmap;
        private static List<ComplexNumber> roots;
        private static Polynome polynome, polynomeDerived;

        private static readonly Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        private const int ITERATION_COUNT = 30;

        static void Main(string[] args)
        {
            InitializeValues(args);

            InitializePolynomes();

            GenerateFractal();

            SaveBitmapFile();
        }

        private static void GenerateFractal()
        {
            bitmap = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    ComplexNumber complexNumberIterationPoint = FindCoordinatesOfPixel(i, j);

                    complexNumberIterationPoint = FindSolutionOfEquation(complexNumberIterationPoint);

                    int identificator = FindSolutionRootNumber(complexNumberIterationPoint);

                    ColorizePixel(i, j, identificator);
                }
            }
        }

        private static ComplexNumber FindCoordinatesOfPixel(int i, int j)
        {
            double y = ymin + i * ystep;
            double x = xmin + j * xstep;

            ComplexNumber complexNumberIterationPoint = new ComplexNumber()
            {
                RealPart = x,
                ImaginaryPart = y
            };

            if (complexNumberIterationPoint.RealPart == 0)
                complexNumberIterationPoint.RealPart = 0.0001;
            if (complexNumberIterationPoint.ImaginaryPart == 0)
                complexNumberIterationPoint.ImaginaryPart = 0.0001;
            return complexNumberIterationPoint;
        }

        private static ComplexNumber FindSolutionOfEquation(ComplexNumber complexNumber)
        {
            for (int i = 0; i < ITERATION_COUNT; i++)
            {
                ComplexNumber diff = polynome.Evaluate(complexNumber).Divide(polynomeDerived.Evaluate(complexNumber));
                complexNumber = complexNumber.Subtract(diff);

                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                {
                    i--;
                }
            }
            return complexNumber;
        }

        private static void SaveBitmapFile()
        {
            bitmap.Save(fileName ?? "../../../out.png");
        }

        private static int FindSolutionRootNumber(ComplexNumber complexNumber)
        {
            bool known = false;
            int identificator = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(complexNumber.RealPart - roots[i].RealPart, 2) + 
                    Math.Pow(complexNumber.ImaginaryPart - roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    known = true;
                    identificator = i;
                }
            }
            if (!known)
            {
                roots.Add(complexNumber);
                identificator = roots.Count;
            }
            return identificator;
        }

        private static void ColorizePixel(int i, int j, int id)
        {
            Color color = colors[id % colors.Length];
            color = Color.FromArgb(color.R, color.G, color.B);
            bitmap.SetPixel(j, i, color);
        }

        private static void InitializePolynomes()
        {
            polynome = new Polynome();
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynomeDerived = polynome.Derive();
        }

        private static void InitializeValues(string[] args)
        {
            width = int.Parse(args[0]);
            height = int.Parse(args[1]);
            xmin = double.Parse(args[2]);
            xmax = double.Parse(args[3]);
            ymin = double.Parse(args[4]);
            ymax = double.Parse(args[5]);
            fileName = args[6];
            xstep = (xmax - xmin) / width;
            ystep = (ymax - ymin) / height;
            roots = new List<ComplexNumber>();
        }
    }

}
