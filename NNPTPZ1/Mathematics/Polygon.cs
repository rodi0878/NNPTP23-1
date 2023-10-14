﻿using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polygon
    {
        /// <summary>
        /// Coefficients
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polygon() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coe) =>
            Coefficients.Add(coe);

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polygon Derive()
        {
            Polygon polygon = new Polygon();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                polygon.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
            }

            return polygon;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(double x)
        {
            return Eval(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="evaluationPoint">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(ComplexNumber evaluationPoint)
        {
            ComplexNumber evaluatedComplexNumber = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                evaluatedComplexNumber = EvaluateAtPoint(evaluationPoint, i, evaluatedComplexNumber);
            }

            return evaluatedComplexNumber;
        }

        private ComplexNumber EvaluateAtPoint(ComplexNumber evaluationPoint, int iteration, ComplexNumber evaluatedComplexNumber)
        {
            ComplexNumber coefficient = Coefficients[iteration];
            ComplexNumber currentPowerValue = evaluationPoint;
            int power = iteration;

            if (iteration > 0)
            {
                for (int i = 0; i < power - 1; i++)
                    currentPowerValue = currentPowerValue.Multiply(evaluationPoint);

                coefficient = coefficient.Multiply(currentPowerValue);
            }

            evaluatedComplexNumber = evaluatedComplexNumber.Add(coefficient);
            return evaluatedComplexNumber;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string s = "";
            int i = 0;
            for (; i < Coefficients.Count; i++)
            {
                s += Coefficients[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        s += "x";
                    }
                }
                if (i+1<Coefficients.Count)
                    s += " + ";
            }
            return s;
        }
    }
}