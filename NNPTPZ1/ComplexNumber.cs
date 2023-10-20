using System;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class ComplexNumber
        {
            public double RealElement { get; set; }
            public double ImaginaryElement { get; set; }

            public readonly static ComplexNumber Zero = new ComplexNumber()
            {
                RealElement = 0,
                ImaginaryElement = 0
            };            
            
            public ComplexNumber Add(ComplexNumber rightAddend)
            {
                ComplexNumber leftAddend = this;
                return new ComplexNumber()
                {
                    RealElement = leftAddend.RealElement + rightAddend.RealElement,
                    ImaginaryElement = leftAddend.ImaginaryElement + rightAddend.ImaginaryElement
                };
            }

            public ComplexNumber Subtract(ComplexNumber subtrahend)
            {
                ComplexNumber minuend = this;
                return new ComplexNumber()
                {
                    RealElement = minuend.RealElement - subtrahend.RealElement,
                    ImaginaryElement = minuend.ImaginaryElement - subtrahend.ImaginaryElement
                };
            }
            public ComplexNumber Multiply(ComplexNumber rightFactor)
            {
                ComplexNumber leftFactor = this;
                return new ComplexNumber()
                {
                    RealElement = (leftFactor.RealElement * rightFactor.RealElement) - (leftFactor.ImaginaryElement * rightFactor.ImaginaryElement),
                    ImaginaryElement = (leftFactor.RealElement * rightFactor.ImaginaryElement) + (leftFactor.ImaginaryElement * rightFactor.RealElement)
                };
            }             
            public ComplexNumber Divide(ComplexNumber complexNumber)
            {
                var divident = this.Multiply(new ComplexNumber() { RealElement = complexNumber.RealElement, ImaginaryElement = -complexNumber.ImaginaryElement });
                var divisor = (complexNumber.RealElement * complexNumber.RealElement) + (complexNumber.ImaginaryElement * complexNumber.ImaginaryElement);

                return new ComplexNumber()
                {
                    RealElement = divident.RealElement / divisor,
                    ImaginaryElement = divident.ImaginaryElement / divisor
                };
            }            
            public bool Equals(object inputObject)
            {
                if (inputObject is ComplexNumber)
                {
                    ComplexNumber complexNumberToCompare = inputObject as ComplexNumber;
                    return complexNumberToCompare.RealElement == RealElement && complexNumberToCompare.ImaginaryElement == ImaginaryElement;
                }
                return base.Equals(inputObject);
            }

            public double GetAbS()
            {
                return Math.Sqrt((RealElement * RealElement) + (ImaginaryElement * ImaginaryElement));
            }

            public double GetAngleInRadians()
            {
                return Math.Atan(ImaginaryElement / RealElement);
            }

            public override string ToString()
            {
                return $"({RealElement} + {ImaginaryElement}i)";
            }
        }
    }
}
