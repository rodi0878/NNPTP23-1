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
        public double Imaginari { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber number = obj as ComplexNumber;
                return number.Real == Real && number.Imaginari == Imaginari;
            }
            return base.Equals(obj);
        }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginari = 0
        };

        public ComplexNumber Multiply(ComplexNumber multiplicand)
        {
            ComplexNumber thisNumber = this;
            return new ComplexNumber()
            {
                Real = thisNumber.Real * multiplicand.Real - thisNumber.Imaginari * multiplicand.Imaginari,
                Imaginari = (float)(thisNumber.Real * multiplicand.Imaginari + thisNumber.Imaginari * multiplicand.Real)
            };
        }
        public double GetAbsoluteValue()
        {
            return Math.Sqrt(Real * Real + Imaginari * Imaginari);
        }

        public ComplexNumber Add(ComplexNumber term)
        {
            ComplexNumber thisNumber = this;
            return new ComplexNumber()
            {
                Real = thisNumber.Real + term.Real,
                Imaginari = thisNumber.Imaginari + term.Imaginari
            };
        }
        public double GetAngleInRadians()
        {
            return Math.Atan(Imaginari / Real);
        }
        public ComplexNumber Subtract(ComplexNumber term)
        {
            ComplexNumber thisNumber = this;
            return new ComplexNumber()
            {
                Real = thisNumber.Real - term.Real,
                Imaginari = thisNumber.Imaginari - term.Imaginari
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginari}i)";
        }

        internal ComplexNumber Divide(ComplexNumber number)
        {
            var numerator = this.Multiply(new ComplexNumber() { Real = number.Real, Imaginari = -number.Imaginari });
            var denominator = number.Real * number.Real + number.Imaginari * number.Imaginari;

            return new ComplexNumber()
            {
                Real = numerator.Real / denominator,
                Imaginari = numerator.Imaginari / denominator
            };
        }
    }
}
