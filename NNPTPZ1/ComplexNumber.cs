using System;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class ComplexNumber
        {
            public double Real { get; set; }
            public float Imaginary { get; set; }

            public ComplexNumber(double Real, float Imaginary)
            {
                this.Real = Real;
                this.Imaginary = Imaginary;
            }

            public readonly static ComplexNumber Zero = new ComplexNumber(0, 0);
            public override bool Equals(object obj)
            {
                if (obj is ComplexNumber)
                {
                    ComplexNumber complexNumber = obj as ComplexNumber;
                    return complexNumber.Real == Real && complexNumber.Imaginary == Imaginary;
                }
                return base.Equals(obj);
            }
            public ComplexNumber Multiply(ComplexNumber complexNumberTwo)
            {
                ComplexNumber complexNumberOne = this;
                double realPart = complexNumberOne.Real * complexNumberTwo.Real - complexNumberOne.Imaginary * complexNumberTwo.Imaginary;
                float imaginaryPart = (float)(complexNumberOne.Real * complexNumberTwo.Imaginary + complexNumberOne.Imaginary * complexNumberTwo.Real);
                return new ComplexNumber(realPart, imaginaryPart);
            }
            public double GetAbsoluteValue()
            {
                return Math.Sqrt( Real * Real + Imaginary * Imaginary);
            }

            public ComplexNumber Add(ComplexNumber complexNumberTwo)
            {
                ComplexNumber complexNumberOne = this;
                double realPart = complexNumberOne.Real + complexNumberTwo.Real;
                float imaginaryPart = complexNumberOne.Imaginary + complexNumberTwo.Imaginary;
                return new ComplexNumber(realPart, imaginaryPart);
            }
            public double GetAngleInDegrees()
            {
                return Math.Atan(Imaginary / Real);
            }
            public ComplexNumber Subtract(ComplexNumber complexNumberTwo)
            {
                ComplexNumber complexNumberOne = this;
                double realPart = complexNumberOne.Real - complexNumberTwo.Real;
                float imaginaryPart = complexNumberOne.Imaginary - complexNumberTwo.Imaginary;
                return new ComplexNumber(realPart, imaginaryPart);
            }
            public override string ToString()
            {
                return $"({Real} + {Imaginary}i)";
            }

            internal ComplexNumber Divide(ComplexNumber complexNumberTwo)
            {
                // (aRe + aIm*i) / (bRe + bIm*i)
                // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
                //  bRe*bRe - bIm*bIm*i*i
                ComplexNumber tmp = Multiply(new ComplexNumber() { Real = complexNumberTwo.Real, Imaginary = -complexNumberTwo.Imaginary });
                var tmp2 = complexNumberTwo.Real * complexNumberTwo.Real + complexNumberTwo.Imaginary * complexNumberTwo.Imaginary;

                return new ComplexNumber()
                {
                    Real = tmp.Real / tmp2,
                    Imaginary = (float)(tmp.Imaginary / tmp2)
                };
            }
        }
    }
}
