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
        private static int[] intArguments = new int[2];
        private static double[] doubleArguments = new double[4];
        private static string output;

        private static Bitmap bitmapImage;
        private static double minX;
        private static double maxX;
        private static double minY;
        private static double maxY;

        private static double xStep;
        private static double yStep;

        private static List<Cplx> roots = new List<Cplx>();

        private static Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        static void Main(string[] args)
        {
            Initialization(args);

            roots = new List<Cplx>();
            // TODO: poly should be parameterised?
            Poly polynomial = new Poly();
            polynomial.Coe.Add(new Cplx() { Re = 1 });
            polynomial.Coe.Add(Cplx.Zero);
            polynomial.Coe.Add(Cplx.Zero);
            //polynomial.Coe.Add(Cplx.Zero);
            polynomial.Coe.Add(new Cplx() { Re = 1 });
            Poly polynomialDerivative = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomialDerivative);

            var maxRootId = 0;

            // TODO: cleanup!!!
            // for every pixel in image...
            for (int i = 0; i < intArguments[0]; i++)
            {
                for (int j = 0; j < intArguments[1]; j++)
                {
                    // find "world" coordinates of pixel
                    double currentY = minY + i * yStep;
                    double currentX = minX + j * xStep;

                    Cplx currentComplex = new Cplx()
                    {
                        Re = currentX,
                        Imaginari = (float)(currentY)
                    };

                    if (currentComplex.Re == 0)
                        currentComplex.Re = 0.0001;
                    if (currentComplex.Imaginari == 0)
                        currentComplex.Imaginari = 0.0001f;

                    //Console.WriteLine(currentComplex);

                    // find solution of equation using newton's iteration
                    float iterations = 0;
                    for (int q = 0; q< 30; q++)
                    {
                        var difference = polynomial.Eval(currentComplex).Divide(polynomialDerivative.Eval(currentComplex));
                        currentComplex = currentComplex.Subtract(difference);

                        //Console.WriteLine($"{q} {ox} -({diff})");
                        if (Math.Pow(difference.Re, 2) + Math.Pow(difference.Imaginari, 2) >= 0.5)
                        {
                            q--;
                        }
                        iterations++;
                    }

                    //Console.ReadKey();

                    // find solution root number
                    var isKnownRoot = false;
                    var id = 0;
                    for (int w = 0; w <roots.Count;w++)
                    {
                        if (Math.Pow(currentComplex.Re- roots[w].Re, 2) + Math.Pow(currentComplex.Imaginari - roots[w].Imaginari, 2) <= 0.01)
                        {
                            isKnownRoot = true;
                            id = w;
                        }
                    }
                    if (!isKnownRoot)
                    {
                        roots.Add(currentComplex);
                        id = roots.Count;
                        maxRootId = id + 1; 
                    }

                    // colorize pixel according to root number
                    //int color = id;
                    //int color = id * 50 + (int)it*5;
                    var color = colors[id % colors.Length];
                    color = Color.FromArgb(color.R, color.G, color.B);
                    color = Color.FromArgb(Math.Min(Math.Max(0, color.R-(int)iterations*2), 255), Math.Min(Math.Max(0, color.G - (int)iterations*2), 255), Math.Min(Math.Max(0, color.B - (int)iterations*2), 255));
                    //color = Math.Min(Math.Max(0, color), 255);
                    bitmapImage.SetPixel(j, i, color);
                    //bmp.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }

            // TODO: delete I suppose...
            //for (int i = 0; i < 300; i++)
            //{
            //    for (int j = 0; j < 300; j++)
            //    {
            //        Color color = bitmapImage.GetPixel(j, i);
            //        int normalizedValue = (int)Math.Floor(color.R * (255.0 / maxRootId));
            //        bitmapImage.SetPixel(j, i, Color.FromArgb(normalizedValue, nvnormalizedValue, normalizedValue));
            //    }
            //}

            bitmapImage.Save(output ?? "../../../out.png");
            //Console.ReadKey();
        }

        static void Initialization(string[] args)
        {
            for (int i = 0; i < intArguments.Length; i++)
            {
                intArguments[i] = int.Parse(args[i]);
            }

            for (int i = 0; i < doubleArguments.Length; i++)
            {
                doubleArguments[i] = double.Parse(args[i + 2]);
            }
            output = args[6];

            // TODO: add parameters from args?
            bitmapImage = new Bitmap(intArguments[0], intArguments[1]);
            minX = doubleArguments[0];
            maxX = doubleArguments[1];
            minY = doubleArguments[2];
            maxY = doubleArguments[3];

            xStep = (maxX - minX) / intArguments[0];
            yStep = (maxY - minY) / intArguments[1];
        }
    }

    namespace Mathematics
    {
        public class Poly
        {
            /// <summary>
            /// Coe
            /// </summary>
            public List<Cplx> Coe { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            public Poly() => Coe = new List<Cplx>();

            public void Add(Cplx coe) =>
                Coe.Add(coe);

            /// <summary>
            /// Derives this polynomial and creates new one
            /// </summary>
            /// <returns>Derivated polynomial</returns>
            public Poly Derive()
            {
                Poly p = new Poly();
                for (int q = 1; q < Coe.Count; q++)
                {
                    p.Coe.Add(Coe[q].Multiply(new Cplx() { Re = q }));
                }

                return p;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public Cplx Eval(double x)
            {
                var y = Eval(new Cplx() { Re = x, Imaginari = 0 });
                return y;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public Cplx Eval(Cplx x)
            {
                Cplx s = Cplx.Zero;
                for (int i = 0; i < Coe.Count; i++)
                {
                    Cplx coef = Coe[i];
                    Cplx bx = x;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            bx = bx.Multiply(x);

                        coef = coef.Multiply(bx);
                    }

                    s = s.Add(coef);
                }

                return s;
            }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns>String repr of polynomial</returns>
            public override string ToString()
            {
                string s = "";
                int i = 0;
                for (; i < Coe.Count; i++)
                {
                    s += Coe[i];
                    if (i > 0)
                    {
                        int j = 0;
                        for (; j < i; j++)
                        {
                            s += "x";
                        }
                    }
                    if (i+1<Coe.Count)
                    s += " + ";
                }
                return s;
            }
        }

        public class Cplx
        {
            public double Re { get; set; }
            public float Imaginari { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is Cplx)
                {
                    Cplx x = obj as Cplx;
                    return x.Re == Re && x.Imaginari == Imaginari;
                }
                return base.Equals(obj);
            }

            public readonly static Cplx Zero = new Cplx()
            {
                Re = 0,
                Imaginari = 0
            };

            public Cplx Multiply(Cplx b)
            {
                Cplx a = this;
                // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
                return new Cplx()
                {
                    Re = a.Re * b.Re - a.Imaginari * b.Imaginari,
                    Imaginari = (float)(a.Re * b.Imaginari + a.Imaginari * b.Re)
                };
            }
            public double GetAbS()
            {
                return Math.Sqrt( Re * Re + Imaginari * Imaginari);
            }

            public Cplx Add(Cplx b)
            {
                Cplx a = this;
                return new Cplx()
                {
                    Re = a.Re + b.Re,
                    Imaginari = a.Imaginari + b.Imaginari
                };
            }
            public double GetAngleInDegrees()
            {
                return Math.Atan(Imaginari / Re);
            }
            public Cplx Subtract(Cplx b)
            {
                Cplx a = this;
                return new Cplx()
                {
                    Re = a.Re - b.Re,
                    Imaginari = a.Imaginari - b.Imaginari
                };
            }

            public override string ToString()
            {
                return $"({Re} + {Imaginari}i)";
            }

            internal Cplx Divide(Cplx b)
            {
                // (aRe + aIm*i) / (bRe + bIm*i)
                // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
                //  bRe*bRe - bIm*bIm*i*i
                var tmp = this.Multiply(new Cplx() { Re = b.Re, Imaginari = -b.Imaginari });
                var tmp2 = b.Re * b.Re + b.Imaginari * b.Imaginari;

                return new Cplx()
                {
                    Re = tmp.Re / tmp2,
                    Imaginari = (float)(tmp.Imaginari / tmp2)
                };
            }
        }
    }
}
