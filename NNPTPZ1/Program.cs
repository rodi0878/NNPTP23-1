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
        private static Color[] colorPalette = {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan,
            Color.Magenta
        };
        
        static void Main(string[] args)
        {
            InitializeScreenSize(args);
            InitializeCoordinates(LoadInputValues(args));

            ComputeNewtonFractal();

            bmp.Save(outputFileName ?? "../../../out.png");
        }

        private static Polygon CreateDefaultPolygonForCalculation()
        {
            Polygon polygon = new Polygon();
            polygon.Coefficients.Add(new ComplexNumber { RealPart = 1 });
            polygon.Coefficients.Add(ComplexNumber.Zero);
            polygon.Coefficients.Add(ComplexNumber.Zero);
            polygon.Coefficients.Add(new ComplexNumber { RealPart = 1 });
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

        private static void ComputeNewtonFractal()
        {
            Polygon polygon = CreateDefaultPolygonForCalculation();
            Polygon polygonDerived = polygon.Derive();

            Console.WriteLine(polygon);
            Console.WriteLine(polygonDerived);
            
            // for every pixel in image...
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    CalculatePixelColor(polygon, polygonDerived, x, y);
                }
            }
        }

        private static void CalculatePixelColor(Polygon polygon, Polygon polygonDerived, int x, int y)
        {
            ComplexNumber complexNumber = CreateComplexNumberByCoordinates(x, y);

            // find solution of equation using newton's iteration
            int iteration = 0;
            for (int i = 0; i < 30; i++)
            {
                var diff = polygon.Eval(complexNumber).Divide(polygonDerived.Eval(complexNumber));
                complexNumber = complexNumber.Subtract(diff);

                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                    i--;

                iteration++;
            }

            int id = FindSulutionRootNumber(complexNumber);

            ColorizePixel(id, iteration, x, y);
        }

        /// <summary>
        /// Find solution root number
        /// </summary>
        /// <param name="complexNumber"></param>
        /// <returns></returns>
        private static int FindSulutionRootNumber(ComplexNumber complexNumber)
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

        private static ComplexNumber CreateComplexNumberByCoordinates(int x, int y)
        {
            // find "world" coordinates of pixel
            double coordinateX = xmin + y * xstep;
            double coordinateY = ymin + x * ystep;

            ComplexNumber complexNumber = new ComplexNumber
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
        /// <param name="id"></param>
        /// <param name="iteration"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void ColorizePixel(int id, int iteration, int x, int y)
        {
            Color selectedColor = colorPalette[id % colorPalette.Length];
            Color adjustedSelectedColor = Color.FromArgb(
                Math.Min(Math.Max(0, selectedColor.R - iteration * 2), 255),
                Math.Min(Math.Max(0, selectedColor.G - iteration * 2), 255),
                Math.Min(Math.Max(0, selectedColor.B - iteration * 2), 255));
            
            bmp.SetPixel(x, y, adjustedSelectedColor);
        }
    }
}
