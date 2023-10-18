using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class Render
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public double XMin { get; private set; }
        public double XMax { get; private set; }
        public double YMin { get; private set; }
        public double YMax { get; private set; }
        public string OutputFile { get; private set; }
        public Bitmap Image { get; private set; }
        public double XStep { get; private set; }
        public double YStep { get; private set; }
        public Render(string[] args) {
            Width = int.Parse(args[0]);
            Height = int.Parse(args[1]);
            XMin = double.Parse(args[2]);
            XMax = double.Parse(args[3]);
            YMin = double.Parse(args[4]);
            YMax = double.Parse(args[5]);
            OutputFile = args[6];

            Image = new Bitmap(Width, Height);

            XStep = (XMax - XMin) / int.Parse(args[0]);
            YStep = (YMax - YMin) / int.Parse(args[1]);
        }
    }
}
