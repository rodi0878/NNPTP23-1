using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber numberToCompare = obj as ComplexNumber;
                return numberToCompare.RealPart == RealPart && numberToCompare.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public ComplexNumber Multiply(ComplexNumber complexNumberArgument)
        {
            ComplexNumber complexNumberBase = this;
            return new ComplexNumber()
            {
                RealPart = complexNumberBase.RealPart * complexNumberArgument.RealPart - complexNumberBase.ImaginaryPart * complexNumberArgument.ImaginaryPart,
                ImaginaryPart = complexNumberBase.RealPart * complexNumberArgument.ImaginaryPart + complexNumberBase.ImaginaryPart * complexNumberArgument.RealPart
            };
        }
        public double GetAbS() => Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);

        public ComplexNumber Add(ComplexNumber complexNumberArgument)
        {
            ComplexNumber complexNumberBase = this;
            return new ComplexNumber()
            {
                RealPart = complexNumberBase.RealPart + complexNumberArgument.RealPart,
                ImaginaryPart = complexNumberBase.ImaginaryPart + complexNumberArgument.ImaginaryPart
            };
        }
        public double GetAngleInDegrees() => Math.Atan(ImaginaryPart / RealPart);

        public ComplexNumber Subtract(ComplexNumber complexNumberArgument)
        {
            ComplexNumber complexNumberBase = this;
            return new ComplexNumber()
            {
                RealPart = complexNumberBase.RealPart - complexNumberArgument.RealPart,
                ImaginaryPart = complexNumberBase.ImaginaryPart - complexNumberArgument.ImaginaryPart
            };
        }

        public override string ToString() => $"({RealPart} + {ImaginaryPart}i)";

        internal ComplexNumber Divide(ComplexNumber b)
        {
            var tmp = this.Multiply(new ComplexNumber() { RealPart = b.RealPart, ImaginaryPart = -b.ImaginaryPart });
            var tmp2 = b.RealPart * b.RealPart + b.ImaginaryPart * b.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = tmp.RealPart / tmp2,
                ImaginaryPart = (float)(tmp.ImaginaryPart / tmp2)
            };
        }
    }
}
