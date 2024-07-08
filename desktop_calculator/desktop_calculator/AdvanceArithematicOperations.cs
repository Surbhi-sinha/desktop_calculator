using System;

namespace desktop_calculator
{
    public class AdvanceArithematicOperations : BasicArithematicOperations
    {
        /// <summary>
        /// Returns the trignometric sine of the specified angle.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Sin(double x)
        {
            return Math.Sin(x);
        }

        /// <summary>
        /// Returns the trignometric cosine of the specified angle.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Cos(double x)
        {
            return Math.Cos(x);
        }

        /// <summary>
        ///  Returns the trignometric tangent of the specified angle.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Tan(double x)
        {
            return Math.Tan(x);
        }

        /// <summary>
        ///  Returns the natural (base e) logarithm of a specified number.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Log(double x)
        {
            return Math.Log(x);
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double Pow(double x, double y)
        {
            return (double)Math.Pow(x, y);
        }

        /// <summary>
        /// Returns inverse of the number.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Inv(double x)
        {
            return 1 / x;
        }

        /// <summary>
        /// Returns the square root of a specified number.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Sqrt(double x)
        {
            return Math.Sqrt(x);
        }

    }
}

