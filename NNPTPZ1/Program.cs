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
    partial class Program
    {
        static string fileName;
        static int[] bitmapDimensions;
        static double[] axisLimits;
        static Bitmap bitmap;
        static List<ComplexNumber> roots;

        static void Main(string[] args)
        {
            bitmapDimensions = new int[2];
            axisLimits = new double[4];
            ComplexNumber pixelWithCoordinates = new ComplexNumber();
            roots = new List<ComplexNumber>();
            //bitmap = new Bitmap(bitmapDimensions[0], bitmapDimensions[1]);

            for (int i = 0; i < bitmapDimensions.Length; i++)
            {
                bitmapDimensions[i] = int.Parse(args[i]);
            }

            for (int i = 0; i < axisLimits.Length; i++)
            {
                axisLimits[i] = double.Parse(args[i + 2]);
            }

            bitmap = new Bitmap(bitmapDimensions[0], bitmapDimensions[1]);
            fileName = args[6];
            // TODO: add parameters from args?


            //List<ComplexNumber> roots = new List<ComplexNumber>();

            // TODO: poly should be parameterised?
            Polynomial polynomial = new Polynomial();
            polynomial.ListOfComplexNumbers.Add(new ComplexNumber() { RealElement = 1 });
            polynomial.ListOfComplexNumbers.Add(ComplexNumber.Zero);
            polynomial.ListOfComplexNumbers.Add(ComplexNumber.Zero);
            //p.Coe.Add(Cplx.Zero);
            polynomial.ListOfComplexNumbers.Add(new ComplexNumber() { RealElement = 1 });
            //Polynomial ptmp = p;
            Polynomial derivedPolynomial = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(derivedPolynomial);

            //var colors = new Color[]
            //{
            //    Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            //};

            //var maxId = 0;

            // TODO: cleanup!!!
            // for every pixel in image...
            for (int i = 0; i < bitmapDimensions[0]; i++)
            {
                for (int j = 0; j < bitmapDimensions[1]; j++)
                {
                    // find "world" coordinates of pixel
                    pixelWithCoordinates = FindBitmapCoordinatesOfPixel(i, j);

                    //Console.WriteLine(ox);

                    // find solution of equation using newton's iteration
                    float it = 0;
                    for (int q = 0; q < 30; q++)
                    {
                        var diff = polynomial.Eval(pixelWithCoordinates).Divide(derivedPolynomial.Eval(pixelWithCoordinates));
                        pixelWithCoordinates = pixelWithCoordinates.Subtract(diff);

                        //Console.WriteLine($"{q} {ox} -({diff})");
                        if (Math.Pow(diff.RealElement, 2) + Math.Pow(diff.ImaginaryElement, 2) >= 0.5)
                        {
                            q--;
                        }
                        it++;
                    }

                    //Console.ReadKey();

                    // find solution root number
                    int rootNumber = getSolutionRootNumber(pixelWithCoordinates);

                    // colorize pixel according to root number
                    //int vv = id;
                    //int vv = id * 50 + (int)it*5;
                    //Color vv = CalculatePixelColorAccordingToRootNumber(colors, it, id);
                    //vv = Math.Min(Math.Max(0, vv), 255);
                    //bitmap.SetPixel(j, i, CalculatePixelColorAccordingToRootNumber(colors, it, rootNumber));
                    bitmap.SetPixel(j, i, CalculatePixelColorAccordingToRootNumber(it, rootNumber));
                    //bmp.SetPixel(j, i, Color.FromArgb(vv, vv, vv));
                }
            }

            // TODO: delete I suppose...
            //for (int i = 0; i < 300; i++)
            //{
            //    for (int j = 0; j < 300; j++)
            //    {
            //        Color c = bmp.GetPixel(j, i);
            //        int nv = (int)Math.Floor(c.R * (255.0 / maxid));
            //        bmp.SetPixel(j, i, Color.FromArgb(nv, nv, nv));
            //    }
            //}


            SaveIntoFile();
            //bitmap.Save(fileName ?? "../../../out.png");

            //Console.ReadKey();
        }

        private static ComplexNumber FindBitmapCoordinatesOfPixel(int i, int j)
        {

            double xmin = axisLimits[0];
            double xmax = axisLimits[1];
            double ymin = axisLimits[2];
            double ymax = axisLimits[3];

            double xstep = (xmax - xmin) / bitmapDimensions[0];
            double ystep = (ymax - ymin) / bitmapDimensions[1];

            ComplexNumber pixelWithCoordinates;

            double y = ymin + i * ystep;
            double x = xmin + j * xstep;

            pixelWithCoordinates = new ComplexNumber()
            {
                RealElement = x,
                ImaginaryElement = (y)
            };

            if (pixelWithCoordinates.RealElement == 0)
                pixelWithCoordinates.RealElement = 0.0001;
            if (pixelWithCoordinates.ImaginaryElement == 0)
                pixelWithCoordinates.ImaginaryElement = 0.0001f;

            return pixelWithCoordinates;
        }

        private static int getSolutionRootNumber(ComplexNumber pixelWithCoordinates)
        {
            var rootNumber = 0;
            var solutionFound = false;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(pixelWithCoordinates.RealElement - roots[i].RealElement, 2) + Math.Pow(pixelWithCoordinates.ImaginaryElement - roots[i].ImaginaryElement, 2) <= 0.01)
                {
                    solutionFound = true;
                    rootNumber = i;
                }
            }
            if (!solutionFound)
            {
                roots.Add(pixelWithCoordinates);
                rootNumber = roots.Count;
                //maxId = id + 1;
            }

            return rootNumber;
        }

        public static Color CalculatePixelColorAccordingToRootNumber(/*Color[] colors,*/ float it, int rootNumber)
        {
            Color pixelColor = Color.FromName(((Colors) (rootNumber % Enum.GetNames(typeof(Colors)).Length)).ToString());
            //Color pixelColor = colors[rootNumber % colors.Length];
            pixelColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
            pixelColor = Color.FromArgb(Math.Min(Math.Max(0, pixelColor.R - (int)it * 2), 255), Math.Min(Math.Max(0, pixelColor.G - (int)it * 2), 255), Math.Min(Math.Max(0, pixelColor.B - (int)it * 2), 255));
            return pixelColor;
        }

        public static void SaveIntoFile()
        {
            bitmap.Save(fileName ?? "../../../out.png");
        }
    }
}
