using System;
using Mathematics;

namespace NNPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            Render render = new Render(args);

            Polynome polynome = new Polynome();
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            Polynome derivatedPolynome = polynome.DerivatePolynome();

            Console.WriteLine(polynome);
            Console.WriteLine(derivatedPolynome);

            render.renderPicture(polynome, derivatedPolynome);
        }
    }
}
