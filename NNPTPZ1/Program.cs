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
        static void Main(string[] args)
        {
            if(args.Length != 7)
            {
                Console.WriteLine("Requested 7 arguments");
                return;
            }
            
            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);
            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            string fileName = args[6];

            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            List<ComplexNumber> roots = new List<ComplexNumber>();
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber(1,0));
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber(1,0));
            Polynomial polynomialDerivative = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomialDerivative);

            Bitmap fractalImage = new Bitmap(width, height);

            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;

                    ComplexNumber ox = new ComplexNumber(x, (float)y);

                    if (ox.Real == 0)
                        ox.Real = 0.0001;
                    if (ox.Imaginary == 0)
                        ox.Imaginary = 0.0001f;

                    float it = 0;
                    for (int k = 0; k< 30; k++)
                    {
                        var diff = polynomial.Eval(ox).Divide(polynomialDerivative.Eval(ox));
                        ox = ox.Subtract(diff);

                        if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= 0.5)
                        {
                            k--;
                        }
                        it++;
                    }

                    var known = false;
                    var id = 0;

                    for (int w = 0; w <roots.Count;w++)
                    {
                        if (Math.Pow(ox.Real- roots[w].Real, 2) + Math.Pow(ox.Imaginary - roots[w].Imaginary, 2) <= 0.01)
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

                    var colorIndex = id % colors.Length;
                    var color = colors[colorIndex];
                    var alpha = Math.Min(Math.Max(0, (int)it * 5), 255);
                    var finalColor = Color.FromArgb(alpha, color);
                    Console.WriteLine(j + " " + i + " " + color.Name);
                    fractalImage.SetPixel(i, j, finalColor);
                }
            }
                    fractalImage.Save(fileName ?? "../../../out.png");
        }
    }
}
