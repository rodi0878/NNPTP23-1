using System;

namespace NNPTPZ1
{
    public sealed class FractalParameters
    {
        public static FractalParameters FromArguments(CommandLineArguments arguments)
        {
            return new FractalParameters(arguments);
        }
        private FractalParameters(CommandLineArguments arguments)
        {
            Width = arguments.Width;
            Height = arguments.Height;
            MinimumX = arguments.MinimumX;
            MaximumX = arguments.MaximumX;
            MinimumY = arguments.MinimumY;
            MaximumY = arguments.MaximumY;

            StepX = (MaximumX - MinimumX) / Width;
            StepY = (MaximumY - MinimumY) / Height;
        }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public double MinimumX { get; private set; }
        public double MaximumX { get; private set; }
        public double MinimumY { get; private set; }
        public double MaximumY { get; private set; }
        public double StepX { get; private set; }
        public double StepY { get; private set; }
    }
}
