using System;
using System.Collections.Generic;
using System.Drawing;
using Mathematics;
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
            string output = args[6];
            // TODO: add parameters from args?
            Bitmap bmp = new Bitmap(intargs[0], intargs[1]);
            double xmin = doubleargs[0];
            double xmax = doubleargs[1];
            double ymin = doubleargs[2];
            double ymax = doubleargs[3];

            double xstep = (xmax - xmin) / intargs[0];
            double ystep = (ymax - ymin) / intargs[1];

            List<Cplx> koreny = new List<Cplx>();
            // TODO: poly should be parameterised?
            Poly p = new Poly();
            p.Coe.Add(new Cplx() { Re = 1 });
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(new Cplx() { Re = 1 });
            Poly pd = p.Derive();

            Console.WriteLine(p);
            Console.WriteLine(pd);

            var clrs = new Color[]
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

                    Cplx ox = new Cplx()
                    {
                        Re = x,
                        Imaginari = (float)(y)
                    };

                    if (ox.Re == 0)
                        ox.Re = 0.0001;
                    if (ox.Imaginari == 0)
                        ox.Imaginari = 0.0001f;

                    // find solution of equation using newton's iteration
                    float it = 0;
                    for (int q = 0; q< 30; q++)
                    {
                        var diff = p.Eval(ox).Divide(pd.Eval(ox));
                        ox = ox.Subtract(diff);

                        if (Math.Pow(diff.Re, 2) + Math.Pow(diff.Imaginari, 2) >= 0.5)
                        {
                            q--;
                        }
                        it++;
                    }

                    // find solution root number
                    var known = false;
                    var id = 0;
                    for (int w = 0; w <koreny.Count;w++)
                    {
                        if (Math.Pow(ox.Re- koreny[w].Re, 2) + Math.Pow(ox.Imaginari - koreny[w].Imaginari, 2) <= 0.01)
                        {
                            known = true;
                            id = w;
                        }
                    }
                    if (!known)
                    {
                        koreny.Add(ox);
                        id = koreny.Count;
                    }

                    var vv = clrs[id % clrs.Length];
                    vv = Color.FromArgb(vv.R, vv.G, vv.B);
                    vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R-(int)it*2), 255), Math.Min(Math.Max(0, vv.G - (int)it*2), 255), Math.Min(Math.Max(0, vv.B - (int)it*2), 255));
                    bmp.SetPixel(j, i, vv);
                }
            }
                    bmp.Save(output ?? "../../../out.png");
        }
    }

    namespace Mathematics
    {
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
