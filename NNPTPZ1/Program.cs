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
            int[] intArgs = new int[2];
            for (int i = 0; i < intArgs.Length; i++)
            {
                intArgs[i] = int.Parse(args[i]);
            }
            double[] doubleArgs = new double[4];
            for (int i = 0; i < doubleArgs.Length; i++)
            {
                doubleArgs[i] = double.Parse(args[i + 2]);
            }
            string output = args[6];
            Bitmap bitmap = new Bitmap(intArgs[0], intArgs[1]);
            double xMin = doubleArgs[0];
            double xMax = doubleArgs[1];
            double yMin = doubleArgs[2];
            double yMax = doubleArgs[3];

            double xStep = (xMax - xMin) / intArgs[0];
            double yStep = (yMax - yMin) / intArgs[1];

            List<ComplexNumber> roots = new List<ComplexNumber>();
            Polynomial poly = new Polynomial();
            poly.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            poly.Coefficients.Add(ComplexNumber.Zero);
            poly.Coefficients.Add(ComplexNumber.Zero);
            //poly.Coefficients.Add(ComplexNumber.Zero);
            poly.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            Polynomial derivedPoly = poly.Derive();

            Console.WriteLine(poly);
            Console.WriteLine(derivedPoly);

            var colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            var maxId = 0;

            for (int i = 0; i < intArgs[0]; i++)
            {
                for (int j = 0; j < intArgs[1]; j++)
                {
                    // find "world" coordinates of pixel
                    double y = yMin + i * yStep;
                    double x = xMin + j * xStep;

                    ComplexNumber ox = new ComplexNumber()
                    {
                        RealPart = x,
                        ImaginaryPart = y
                    };

                    if (ox.RealPart == 0)
                        ox.RealPart = 0.0001;
                    if (ox.ImaginaryPart == 0)
                        ox.ImaginaryPart = 0.0001f;

                    //Console.WriteLine(ox);

                    // find solution of equation using newton'startingComplexNumber iteration
                    float it = 0;
                    for (int q = 0; q < 30; q++)
                    {
                        var diff = poly.Eval(ox).Divide(derivedPoly.Eval(ox));
                        ox = ox.Subtract(diff);

                        //Console.WriteLine($"{coefficientNumber} {ox} -({diff})");
                        if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                        {
                            q--;
                        }
                        it++;
                    }

                    //Console.ReadKey();

                    // find solution root number
                    var known = false;
                    var id = 0;
                    for (int w = 0; w <roots.Count;w++)
                    {
                        if (Math.Pow(ox.RealPart- roots[w].RealPart, 2) + Math.Pow(ox.ImaginaryPart - roots[w].ImaginaryPart, 2) <= 0.01)
                        {
                            known = true;
                            id = w;
                        }
                    }
                    if (!known)
                    {
                        roots.Add(ox);
                        id = roots.Count;
                        maxId = id + 1; 
                    }

                    // colorize pixel according to root number
                    //int selectedColor = id;
                    //int selectedColor = id * 50 + (int)it*5;
                    var selectedColor = colors[id % colors.Length];
                    selectedColor = Color.FromArgb(selectedColor.R, selectedColor.G, selectedColor.B);
                    selectedColor = Color.FromArgb(Math.Min(Math.Max(0, selectedColor.R - (int)it * 2), 255),
                                                   Math.Min(Math.Max(0, selectedColor.G - (int)it * 2), 255),
                                                   Math.Min(Math.Max(0, selectedColor.B - (int)it * 2), 255));
                    //selectedColor = Math.Min(Math.Max(0, selectedColor), 255);
                    bitmap.SetPixel(j, i, selectedColor);
                    //bitmap.SetPixel(j, i, Color.FromArgb(selectedColor, selectedColor, selectedColor));
                }
            }

            // TODO: delete I suppose...
            //for (int i = 0; i < 300; i++)
            //{
            //    for (int j = 0; j < 300; j++)
            //    {
            //        Color c = bitmap.GetPixel(j, i);
            //        int nv = (int)Math.Floor(c.R * (255.0 / maxId));
            //        bitmap.SetPixel(j, i, Color.FromArgb(nv, nv, nv));
            //    }
            //}

                    bitmap.Save(output ?? "../../../out.png");
            //Console.ReadKey();
        }
    }
}
