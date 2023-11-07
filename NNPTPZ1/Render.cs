using Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    public class Render
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public string OutputFile { get; set; }
        public Bitmap Image { get; set; }
        public double XStep { get; set; }
        public double YStep { get; set; }
        public List<ComplexNumber> Roots { get; set; } = new List<ComplexNumber>();
        public static Color[] Colors { get; } = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };
        public Render(string[] args)
        {
            Width = int.Parse(args[0]);
            Height = int.Parse(args[1]);
            XMin = double.Parse(args[2]);
            XMax = double.Parse(args[3]);
            YMin = double.Parse(args[4]);
            YMax = double.Parse(args[5]);
            OutputFile = args[6];

            Image = new Bitmap(Width, Height);

            XStep = (XMax - XMin) / Width;
            YStep = (YMax - YMin) / Height;
        }

        public void RenderPicture(Polynome polynome, Polynome derivatedPolynome)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    ComplexNumber complexNumber = FindWorldCoordinates(i, j);
                    float iteration = FindSolutionOfEquation(ref complexNumber, polynome, derivatedPolynome);
                    int identificator = FindSolutionRootNumber(complexNumber, Roots);
                    Color pixelColor = Colors[identificator % Colors.Length];
                    pixelColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
                    pixelColor = Color.FromArgb(Math.Min(Math.Max(0, pixelColor.R - (int)iteration * 2), 255), Math.Min(Math.Max(0, pixelColor.G - (int)iteration * 2), 255), Math.Min(Math.Max(0, pixelColor.B - (int)iteration * 2), 255));
                    Image.SetPixel(j, i, pixelColor);
                }
            }
            Image.Save(OutputFile ?? "../../../out.png");
        }

        private ComplexNumber FindWorldCoordinates(int i, int j)
        {
            double y = YMin + i * YStep;
            double x = XMin + j * XStep;

            double realPart = (x == 0) ? 0.0001f : x;
            float imagPart = (y == 0) ? 0.0001f : (float)y;

            return new ComplexNumber { RealPart = realPart, ImaginariPart = imagPart };
        }

        private int FindSolutionOfEquation(ref ComplexNumber complexNumber, Polynome polynome, Polynome derivatedPolynome)
        {
            int iteration = 0;
            ComplexNumber localComplex = new ComplexNumber { RealPart = complexNumber.RealPart, ImaginariPart = complexNumber.ImaginariPart };
            for (int i = 0; i < 30; i++)
            {
                ComplexNumber polynomialDifference = polynome.EvaluatePolynome(localComplex).Divide(derivatedPolynome.EvaluatePolynome(localComplex));
                localComplex = localComplex.Subtract(polynomialDifference);

                if (Math.Pow(polynomialDifference.RealPart, 2) + Math.Pow(polynomialDifference.ImaginariPart, 2) >= 0.5)
                {
                    i--;
                }
                iteration++;
            }
            complexNumber = localComplex;
            return iteration;
        }

        private int FindSolutionRootNumber(ComplexNumber complexNumber, List<ComplexNumber> roots)
        {
            double epsilon = 0.01;

            for (int i = 0; i < roots.Count; i++)
            {
                double distanceSquared = Math.Pow(complexNumber.RealPart - roots[i].RealPart, 2) +
                                        Math.Pow(complexNumber.ImaginariPart - roots[i].ImaginariPart, 2);
                if (distanceSquared <= epsilon)
                {
                    return i;
                }
            }

            roots.Add(complexNumber);
            return roots.Count - 1;
        }
    }
}