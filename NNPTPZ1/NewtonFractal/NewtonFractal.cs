using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;
using NNPTPZ1.NewtonFractal.Coordinates;
using NNPTPZ1.NewtonFractal.Screen;
using NNPTPZ1.NewtonFractal.Settings;

namespace NNPTPZ1.NewtonFractal
{
    /// <summary>
    /// Used to generate Newton's fractals.
    /// </summary>
    public class NewtonFractal
    {
        private readonly ScreenSize _screenSize;
        private readonly BoundaryCoordinates _boundaryCoordinates;
        private readonly Bitmap _bmp;
        private readonly List<ComplexNumber> _roots;
        private readonly Color[] _colorPalette;

        public NewtonFractal(NewtonFractalSettings settings)
        {
            _screenSize = settings.ScreenSize;
            _boundaryCoordinates = settings.BoundaryCoordinates;
            
            _bmp = new Bitmap(_screenSize.Width, _screenSize.Height);
            _roots = new List<ComplexNumber>();
            _colorPalette = new[] {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan,
                Color.Magenta
            };
        }

        /// <summary>
        /// Calculates Newton's fractals based on the settings passed.
        /// </summary>
        /// <returns>Bitmap of the resulting Newton fractal image.</returns>
        public Bitmap Compute()
        {
            Polygon polygon = CreateDefaultPolygonForCalculation();
            Polygon polygonDerived = polygon.Derive();

            Console.WriteLine(polygon);
            Console.WriteLine(polygonDerived);
            
            // for every pixel in image...
            for (int x = 0; x < _screenSize.Width; x++)
            {
                for (int y = 0; y < _screenSize.Height; y++)
                {
                    CalculatePixelColor(new PolygonPair(polygon, polygonDerived), new Coordinate(x, y));
                }
            }

            return _bmp;
        }
        
        private Polygon CreateDefaultPolygonForCalculation()
        {
            Polygon polygon = new Polygon();
            polygon.Coefficients.Add(new ComplexNumber { RealPart = 1 });
            polygon.Coefficients.Add(ComplexNumber.Zero);
            polygon.Coefficients.Add(ComplexNumber.Zero);
            polygon.Coefficients.Add(new ComplexNumber { RealPart = 1 });
            return polygon;
        }

        private void CalculatePixelColor(PolygonPair polygons, Coordinate coordinate)
        {
            ComplexNumber complexNumber = CreateComplexNumberByCoordinates(coordinate);
            Polygon polygon = polygons.Polygon;
            Polygon polygonDerived = polygons.PolygonDerived;

            // find solution of equation using newton's iteration
            int iteration = 0;
            for (int i = 0; i < 30; i++)
            {
                var diff = polygon.Eval(complexNumber).Divide(polygonDerived.Eval(complexNumber));
                complexNumber = complexNumber.Subtract(diff);

                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                    i--;

                iteration++;
            }

            int rootNumberId = FindSolutionRootNumber(complexNumber);

            ColorizePixel(rootNumberId, iteration, coordinate);
        }
        
        private int FindSolutionRootNumber(ComplexNumber complexNumber)
        {
            bool known = false;
            int rootNumberId = 0;
            for (int i = 0; i < _roots.Count; i++)
            {
                if (Math.Pow(complexNumber.RealPart - _roots[i].RealPart, 2)
                    + Math.Pow(complexNumber.ImaginaryPart - _roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    known = true;
                    rootNumberId = i;
                }
            }

            if (!known)
            {
                _roots.Add(complexNumber);
                rootNumberId = _roots.Count;
            }

            return rootNumberId;
        }
        
        private ComplexNumber CreateComplexNumberByCoordinates(Coordinate coordinate)
        {
            // find "world" coordinates of pixel
            double coordinateX = _boundaryCoordinates.XMin + coordinate.Y * _boundaryCoordinates.XStep;
            double coordinateY = _boundaryCoordinates.Ymin + coordinate.X * _boundaryCoordinates.YStep;

            ComplexNumber complexNumber = new ComplexNumber
            {
                RealPart = coordinateX,
                ImaginaryPart = coordinateY
            };

            if (complexNumber.RealPart == 0)
                complexNumber.RealPart = 0.0001;

            if (complexNumber.ImaginaryPart == 0)
                complexNumber.ImaginaryPart = 0.0001;
            
            return complexNumber;
        }

        private void ColorizePixel(int rootNumberId, int iteration, Coordinate coordinate)
        {
            Color selectedColor = _colorPalette[rootNumberId % _colorPalette.Length];
            Color adjustedSelectedColor = Color.FromArgb(
                Math.Min(Math.Max(0, selectedColor.R - iteration * 2), 255),
                Math.Min(Math.Max(0, selectedColor.G - iteration * 2), 255),
                Math.Min(Math.Max(0, selectedColor.B - iteration * 2), 255));
            
            _bmp.SetPixel(coordinate.X, coordinate.Y, adjustedSelectedColor);
        }
    }
}