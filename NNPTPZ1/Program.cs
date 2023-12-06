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
        private const int MAX_ITERATIONS = 30;

        private static int height, width;
        private static double xmax, xmin, xstep, ymax, ymin, ystep;
        private static string fileName;
        private static Polynome polynome, derivedPolynome;
        private static Bitmap bitmap;
        
        private static List<ComplexNumber> roots;
        private static Color[] colors;
        static void Main(string[] args)
        {
            Initialize(args);
            InitializePolynomes();
            CreateBitmap();
            SaveBitmap();
        }

        private static void SaveBitmap()
        {
            bitmap.Save(fileName ?? "../../../out.png");
        }

        private static void CreateBitmap()
        {
            bitmap = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    ComplexNumber coordinatesComplexNumber = FindPixelCoordinates(i, j);
                    int iteration = NewtonIteration(ref coordinatesComplexNumber);
                    int id = FindRootNumber(coordinatesComplexNumber);
                    ColorizePixel(i, j, id, iteration);
                }
            }
        }

        private static void ColorizePixel(int i, int j, int id, int iteration)
        {
            Color color = colors[id % colors.Length];
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - iteration * 2), 255), Math.Min(Math.Max(0, color.G - iteration * 2), 255), Math.Min(Math.Max(0, color.B - iteration * 2), 255));
            bitmap.SetPixel(j, i, color);
        }

        private static int FindRootNumber(ComplexNumber complexNumber)
        {
            bool known = false;
            int id = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(complexNumber.RealPart - roots[i].RealPart, 2) 
                    + Math.Pow(complexNumber.ImaginaryPart - roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    known = true;
                    id = i;
                }
            }
            if (!known)
            {
                roots.Add(complexNumber);
                id = roots.Count;
            }

            return id;
        }

        private static int NewtonIteration(ref ComplexNumber complexNumber)
        {
            int iteration = 0;
            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                ComplexNumber correction = polynome.Evaluate(complexNumber).Divide(derivedPolynome.Evaluate(complexNumber));
                complexNumber = complexNumber.Subtract(correction);

                if (Math.Pow(correction.RealPart, 2) + Math.Pow(correction.ImaginaryPart, 2) >= 0.5)
                {
                    i--;
                }
                iteration++;
            }

            return iteration;
        }

        private static ComplexNumber FindPixelCoordinates(int i, int j)
        {
            double x = xmin + j * xstep;
            double y = ymin + i * ystep;

            ComplexNumber complexNumber = new ComplexNumber()
            {
                RealPart = x,
                ImaginaryPart = y
            };

            if (complexNumber.RealPart == 0)
            {
                complexNumber.RealPart = 0.0001;
            }
                
            if (complexNumber.ImaginaryPart == 0)
            {
                complexNumber.ImaginaryPart = 0.0001f;
            }
                
            return complexNumber;
        }

        private static void InitializePolynomes()
        {
            polynome = new Polynome();
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            derivedPolynome  = polynome.Derive();
        }

        private static void Initialize(string[] args)
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
            colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };
        }
    }

   
}
