using NNPTPZ1.NewtonFractal.Coordinates;
using NNPTPZ1.NewtonFractal.Screen;

namespace NNPTPZ1.NewtonFractal.Settings
{
    /// <summary>
    /// Basic settings for generating Newton fractals.
    /// </summary>
    public class NewtonFractalSettings
    {
        public ScreenSize ScreenSize { get; }
        public BoundaryCoordinates BoundaryCoordinates { get; }

        public NewtonFractalSettings(ScreenSize screenSize, BoundaryCoordinates boundaryCoordinates)
        {
            ScreenSize = screenSize;
            BoundaryCoordinates = boundaryCoordinates;
        }
    }
}