using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginary = 0
        };

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(Imaginary / Real);
        }

        public ComplexNumber Add(ComplexNumber term)
        {
            ComplexNumber thisNumber = this;
            return new ComplexNumber()
            {
                Real = thisNumber.Real + term.Real,
                Imaginary = thisNumber.Imaginary + term.Imaginary
            };
        }

        public ComplexNumber Subtract(ComplexNumber term)
        {
            ComplexNumber thisNumber = this;
            return new ComplexNumber()
            {
                Real = thisNumber.Real - term.Real,
                Imaginary = thisNumber.Imaginary - term.Imaginary
            };
        }

        public ComplexNumber Multiply(ComplexNumber multiplicand)
        {
            ComplexNumber thisNumber = this;
            return new ComplexNumber()
            {
                Real = thisNumber.Real * multiplicand.Real - thisNumber.Imaginary * multiplicand.Imaginary,
                Imaginary = (float)(thisNumber.Real * multiplicand.Imaginary + thisNumber.Imaginary * multiplicand.Real)
            };
        }
        
        internal ComplexNumber Divide(ComplexNumber number)
        {
            var numerator = this.Multiply(new ComplexNumber() { Real = number.Real, Imaginary = -number.Imaginary });
            var denominator = number.Real * number.Real + number.Imaginary * number.Imaginary;

            return new ComplexNumber()
            {
                Real = numerator.Real / denominator,
                Imaginary = numerator.Imaginary / denominator
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber number = obj as ComplexNumber;
                return number.Real == Real && number.Imaginary == Imaginary;
            }
            return base.Equals(obj);
        }

        
    }
}
