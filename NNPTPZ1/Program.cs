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
        private static int screenWidth;
        private static int screenHeight;
        
        private static double xmin;
        private static double xmax;
        private static double ymin;
        private static double ymax;
        
        private static double xstep;
        private static double ystep;

        private static string outputFileName;

        private static Bitmap bmp;
        private static List<ComplexNumber> roots = new List<ComplexNumber>();
        
        static void Main(string[] args)
        {
            InitializeScreenSize(args);
            InitializeCoordinates(LoadInputValues(args));

            Polygon polygon = CreateDefaultPolygonForCalculation();
            Polygon polygonDerived = polygon.Derive();

            Console.WriteLine(polygon);
            Console.WriteLine(polygonDerived);

            ComputeNewtonFractal(polygon, polygonDerived, roots);

            bmp.Save(outputFileName ?? "../../../out.png");
        }

        private static Polygon CreateDefaultPolygonForCalculation()
        {
            Polygon polygon = new Polygon();
            polygon.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polygon.Coefficients.Add(ComplexNumber.Zero);
            polygon.Coefficients.Add(ComplexNumber.Zero);
            polygon.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            return polygon;
        }

        private static void InitializeCoordinates(double[] doubleargs)
        {
            xmin = doubleargs[0];
            xmax = doubleargs[1];
            ymin = doubleargs[2];
            ymax = doubleargs[3];

            xstep = (xmax - xmin) / screenWidth;
            ystep = (ymax - ymin) / screenHeight;
        }

        private static double[] LoadInputValues(string[] args)
        {
            double[] doubleargs = new double[4];
            for (int i = 0; i < doubleargs.Length; i++)
            {
                doubleargs[i] = double.Parse(args[i + 2]);
            }

            outputFileName = args[6];
            return doubleargs;
        }

        private static void InitializeScreenSize(string[] args)
        {
            screenWidth = int.Parse(args[0]);
            screenHeight = int.Parse(args[1]);
            bmp = new Bitmap(screenWidth, screenHeight);
        }

        private static void ComputeNewtonFractal(Polygon p, Polygon pd, List<ComplexNumber> roots)
        {
            Color[] colorPalette = GetColorPalette();
            
            // for every pixel in image...
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    CalculatePixelColor(p, pd, roots, x, y, colorPalette);
                }
            }
        }

        private static void CalculatePixelColor(Polygon polygon, Polygon polygonDerived, List<ComplexNumber> roots, int i, int j, Color[] colorPalette)
        {
            ComplexNumber complexNumber = CreateComplexNumberByCoordinates(i, j);

            // find solution of equation using newton's iteration
            int iteration = 0;
            for (int q = 0; q < 30; q++)
            {
                var diff = polygon.Eval(complexNumber).Divide(polygonDerived.Eval(complexNumber));
                complexNumber = complexNumber.Subtract(diff);

                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                {
                    q--;
                }

                iteration++;
            }

            // find solution root number
            var known = false;
            var id = 0;
            for (int w = 0; w < roots.Count; w++)
            {
                if (Math.Pow(complexNumber.RealPart - roots[w].RealPart, 2) + Math.Pow(complexNumber.ImaginaryPart - roots[w].ImaginaryPart, 2) <=
                    0.01)
                {
                    known = true;
                    id = w;
                }
            }

            if (!known)
            {
                roots.Add(complexNumber);
                id = roots.Count;
            }

            ColorizePixel(colorPalette, id, iteration, j, i);
        }

        private static ComplexNumber CreateComplexNumberByCoordinates(int x, int y)
        {
            // find "world" coordinates of pixel
            double coordinateX = xmin + y * xstep;
            double coordinateY = ymin + x * ystep;

            ComplexNumber complexNumber = new ComplexNumber()
            {
                RealPart = coordinateX,
                ImaginaryPart = coordinateY
            };

            if (complexNumber.RealPart == 0)
                complexNumber.RealPart = 0.0001;

            if (complexNumber.ImaginaryPart == 0)
                complexNumber.ImaginaryPart = 0.0001;
            
            return complexNumber;
        }

        /// <summary>
        /// Colorize pixel according to root number
        /// </summary>
        /// <param name="colorPalette"></param>
        /// <param name="id"></param>
        /// <param name="iteration"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void ColorizePixel(Color[] colorPalette, int id, int iteration, int x, int y)
        {
            Color selectedColor = colorPalette[id % colorPalette.Length];
            Color adjustedSelectedColor = Color.FromArgb(
                Math.Min(Math.Max(0, selectedColor.R - iteration * 2), 255),
                Math.Min(Math.Max(0, selectedColor.G - iteration * 2), 255),
                Math.Min(Math.Max(0, selectedColor.B - iteration * 2), 255));
            
            bmp.SetPixel(x, y, adjustedSelectedColor);
        }

        private static Color[] GetColorPalette()
        {
            return new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan,
                Color.Magenta
            };
        }
    }
}
