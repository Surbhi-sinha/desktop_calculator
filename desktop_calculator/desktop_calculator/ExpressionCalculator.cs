using System.Collections.Generic;


namespace desktop_calculator
{
    public class ExpressionCalculator
    {
        private readonly Tokeniser _tokeniser;
        private readonly IExpressionEvaluator _expressionEvaluator;

        public ExpressionCalculator()
        {
            _tokeniser = new Tokeniser();
            _expressionEvaluator = new ExpressionEvaluator();
        }

        /// <summary>
        /// Initialises the object of tokeniser and sets the expressionEvaluation to the Custom Evaluator.
        /// </summary>
        /// <param name="customEvalutator"></param>
        public ExpressionCalculator(IExpressionEvaluator customEvalutator)
        {
            _expressionEvaluator = customEvalutator;
            _tokeniser = new Tokeniser();
        }

        /// <summary>
        /// Returns the value of Expression after the evaluation.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public double Evaluate(string expression)
        {
            List<Token> _tokens = _tokeniser.Tokenise(expression);
            return _expressionEvaluator.EvaluateExpression(_tokens);
        }
    }

}
