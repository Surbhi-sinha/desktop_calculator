using System;

namespace desktop_calculator
{
    public class BasicArithematicOperations
    {
        // methods

        /// <summary>
        /// Perform arithematic add operation on two variables
        /// </summary>
        /// <param name="firstVariable"></param>
        /// <param name="secondVariable"></param>
        /// <returns></returns>
        public double Add(double firstVariable, double secondVariable)
        {
            return firstVariable + secondVariable;
        }

        /// <summary>
        ///  Perform arithematic Subtract operation on two variables of type <see cref="T:System.Double" />.
        /// </summary>
        /// <param name="firstVariable"></param>
        /// <param name="secondVariable"></param>
        /// <returns></returns>
        public double Subtract(double firstVariable, double secondVariable)
        {
            return firstVariable - secondVariable;
        }

        /// <summary>
        ///  Perform arithematic multiplication operation on two variables of type <see cref="T:System.Double" />
        /// </summary>
        /// <param name="firstVariable"></param>
        /// <param name="secondVariable"></param>
        /// <returns></returns>
        public double Multiply(double firstVariable, double secondVariable)
        {
            return firstVariable * secondVariable;
        }

        public double Negative(double variable) { return -variable; }


        /// <summary>
        /// Perform arithematic Division operation on two variables of type<see cref="T:System.Double" />.
        /// </summary>
        /// <param name="firstVariable"></param>
        /// <param name="secondVariable"></param>
        /// <returns></returns>
        /// <exception cref="DivideByZeroException"></exception>
        public double Divide(double firstVariable, double secondVariable)
        {
            if (secondVariable == 0) throw new DivideByZeroException();
            return firstVariable / secondVariable;
        }

        /// <summary>
        ///  Perform arithematic Percentage operation on two variables of type<see cref="T:System.Double" />.
        /// </summary>
        /// <param name="firstVariable"></param>
        /// <param name="secondVariable"></param>
        /// <returns></returns>
        public double Percentage(double firstVariable, double secondVariable)
        {
            return 0.01 * firstVariable * secondVariable;
        }

    }

}
