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

        public RenderPixel(string[] args) {

            InputArguments = args;

            bitmap = new Bitmap(int.Parse(InputArguments[0]), int.Parse(InputArguments[0]));

            roots = new List<ComplexNumber>();

            pixelWithCoordinates = new ComplexNumber();

        }

        public void SaveIntoFile()
        {
            bitmap.Save(InputArguments[6] ?? "../../../out.png");
        }





    }
}
