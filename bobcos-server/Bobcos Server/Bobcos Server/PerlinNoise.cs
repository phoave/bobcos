using System;

namespace Bobcos_Server
{
    public static class PerlinNoise
    {
        private static readonly Random random = new Random();

        public static double Noise(double x, double y)
        {
            int X = (int)Math.Floor(x) & 255;
            int Y = (int)Math.Floor(y) & 255;
            double xf = x - Math.Floor(x);
            double yf = y - Math.Floor(y);
            double u = Fade(xf);
            double v = Fade(yf);

            int aa = PseudoRandom(X, Y);
            int ab = PseudoRandom(X, Y + 1);
            int ba = PseudoRandom(X + 1, Y);
            int bb = PseudoRandom(X + 1, Y + 1);

            double x1 = Lerp(Grad(aa, xf, yf), Grad(ba, xf - 1, yf), u);
            double x2 = Lerp(Grad(ab, xf, yf - 1), Grad(bb, xf - 1, yf - 1), u);

            return (Lerp(x1, x2, v) + 1) / 2; // Normalize to [0, 1]
        }

        private static double Fade(double t) => t * t * t * (t * (t * 6 - 15) + 10);
        private static double Lerp(double a, double b, double t) => a + t * (b - a);
        private static double Grad(int hash, double x, double y)
        {
            int h = hash & 15;
            double u = h < 8 ? x : y;
            double v = h < 4 ? y : h == 12 || h == 14 ? x : 0;
            return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
        }

        // Generate a pseudo-random number based on coordinates.
        private static int PseudoRandom(int x, int y)
        {
            int hash = (x * 34 + y * 29) % 256;  // Simple hash function
            return (random.Next() + hash) & 15;  // Randomize output and return a value between 0 and 15
        }
    }
}
