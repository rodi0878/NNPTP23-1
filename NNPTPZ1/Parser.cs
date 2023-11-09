using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class Parser
    {
        private static int[] intArguments = new int[2];
        private static double[] doubleArguments = new double[4];
        private string[] args;

        public Parser(string[] args) {
            this.args = args;

        }
        public int[] ParseIntArguments()
        {
            for (int i = 0; i < intArguments.Length; i++)
            {
                intArguments[i] = int.Parse(args[i]);
            }
            return intArguments;
        }

        public double[] ParseDoubleArguments()
        {
            for (int i = 0; i < doubleArguments.Length; i++)
            {
                doubleArguments[i] = double.Parse(args[i + 2]);
            }
            return doubleArguments;
        }
    }
}
