using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace NNPTPZ1.Fractal
{
    public static class FractalGenerator
    {
        private const int MaxIterations = 30;
        private const double DifferenceRetryThreshold = 0.5;
        private const double RootDistanceThreshold = 0.0001;

        private static readonly Color[] Palette = new[]
        {
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.Yellow,
            Color.Orange,
            Color.Fuchsia,
            Color.Gold,
            Color.Cyan,
            Color.Magenta
        };
        public static Bitmap GenerateFractal(FractalParameters parameters)
        {
            Bitmap result = new Bitmap(parameters.Width, parameters.Height);
            List<Complex> distinctRoots = new List<Complex>();
            Polynomial polynomial = new Polynomial(Complex.One, Complex.Zero, Complex.Zero, Complex.One);
            Polynomial derivative = polynomial.GetDerivative();
#if DEBUG
            var st = new Stopwatch();
            Debug.WriteLine("Began generating fractal...");
            st.Start();
#endif
            var calculatedResults = new Color[parameters.Width, parameters.Height];
            ReaderWriterLockSlim rootLock = new ReaderWriterLockSlim();
            Parallel.For(0, parameters.Width, pixelX =>
            {
                for (int pixelY = 0; pixelY < parameters.Height; pixelY++)
                {
                    Complex point = ConvertPixelToComplexCoordinate(parameters, pixelX, pixelY);
                    (Complex newPoint, int iteration) = NewtonIterationFindRoot(polynomial, derivative, point);
                    int rootIndex = GetCreateRootIndex(distinctRoots, newPoint, rootLock);
                    Color rootColor = Palette[rootIndex % Palette.Length];
                    calculatedResults[pixelX, pixelY] = GetAdjustedColor(rootColor, iteration);
                }
            });
#if DEBUG
            st.Stop();
            Debug.WriteLine("Finished calculating fractal in {0} ms.", st.ElapsedMilliseconds);
            Debug.WriteLine("Began drawing fractal.");
            st.Restart();
#endif
            for (int pixelX = 0; pixelX < parameters.Width; pixelX++)
            {
                for (int pixelY = 0; pixelY < parameters.Height; pixelY++)
                {
                    result.SetPixel(pixelX, pixelY, calculatedResults[pixelX, pixelY]);
                }
            }
            Debug.WriteLine("Finished drawing fractal in {0} milliseconds.", st.ElapsedMilliseconds);
            return result;
        }
        private static Complex ConvertPixelToComplexCoordinate(FractalParameters parameters, int pixelX, int pixelY)
        {
            double x = parameters.MinimumX + pixelY * parameters.StepX;
            double y = parameters.MinimumY + pixelX * parameters.StepY;
            if (x == 0)
            {
                x = 0.0001;
            }
            if (y == 0)
            {
                y = 0.0001;
            }
            return new Complex(x, y);
        }
        private static (Complex point, int iteration) NewtonIterationFindRoot(Polynomial polynomial, Polynomial derivative, Complex point)
        {
            int iterationAttempts = 0;
            for (int i = 0; i < MaxIterations; i++)
            {
                Complex functionValue = polynomial.Evaluate(point);
                Complex derivativeValue = derivative.Evaluate(point);
                Complex difference = functionValue / derivativeValue;
                point -= difference;
                bool retryIteration = difference.GetSquaredMagnitude() >= DifferenceRetryThreshold;
                if (retryIteration)
                {
                    i--;
                }
                iterationAttempts++;
            }
            return (point, iterationAttempts);
        }
        private static int GetCreateRootIndex(List<Complex> distinctRoots, Complex point, ReaderWriterLockSlim rootLock)
        {
            rootLock.EnterReadLock();
            int rootIndex = -1;
            int searchedRootCount = 0;
            try
            {
                for (int i = 0; i < distinctRoots.Count; i++)
                {
                    Complex difference = point - distinctRoots[i];
                    if (difference.GetSquaredMagnitude() < RootDistanceThreshold)
                    {
                        rootIndex = i;
                    }
                    searchedRootCount++;
                }
            }
            finally
            {
                rootLock.ExitReadLock();
            }
            if (rootIndex != -1)
            {
                return rootIndex;
            }
            return AppendRootIfNecessary(distinctRoots, point, rootLock, searchedRootCount);
        }
        private static int AppendRootIfNecessary(List<Complex> distinctRoots, Complex point, ReaderWriterLockSlim rootLock, int searchedRootCount)
        {
            rootLock.EnterWriteLock();
            int rootIndex = -1;
            try
            {
                // another thread may have appended a root while we were acquiring the write lock
                // since our collection can only grow, we may resume the search past the last visited index
                for (int i = searchedRootCount; i < distinctRoots.Count; i++)
                {
                    Complex difference = point - distinctRoots[i];
                    if (difference.GetSquaredMagnitude() < RootDistanceThreshold)
                    {
                        rootIndex = i;
                    }
                }
                if (rootIndex == -1)
                {
                    distinctRoots.Add(point);
                    rootIndex = distinctRoots.Count - 1;
                }
            }
            finally
            {
                rootLock.ExitWriteLock();
            }
            return rootIndex;
        }
        private static Color GetAdjustedColor(Color rootColor, int iteration)
        {
            Color adjustedColor = Color.FromArgb(
                Math.Min(Math.Max(0, rootColor.R - iteration * 2), 255),
                Math.Min(Math.Max(0, rootColor.G - iteration * 2), 255),
                Math.Min(Math.Max(0, rootColor.B - iteration * 2), 255));
            return adjustedColor;
        }
    }
}

