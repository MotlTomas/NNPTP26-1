using System.Linq;

namespace NNPTPZ1
{
    class FractalSettings
    {
        // x^3 + 1, from the constant term up
        static readonly double[] DefaultCoefficients = { 1, 0, 0, 1 };

        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }
        public double XMin { get; private set; }
        public double XMax { get; private set; }
        public double YMin { get; private set; }
        public double YMax { get; private set; }
        public string OutputPath { get; private set; }
        public double[] Coefficients { get; private set; }

        /// <summary>
        /// Expects: width height xMin xMax yMin yMax outputPath [coefficients from the constant term up]
        /// </summary>
        public static FractalSettings Parse(string[] args)
        {
            return new FractalSettings
            {
                ImageWidth = int.Parse(args[0]),
                ImageHeight = int.Parse(args[1]),
                XMin = double.Parse(args[2]),
                XMax = double.Parse(args[3]),
                YMin = double.Parse(args[4]),
                YMax = double.Parse(args[5]),
                OutputPath = args[6],
                Coefficients = args.Length > 7
                    ? args.Skip(7).Select(arg => double.Parse(arg)).ToArray()
                    : DefaultCoefficients
            };
        }
    }
}
