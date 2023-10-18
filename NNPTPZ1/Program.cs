using System;
using System.Collections.Generic;
using System.Drawing;
using Mathematics;

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
            int[] intargs = new int[2];
            for (int i = 0; i < intargs.Length; i++)
            {
                intargs[i] = int.Parse(args[i]);
            }
            double[] doubleargs = new double[4];
            for (int i = 0; i < doubleargs.Length; i++)
            {
                doubleargs[i] = double.Parse(args[i + 2]);
            }
            string outputFile = args[6];
            // TODO: add parameters from args?
            Bitmap bitmap = new Bitmap(intargs[0], intargs[1]);
            double xmin = doubleargs[0];
            double xmax = doubleargs[1];
            double ymin = doubleargs[2];
            double ymax = doubleargs[3];

            double xstep = (xmax - xmin) / intargs[0];
            double ystep = (ymax - ymin) / intargs[1];

            List<ComplexNumber> roots = new List<ComplexNumber>();
            // TODO: poly should be parameterised?
            Polynome polynome = new Polynome();
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            Polynome derivatedPolynome = polynome.DerivatePolynome();

            Console.WriteLine(polynome);
            Console.WriteLine(derivatedPolynome);

            Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            // TODO: cleanup!!!
            // for every pixel in image...
            for (int i = 0; i < intargs[0]; i++)
            {
                for (int j = 0; j < intargs[1]; j++)
                {
                    // find "world" coordinates of pixel
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;

                    ComplexNumber complexNumber = new ComplexNumber()
                    {
                        RealPart = x,
                        ImaginariPart = (float)(y)
                    };

                    if (complexNumber.RealPart == 0)
                        complexNumber.RealPart = 0.0001;
                    if (complexNumber.ImaginariPart == 0)
                        complexNumber.ImaginariPart = 0.0001f;

                    // find solution of equation using newton's iteration
                    float it = 0;
                    for (int k = 0; k < 30; k++)
                    {
                        ComplexNumber polynomialDifference = polynome.EvaluatePolynome(complexNumber).Divide(derivatedPolynome.EvaluatePolynome(complexNumber));
                        complexNumber = complexNumber.Subtract(polynomialDifference);

                        if (Math.Pow(polynomialDifference.RealPart, 2) + Math.Pow(polynomialDifference.ImaginariPart, 2) >= 0.5)
                        {
                            k--;
                        }
                        it++;
                    }

                    // find solution root number
                    bool known = false;
                    int id = 0;
                    for (int l = 0; l < roots.Count; l++)
                    {
                        if (Math.Pow(complexNumber.RealPart - roots[l].RealPart, 2) + Math.Pow(complexNumber.ImaginariPart - roots[l].ImaginariPart, 2) <= 0.01)
                        {
                            known = true;
                            id = l;
                        }
                    }
                    if (!known)
                    {
                        roots.Add(complexNumber);
                        id = roots.Count;
                    }

                    Color pixelColor = colors[id % colors.Length];
                    pixelColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
                    pixelColor = Color.FromArgb(Math.Min(Math.Max(0, pixelColor.R - (int)it * 2), 255), Math.Min(Math.Max(0, pixelColor.G - (int)it * 2), 255), Math.Min(Math.Max(0, pixelColor.B - (int)it * 2), 255));
                    bitmap.SetPixel(j, i, pixelColor);
                }
            }
            bitmap.Save(outputFile ?? "../../../out.png");
        }
    }
}
