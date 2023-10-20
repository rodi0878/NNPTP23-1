using System;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    partial class Program
    {
        static void Main(string[] args)
        {
            Polynomial polynomial = new Polynomial();
            polynomial.ListOfComplexNumbers.Add(new ComplexNumber() { RealElement = 1 });
            polynomial.ListOfComplexNumbers.Add(ComplexNumber.Zero);
            polynomial.ListOfComplexNumbers.Add(ComplexNumber.Zero);
            polynomial.ListOfComplexNumbers.Add(new ComplexNumber() { RealElement = 1 });
            Polynomial derivedPolynomial = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(derivedPolynomial);

            RenderPixel render = new RenderPixel(args);

            render.RenderBitmap(polynomial, derivedPolynomial);

            render.SaveIntoFile();
        }
    }
}
