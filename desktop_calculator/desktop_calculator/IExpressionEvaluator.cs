using System.Collections.Generic;

namespace desktop_calculator
{
    public interface IExpressionEvaluator
    {
        /// <summary>
        ///  Returns the evaluted expression value and can be overridden by the user if Creating an object through the Custom Expression cunstructor.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        double EvaluateExpression(List<Token> tokens);
    }

}
