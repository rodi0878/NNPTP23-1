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
using System.Linq.Expressions;
using System.Threading;
using NNPTPZ1.Mathematics;
using System.Xml.Serialization;

namespace NNPTPZ1
{
    class Program
    {
        private static string fileName;
        private static int width, height;
        private static double xmin, xmax, ymin, ymax, xstep, ystep;
        private static Bitmap bitmap;
        static List<ComplexNumber> roots;

        static void Main(string[] args)
        {
            InitializeValues(args);
            xstep = (xmax - xmin) / width;
            ystep = (ymax - ymin) / height;


            roots = new List<ComplexNumber>();
            Polynome polynome = new Polynome();
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            Polynome polynomeDerived = polynome.Derive();

            Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };
            bitmap = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
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
                        var diff = polynome.Evaluate(ox).Divide(polynomeDerived.Evaluate(ox));
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
                    var vv = colors[id % colors.Length];
                    vv = Color.FromArgb(vv.R, vv.G, vv.B);
                    bitmap.SetPixel(j, i, vv);
                }
            }
            bitmap.Save(fileName ?? "../../../out.png");

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
        }
    }

}
