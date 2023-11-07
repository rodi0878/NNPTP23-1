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

        private static int maxRootId = 0;

        private static List<Cplx> roots = new List<Cplx>();

        private static Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        static void Main(string[] args)
        {
            Initialization(args);

            Poly polynomial = CreatePolynomial();
            Poly polynomialDerivative = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomialDerivative);

            CalculateAndColorizePixels(polynomial, polynomialDerivative);

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

            SaveImage(bitmapImage, output);
            //Console.ReadKey();
        }

        static void Initialization(string[] args)
        {

            intArguments = ParseIntArguments(args);
            doubleArguments = ParseDoubleArguments(args);
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

        static int[] ParseIntArguments(string[] args)
        {
            for (int i = 0; i < intArguments.Length; i++)
            {
                intArguments[i] = int.Parse(args[i]);
            }
            return intArguments;
        }

        static double[] ParseDoubleArguments(string[] args)
        {
            for (int i = 0; i < doubleArguments.Length; i++)
            {
                doubleArguments[i] = double.Parse(args[i + 2]);
            }
            return doubleArguments;
        }

        static Poly CreatePolynomial()
        {
            // TODO: poly should be parameterised?
            Poly polynomial = new Poly();
            polynomial.Coe.Add(new Cplx() { Re = 1 });
            polynomial.Coe.Add(Cplx.Zero);
            polynomial.Coe.Add(Cplx.Zero);
            //polynomial.Coe.Add(Cplx.Zero);
            polynomial.Coe.Add(new Cplx() { Re = 1 });
            return polynomial;
        }

        static void CalculateAndColorizePixels(Poly polynomial, Poly polynomialDerivative)
        {
            var computedRoots = new Dictionary<Cplx, int>();

            for (int i = 0; i < intArguments[0]; i++)
            {
                for (int j = 0; j < intArguments[1]; j++)
                {
                    Cplx currentComplex = CalculateWorldCoordinates(i, j);
                    float iterations = NewtonIteration(polynomial, polynomialDerivative, currentComplex);

                    int rootId = FindRootNumber(currentComplex);

                    Color color = ColorizePixel(rootId, (int)iterations);
                    SetPixelColor(j, i, color);
                }
            }
        }

        static Cplx CalculateWorldCoordinates(int i, int j)
        {
            double currentY = minY + i * yStep;
            double currentX = minX + j * xStep;
            return new Cplx()
            {
                Re = currentX,
                Imaginari = (float)currentY
            };
        }

        static float NewtonIteration(Poly polynomial, Poly polynomialDerivative, Cplx currentComplex)
        {
            float iterations = 0;
            for (int q = 0; q < 30; q++)
            {
                var difference = polynomial.Eval(currentComplex).Divide(polynomialDerivative.Eval(currentComplex));
                currentComplex = currentComplex.Subtract(difference);
                if (IsConverged(difference))
                {
                    q--;
                }
                iterations++;
            }
            return iterations;
        }

        static bool IsConverged(Cplx difference)
        {
            return Math.Pow(difference.Re, 2) + Math.Pow(difference.Imaginari, 2) >= 0.5;
        }

   

        static int FindRootNumber(Cplx currentComplex)
        {
            Boolean isKnownRoot = false;
            int rootId = 0;
            for (int w = 0; w < roots.Count; w++)
            {
                if (IsCloseToRoot(currentComplex, roots[w]))
                {
                    isKnownRoot = true;
                    rootId = w;
                }
            }
            if (!isKnownRoot)
            {
                roots.Add(currentComplex);
                rootId = roots.Count;
                maxRootId = rootId + 1;
            }
            return rootId;
        }

        static bool IsCloseToRoot(Cplx currentComplex, Cplx root)
        {
            return Math.Pow(currentComplex.Re - root.Re, 2) + Math.Pow(currentComplex.Imaginari - root.Imaginari, 2) <= 0.01;
        }

        static Color ColorizePixel(int id, int iterations)
        {
            var color = colors[id % colors.Length];
            color = Color.FromArgb(color.R, color.G, color.B);
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - iterations * 2), 255), Math.Min(Math.Max(0, color.G - iterations * 2), 255), Math.Min(Math.Max(0, color.B - iterations * 2), 255));
            return color;
        }

        static void SetPixelColor(int i, int j, Color color)
        {
            bitmapImage.SetPixel(j, i, color);
        }



        static void SaveImage(Bitmap bitmapImage, string output)
        {
            // TODO: delete I suppose...
            //for (int i = 0; i < 300; i++)
            //{
            //    for (int j = 0; j < 300; j++)
            //    {
            //        Color c = bmp.GetPixel(j, i);
            //        int nv = (int)Math.Floor(c.R * (255.0 / maxid));
            //        bitmapImage.SetPixel(j, i, Color.FromArgb(nv, nv, nv));
            //    }
            //}
            bitmapImage.Save(output ?? "../../../out.png");
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
