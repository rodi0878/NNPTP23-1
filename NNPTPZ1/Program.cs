using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using System.Threading;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private const int ITERATION_LIMIT = 30;
        private static int[] intargs;
        private static double[] doubleargs;
        private static string output;
        private static Bitmap bitmap;
        private static double xmin, ymin, xstep, ystep;

        private static Polynomial polynomial;
        private static Polynomial polynomialDerive;

        private static readonly Color[] colours = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        static void Main(string[] args)
        {
            LoadArguments(args);
            Initialization();
            PrepareData();
            CreateImage();
            SaveImage();
        }
        
        private static void LoadArguments(string[] args)
        {
            intargs = new int[2];
            for (int i = 0; i < intargs.Length; i++)
            {
                intargs[i] = int.Parse(args[i]);
            }
            doubleargs = new double[4];
            for (int i = 0; i < doubleargs.Length; i++)
            {
                doubleargs[i] = double.Parse(args[i + 2]);
            }
            output = args[6];
        }

        private static void Initialization()
        {
            bitmap = new Bitmap(intargs[0], intargs[1]);
            xmin = doubleargs[0];
            double xmax = doubleargs[1];
            ymin = doubleargs[2];
            double ymax = doubleargs[3];

            xstep = (xmax - xmin) / intargs[0];
            ystep = (ymax - ymin) / intargs[1];
        }

        private static void PrepareData()
        {
            polynomial = new Polynomial();
            polynomial.ComplexNumbersList.Add(new ComplexNumber() { Real = 1 });
            polynomial.ComplexNumbersList.Add(ComplexNumber.Zero);
            polynomial.ComplexNumbersList.Add(ComplexNumber.Zero);
            polynomial.ComplexNumbersList.Add(new ComplexNumber() { Real = 1 });
            polynomialDerive = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomialDerive);
        }

        private static void CreateImage()
        {
            // for every pixel in image...
            List<ComplexNumber> listOfRoots = new List<ComplexNumber>();
            for (int y = 0; y < intargs[0]; y++)
            {
                for (int x = 0; x < intargs[1]; x++)
                {

                    ComplexNumber number = CreateComplexNumber(y, x);

                    int iteration = FindSolutionByNewtonIteration(ref number);

                    int rootNumber = FindRootNumber(listOfRoots, number);

                    ColourizePixelByRootNumber(y, x, iteration, rootNumber);

                }
            }
        }

        private static int FindRootNumber(List<ComplexNumber> listOfRoots, ComplexNumber number)
        {
            int colourId = 0;
            var known = false;
            for (int i = 0; i < listOfRoots.Count; i++)
            {
                if (Math.Pow(number.Real - listOfRoots[i].Real, 2) + Math.Pow(number.Imaginary - listOfRoots[i].Imaginary, 2) <= 0.01)
                {
                    known = true;
                    colourId = i;
                }
            }
            if (!known)
            {
                listOfRoots.Add(number);
                colourId = listOfRoots.Count;
            }

            return colourId;
        }

        private static int FindSolutionByNewtonIteration(ref ComplexNumber number)
        {
            int iteration = 0;
            for (int i = 0; i < ITERATION_LIMIT; i++)
            {
                var difference = polynomial.Evaluate(number).Divide(polynomialDerive.Evaluate(number));
                number = number.Subtract(difference);

                if (Math.Pow(difference.Real, 2) + Math.Pow(difference.Imaginary, 2) >= 0.5)
                {
                    i--;
                }
                iteration++;
            }
            return iteration;
        }

        private static ComplexNumber CreateComplexNumber(int inputY, int inputX)
        {

            double y = ymin + inputY * ystep;
            double x = xmin + inputX * xstep;

            ComplexNumber number = new ComplexNumber()
            {
                Real = x,
                Imaginary = (float)(y)
            };

            if (number.Real == 0)
                number.Real = 0.0001;
            if (number.Imaginary == 0)
                number.Imaginary = 0.0001f;

            return number;
        }

        private static void ColourizePixelByRootNumber(int y, int x, int iteration, int rootNumber)
        {
            Color colour = colours[rootNumber % colours.Length];
            colour = Color.FromArgb(colour.R, colour.G, colour.B);
            colour = Color.FromArgb(Math.Min(Math.Max(0, colour.R - iteration * 2), 255), Math.Min(Math.Max(0, colour.G - iteration * 2), 255), Math.Min(Math.Max(0, colour.B - iteration * 2), 255));
            bitmap.SetPixel(x, y, colour);
        }

        private static void SaveImage()
        {
            bitmap.Save(output ?? "../../../out.png");
        }
    }
}
