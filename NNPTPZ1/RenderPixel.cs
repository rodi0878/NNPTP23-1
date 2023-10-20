using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    class RenderPixel
    {
        public Bitmap bitmap { get; set; }

        public List<ComplexNumber> roots { get; set; }

        public ComplexNumber pixelWithCoordinates { get; set; }

        public string[] InputArguments { get; set; }

        private const int baseNumberOfIterations = 30;

        public RenderPixel(string[] args) {

            InputArguments = args;

            bitmap = new Bitmap(int.Parse(InputArguments[0]), int.Parse(InputArguments[1]));

            roots = new List<ComplexNumber>();

            pixelWithCoordinates = new ComplexNumber();

        }

        public void RenderBitmap(Polynomial polynomial, Polynomial derivedPolynomial)
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {

                    FindBitmapCoordinatesOfPixel(i, j);

                    int numberOfIterations = CalculatingTheEquationByNewtonsIteration(polynomial, derivedPolynomial);

                    int rootNumber = getSolutionRootNumber(pixelWithCoordinates);

                    bitmap.SetPixel(j, i, CalculatePixelColorAccordingToRootNumber(numberOfIterations, rootNumber));
                }
            }
        }
        public void FindBitmapCoordinatesOfPixel(int row, int column)
        {
            double xmin = double.Parse(InputArguments[2]);
            double xmax = double.Parse(InputArguments[3]);
            double ymin = double.Parse(InputArguments[4]);
            double ymax = double.Parse(InputArguments[5]);

            double coordinateY = ymin + row * ((ymax - ymin) / int.Parse(InputArguments[1]));
            double coordinateX = xmin + column * ((xmax - xmin) / int.Parse(InputArguments[0]));

            pixelWithCoordinates = new ComplexNumber()
            {
                RealElement = coordinateX,
                ImaginaryElement = coordinateY
            };

            if (pixelWithCoordinates.RealElement == 0)
                pixelWithCoordinates.RealElement = 0.0001;
            if (pixelWithCoordinates.ImaginaryElement == 0)
                pixelWithCoordinates.ImaginaryElement = 0.0001f;

        }

        public int CalculatingTheEquationByNewtonsIteration(Polynomial polynomial, Polynomial derivedPolynomial)
        {
            int numberOfIterations = 0;
            for (int i = 0; i < baseNumberOfIterations; i++)
            {
                var quotient = polynomial.Eval(pixelWithCoordinates).Divide(derivedPolynomial.Eval(pixelWithCoordinates));
                pixelWithCoordinates = pixelWithCoordinates.Subtract(quotient);

                if (Math.Pow(quotient.RealElement, 2) + Math.Pow(quotient.ImaginaryElement, 2) >= 0.5)
                {
                    i--;
                }
                numberOfIterations++;
            }

            return numberOfIterations;
        }

        private int getSolutionRootNumber(ComplexNumber pixelWithCoordinates)
        {
            int  rootNumber = 0;
            bool solutionFound = false;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(pixelWithCoordinates.RealElement - roots[i].RealElement, 2) + Math.Pow(pixelWithCoordinates.ImaginaryElement - roots[i].ImaginaryElement, 2) <= 0.01)
                {
                    solutionFound = true;
                    rootNumber = i;
                    break;
                }
            }
            if (!solutionFound)
            {
                roots.Add(pixelWithCoordinates);
                rootNumber = roots.Count;
            }

            return rootNumber;
        }

        public Color CalculatePixelColorAccordingToRootNumber(int numberOfIterations, int rootNumber)
        {
            Color pixelColor = Color.FromName(((Colors)(rootNumber % Enum.GetNames(typeof(Colors)).Length)).ToString());
            pixelColor = Color.FromArgb(Math.Min(Math.Max(0, pixelColor.R - numberOfIterations * 2), 255), 
                Math.Min(Math.Max(0, pixelColor.G - numberOfIterations * 2), 255), Math.Min(Math.Max(0, pixelColor.B - numberOfIterations * 2), 255));
            return pixelColor;
        }

        public void SaveIntoFile()
        {
            bitmap.Save(InputArguments[6] ?? "../../../out.png");
        }





    }
}
