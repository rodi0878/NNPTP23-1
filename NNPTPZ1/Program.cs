namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
         static void Main(string[] args)
        {
            FractalGenerator fractalGenerator = new FractalGenerator(args);
            fractalGenerator.GenerateFractal();

            fractalGenerator.SaveImage();
        }

    }

}
