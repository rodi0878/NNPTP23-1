using System.Drawing;
using NNPTPZ1.NewtonFractal;
using NNPTPZ1.NewtonFractal.Coordinates;
using NNPTPZ1.NewtonFractal.Screen;
using NNPTPZ1.NewtonFractal.Settings;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    public static class Program
    {
        static void Main(string[] args)
        {
            NewtonFractalSettings settings = InitializeNewtonFractalSettings(args);
            NewtonFractal.NewtonFractal newtonFractal = new NewtonFractal.NewtonFractal(settings);
            Bitmap bmp = newtonFractal.Compute();

            bmp.Save(LoadOutputFileName(args) ?? "../../../out.png");
        }

        private static NewtonFractalSettings InitializeNewtonFractalSettings(string[] args)
        {
            ScreenSize screenSize = InitializeScreenSize(args);
            BoundaryCoordinates boundaryCoordinates = InitializeBoundaryCoordinates(LoadInputValues(args), screenSize);
            return new NewtonFractalSettings(screenSize, boundaryCoordinates);
        }

        private static double[] LoadInputValues(string[] args)
        {
            double[] doubleargs = new double[4];
            for (int i = 0; i < doubleargs.Length; i++)
            {
                doubleargs[i] = double.Parse(args[i + 2]);
            }

            return doubleargs;
        }
        
        private static ScreenSize InitializeScreenSize(string[] args)
        {
            return new ScreenSize(int.Parse(args[0]), int.Parse(args[1]));
        }

        private static BoundaryCoordinates InitializeBoundaryCoordinates(double[] doubleargs, ScreenSize screenSize)
        {
            double xmin = doubleargs[0];
            double xmax = doubleargs[1];
            double ymin = doubleargs[2];
            double ymax = doubleargs[3];
            
            double xstep = (xmax - xmin) / screenSize.Width;
            double ystep = (ymax - ymin) / screenSize.Height;
            
            return new BoundaryCoordinates(xmin, ymin, xstep, ystep);
        }

        private static string LoadOutputFileName(string[] args)
        {
            return args[6];
        }
    }
}
