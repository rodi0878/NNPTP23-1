using NNPTPZ1.Mathematics;
using System.Collections.Generic;

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

        public void Add(Cplx coe) => Coe.Add(coe);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Poly Derive()
        {
            Poly derivedPoly = new Poly();
            for (int i = 1; i < Coe.Count; i++)
            {
                derivedPoly.Coe.Add(Coe[i].Multiply(new Cplx() { Re = i }));
            }

            return derivedPoly;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public Cplx Eval(double x) => Eval(new Cplx() { Re = x, Imaginari = 0 });

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public Cplx Eval(Cplx x)
        {
            Cplx result = Cplx.Zero;
            for (int i = 0; i < Coe.Count; i++)
            {
                Cplx coef = Coe[i];

                for (int j = 0; j < i; j++)
                    coef = coef.Multiply(x);

                result = result.Add(coef);
            }
            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string polyString = "";
            for (int i = 0; i < Coe.Count; i++)
            {
                polyString += Coe[i];
                if (i > 0)
                {
                    polyString += new string('x', i);
                }
                if (i + 1 < Coe.Count)
                    polyString += " + ";
            }
            return polyString;
        }
    }
}
