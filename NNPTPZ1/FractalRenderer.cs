using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    class FractalRenderer
    {
        // Iterations of Newton's method to perform for each pixel
        const int NewtonIterations = 30;
        const double LargeStepSquaredThreshold = 0.5;
        const double RootToleranceSquared = 0.01;
        // Offset of the axis to avoid division by zero in Newton's iteration
        const double AxisOffset = 0.0001;
        const int DarkeningPerIteration = 2;

        static readonly Color[] RootColors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        readonly Polynomial polynomial;
        readonly Polynomial derivative;
        readonly List<Complex> roots = new List<Complex>();

        public FractalRenderer(Polynomial polynomial)
        {
            this.polynomial = polynomial;
            derivative = polynomial.Derive();
        }

        public Bitmap Render(FractalSettings settings)
        {
            var bitmap = new Bitmap(settings.ImageWidth, settings.ImageHeight);
            for (int pixelY = 0; pixelY < settings.ImageHeight; pixelY++)
            {
                for (int pixelX = 0; pixelX < settings.ImageWidth; pixelX++)
                {
                    Complex start = ToStartPoint(settings, pixelX, pixelY);
                    var (root, iterations) = FindRoot(start);
                    int rootIndex = GetOrAddRootIndex(root);
                    bitmap.SetPixel(pixelX, pixelY, GetColor(rootIndex, iterations));
                }
            }
            return bitmap;
        }

        static Complex ToStartPoint(FractalSettings settings, int pixelX, int pixelY)
        {
            double xStep = (settings.XMax - settings.XMin) / settings.ImageWidth;
            double yStep = (settings.YMax - settings.YMin) / settings.ImageHeight;
            double x = settings.XMin + pixelX * xStep;
            double y = settings.YMin + pixelY * yStep;

            return new Complex()
            {
                Real = x == 0 ? AxisOffset : x,
                Imaginary = (float)y == 0 ? (float)AxisOffset : (float)y
            };
        }

        (Complex Root, int Iterations) FindRoot(Complex start)
        {
            Complex point = start;
            int iterationCount = 0;
            for (int iteration = 0; iteration < NewtonIterations; iteration++)
            {
                var step = polynomial.Evaluate(point).Divide(derivative.Evaluate(point));
                point = point.Subtract(step);

                if (Math.Pow(step.Real, 2) + Math.Pow(step.Imaginary, 2) >= LargeStepSquaredThreshold)
                {
                    iteration--;
                }
                iterationCount++;
            }
            return (point, iterationCount);
        }

        int GetOrAddRootIndex(Complex root)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(root.Real - roots[i].Real, 2) + Math.Pow(root.Imaginary - roots[i].Imaginary, 2) <= RootToleranceSquared)
                    return i;
            }

            roots.Add(root);
            return roots.Count - 1;
        }

        static Color GetColor(int rootIndex, int iterations)
        {
            Color baseColor = RootColors[rootIndex % RootColors.Length];
            int darkening = iterations * DarkeningPerIteration;
            return Color.FromArgb(
                Darken(baseColor.R, darkening),
                Darken(baseColor.G, darkening),
                Darken(baseColor.B, darkening));
        }

        static int Darken(int channel, int amount) => Math.Max(0, channel - amount);
    }
}
