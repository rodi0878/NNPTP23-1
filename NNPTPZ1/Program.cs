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

        private static List<ComplexNumber> roots = new List<ComplexNumber>();

        private static Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        static void Main(string[] args)
        {
            Initialization(args);

            Polynomial polynomial = CreatePolynomial();
            Polynomial polynomialDerivative = polynomial.Derivative();

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

        static Polynomial CreatePolynomial()
        {
            // TODO: poly should be parameterised?
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            //polynomial.Coe.Add(Cplx.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            return polynomial;
        }

        static void CalculateAndColorizePixels(Polynomial polynomial, Polynomial polynomialDerivative)
        {
            var computedRoots = new Dictionary<ComplexNumber, int>();

            for (int i = 0; i < intArguments[0]; i++)
            {
                for (int j = 0; j < intArguments[1]; j++)
                {
                    ComplexNumber currentComplex = CalculateWorldCoordinates(i, j);
                    float iterations = NewtonIteration(polynomial, polynomialDerivative, currentComplex);

                    int rootId = FindRootNumber(currentComplex);

                    Color color = ColorizePixel(rootId, (int)iterations);
                    SetPixelColor(j, i, color);
                }
            }
        }

        static ComplexNumber CalculateWorldCoordinates(int i, int j)
        {
            double currentY = minY + i * yStep;
            double currentX = minX + j * xStep;
            return new ComplexNumber()
            {
                Real = currentX,
                Imaginary = (float)currentY
            };
        }

        static float NewtonIteration(Polynomial polynomial, Polynomial polynomialDerivative, ComplexNumber currentComplex)
        {
            float iterations = 0;
            for (int q = 0; q < 30; q++)
            {
                var difference = polynomial.Evaluate(currentComplex).Divide(polynomialDerivative.Evaluate(currentComplex));
                currentComplex = currentComplex.Subtract(difference);
                if (IsConverged(difference))
                {
                    q--;
                }
                iterations++;
            }
            return iterations;
        }

        static bool IsConverged(ComplexNumber difference)
        {
            return Math.Pow(difference.Real, 2) + Math.Pow(difference.Imaginary, 2) >= 0.5;
        }

   

        static int FindRootNumber(ComplexNumber currentComplex)
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

        static bool IsCloseToRoot(ComplexNumber currentComplex, ComplexNumber root)
        {
            return Math.Pow(currentComplex.Real - root.Real, 2) + Math.Pow(currentComplex.Imaginary - root.Imaginary, 2) <= 0.01;
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
        public class Polynomial
        {
            /// <summary>
            /// Coe
            /// </summary>
            public List<ComplexNumber> Coefficients { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            public Polynomial() => Coefficients = new List<ComplexNumber>();

            public void Add(ComplexNumber coefficient) =>
                Coefficients.Add(coefficient);

            /// <summary>
            /// Derives this polynomial and creates new one
            /// </summary>
            /// <returns>Derivated polynomial</returns>
            public Polynomial Derivative()
            {
                Polynomial derivatedPolynomial = new Polynomial();
                for (int q = 1; q < Coefficients.Count; q++)
                {
                    derivatedPolynomial.Coefficients.Add(Coefficients[q].Multiply(new ComplexNumber() { Real = q }));
                }

                return derivatedPolynomial;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Evaluate(double x)
            {
                var y = Evaluate(new ComplexNumber() { Real = x, Imaginary = 0 });
                return y;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Evaluate(ComplexNumber x)
            {
                ComplexNumber zero = ComplexNumber.Zero;
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber coefficient = Coefficients[i];
                    ComplexNumber currentTerm = x;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            currentTerm = currentTerm.Multiply(x);

                        coefficient = coefficient.Multiply(currentTerm);
                    }

                    zero = zero.Add(coefficient);
                }

                return zero;
            }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns>String repr of polynomial</returns>
            public override string ToString()
            {
                string termStrings = "";
                int i = 0;
                for (; i < Coefficients.Count; i++)
                {
                    termStrings += Coefficients[i];
                    if (i > 0)
                    {
                        int j = 0;
                        for (; j < i; j++)
                        {
                            termStrings += "x";
                        }
                    }
                    if (i+1<Coefficients.Count)
                    termStrings += " + ";
                }
                return termStrings;
            }
        }

        public class ComplexNumber
        {
            public double Real { get; set; }
            public float Imaginary { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is ComplexNumber)
                {
                    ComplexNumber x = obj as ComplexNumber;
                    return x.Real == Real && x.Imaginary == Imaginary;
                }
                return base.Equals(obj);
            }

            public readonly static ComplexNumber Zero = new ComplexNumber()
            {
                Real = 0,
                Imaginary = 0
            };

            public ComplexNumber Multiply(ComplexNumber complexNumber)
            {
                ComplexNumber dividedNumber = this;
                // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
                return new ComplexNumber()
                {
                    Real = dividedNumber.Real * complexNumber.Real - dividedNumber.Imaginary * complexNumber.Imaginary,
                    Imaginary = (float)(dividedNumber.Real * complexNumber.Imaginary + dividedNumber.Imaginary * complexNumber.Real)
                };
            }
            public double GetAbS()
            {
                return Math.Sqrt( Real * Real + Imaginary * Imaginary);
            }

            public ComplexNumber Add(ComplexNumber complexNumber)
            {
                ComplexNumber dividedNumber = this;
                return new ComplexNumber()
                {
                    Real = dividedNumber.Real + complexNumber.Real,
                    Imaginary = dividedNumber.Imaginary + complexNumber.Imaginary
                };
            }
            public double GetAngleInDegrees()
            {
                return Math.Atan(Imaginary / Real);
            }
            public ComplexNumber Subtract(ComplexNumber complexNumber)
            {
                ComplexNumber dividedNumber = this;
                return new ComplexNumber()
                {
                    Real = dividedNumber.Real - complexNumber.Real,
                    Imaginary = dividedNumber.Imaginary - complexNumber.Imaginary
                };
            }

            public override string ToString()
            {
                return $"({Real} + {Imaginary}i)";
            }

            internal ComplexNumber Divide(ComplexNumber complexNumber)
            {
                // (aRe + aIm*i) / (bRe + bIm*i)
                // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
                //  bRe*bRe - bIm*bIm*i*i
                var dividend = this.Multiply(new ComplexNumber() { Real = complexNumber.Real, Imaginary = -complexNumber.Imaginary });
                var devider = complexNumber.Real * complexNumber.Real + complexNumber.Imaginary * complexNumber.Imaginary;

                return new ComplexNumber()
                {
                    Real = dividend.Real / devider,
                    Imaginary = (float)(dividend.Imaginary / devider)
                };
            }
        }
    }
}
