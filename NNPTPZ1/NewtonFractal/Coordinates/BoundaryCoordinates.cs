namespace NNPTPZ1.NewtonFractal.Coordinates
{
    public class BoundaryCoordinates
    {
        public double XMin { get; }
        public double Ymin { get; }
        
        public double XStep { get; }
        public double YStep { get; }

        public BoundaryCoordinates(double xMin, double ymin, double xStep, double yStep)
        {
            this.XMin = xMin;
            this.Ymin = ymin;
            this.XStep = xStep;
            this.YStep = yStep;
        }
    }
}