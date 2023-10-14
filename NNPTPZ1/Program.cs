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
        static void Main(string[] args)
        {
            int screenWidth = int.Parse(args[0]);
            int screenHeight = int.Parse(args[1]);

            double[] doubleargs = new double[4];
            for (int i = 0; i < doubleargs.Length; i++)
            {
                doubleargs[i] = double.Parse(args[i + 2]);
            }
            string outputFileName = args[6];
            // TODO: add parameters from args?
            Bitmap bmp = new Bitmap(screenWidth, screenHeight);
            double xmin = doubleargs[0];
            double xmax = doubleargs[1];
            double ymin = doubleargs[2];
            double ymax = doubleargs[3];

            double xstep = (xmax - xmin) / screenWidth;
            double ystep = (ymax - ymin) / screenHeight;

            List<ComplexNumber> roots = new List<ComplexNumber>();
            // TODO: poly should be parameterised?
            Polygon p = new Polygon();
            p.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            Polygon pd = p.Derive();

            Console.WriteLine(p);
            Console.WriteLine(pd);

            var colorPalette = GetColorPalette();

            ComputeNewtonFractal(screenHeight, screenWidth, ymin, ystep, xmin, xstep, p, pd, roots, colorPalette, bmp);

            bmp.Save(outputFileName ?? "../../../out.png");
        }

        private static void ComputeNewtonFractal(int screenHeight, int screenWidth, double ymin, double ystep, double xmin,
            double xstep, Polygon p, Polygon pd, List<ComplexNumber> roots, Color[] colorPalette, Bitmap bmp)
        {
            // for every pixel in image...
            for (int i = 0; i < screenHeight; i++)
            {
                for (int j = 0; j < screenWidth; j++)
                {
                    // find "world" coordinates of pixel
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;

                    ComplexNumber ox = new ComplexNumber()
                    {
                        RealPart = x,
                        ImaginaryPart = (float)(y)
                    };

                    if (ox.RealPart == 0)
                        ox.RealPart = 0.0001;

                    if (ox.ImaginaryPart == 0)
                        ox.ImaginaryPart = 0.0001f;

                    // find solution of equation using newton's iteration
                    float it = 0;
                    for (int q = 0; q < 30; q++)
                    {
                        var diff = p.Eval(ox).Divide(pd.Eval(ox));
                        ox = ox.Subtract(diff);

                        if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                        {
                            q--;
                        }

                        it++;
                    }

                    // find solution root number
                    var known = false;
                    var id = 0;
                    for (int w = 0; w < roots.Count; w++)
                    {
                        if (Math.Pow(ox.RealPart - roots[w].RealPart, 2) + Math.Pow(ox.ImaginaryPart - roots[w].ImaginaryPart, 2) <= 0.01)
                        {
                            known = true;
                            id = w;
                        }
                    }

                    if (!known)
                    {
                        roots.Add(ox);
                        id = roots.Count;
                    }

                    // colorize pixel according to root number
                    var vv = colorPalette[id % colorPalette.Length];
                    vv = Color.FromArgb(vv.R, vv.G, vv.B);
                    vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R - (int)it * 2), 255),
                        Math.Min(Math.Max(0, vv.G - (int)it * 2), 255), Math.Min(Math.Max(0, vv.B - (int)it * 2), 255));
                    bmp.SetPixel(j, i, vv);
                }
            }
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
