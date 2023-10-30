using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// List of Complex numbers
        /// </summary>
        public List<ComplexNumber> ComplexNumbersList { get; set; }

        public Polynomial() => ComplexNumbersList = new List<ComplexNumber>();

        public void Add(ComplexNumber number) =>
            ComplexNumbersList.Add(number);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();
            for (int i = 1; i < ComplexNumbersList.Count; i++)
            {
                polynomial.ComplexNumbersList.Add(ComplexNumbersList[i].Multiply(new ComplexNumber() { Real = i }));
            }

            return polynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="pointOfEvaluation">point of evaluation</param>
        /// <returns>evaluation</returns>
        public ComplexNumber Evaluate(double pointOfEvaluation)
        {
            var evaluation = Evaluate(new ComplexNumber() { Real = pointOfEvaluation, Imaginari = 0 });
            return evaluation;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="pointOfEvaluation">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(ComplexNumber pointOfEvaluation)
        {
            ComplexNumber evaluation = ComplexNumber.Zero;
            for (int i = 0; i < ComplexNumbersList.Count; i++)
            {
                ComplexNumber coeficient = ComplexNumbersList[i];
                ComplexNumber number = pointOfEvaluation;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        number = number.Multiply(pointOfEvaluation);

                    coeficient = coeficient.Multiply(number);
                }

                evaluation = evaluation.Add(coeficient);
            }

            return evaluation;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string s = "";
            int i = 0;
            for (; i < ComplexNumbersList.Count; i++)
            {
                s += ComplexNumbersList[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i + 1 < ComplexNumbersList.Count)
                    s += " + ";
            }
            return s;
        }
    }
}
