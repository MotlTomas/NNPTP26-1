using System;
using System.Drawing;
using NNPTPZ1.Mathematics;

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
            var settings = FractalSettings.Parse(args);
            var polynomial = Polynomial.FromRealCoefficients(settings.Coefficients);
            Console.WriteLine(polynomial);
            Console.WriteLine(polynomial.Derive());

            var renderer = new FractalRenderer(polynomial);
            Bitmap fractal = renderer.Render(settings);
            fractal.Save(settings.OutputPath);
        }
    }
}
