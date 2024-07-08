using System;
using System.Collections.Generic;


namespace desktop_calculator
{
    public class ExpressionEvaluator : IExpressionEvaluator
    {
        private readonly IExpressionConvertor _expressionConvertor;
        private readonly AdvanceArithematicOperations _advancedOperations;

        public ExpressionEvaluator()
        {
            _expressionConvertor = new InfixToPostfixConvertor();
            _advancedOperations = new AdvanceArithematicOperations();
        }

        /// <summary>
        /// Initialises the object of ExpressionEvaluator When User want to use the custom Convertor.
        /// </summary>
        /// <param name="expressionConvertor"></param>
        public ExpressionEvaluator(IExpressionConvertor expressionConvertor)
        {
            _expressionConvertor = expressionConvertor;
            _advancedOperations = new AdvanceArithematicOperations();
        }

        /// <summary>
        /// Returns the evaluted expression value and can be overridden by the user if Creating an object through the Custom Expression cunstructor.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual double EvaluateExpression(List<Token> tokens)
        {
            List<Token> postFixedToken = _expressionConvertor.ConvertExpression(tokens);

            Stack<string> stack = new Stack<string>();

            double operand1, operand2, operationResult = 0;
            foreach (Token token in postFixedToken)
            {
                if (token.TokenType.Equals(TokenType.Operand))
                {
                    stack.Push(token.TokenValue);
                }
                else if (token.IsUnary == false)
                {
                    if (stack.Count <= 1)
                    {
                        throw new Exception(ExceptionStrings.Evaluation_Exception);
                    }
                    operand1 = Convert.ToDouble(stack.Pop());
                    operand2 = Convert.ToDouble(stack.Pop());
                    if (token.TokenValue.Equals(Tokens.Multiply))
                    {
                        operationResult = _advancedOperations.Multiply(operand1, operand2);
                    }
                    else if (token.TokenValue.Equals(Tokens.Add))
                    {
                        operationResult = _advancedOperations.Add(operand1, operand2);
                    }
                    else if (token.TokenValue.Equals(Tokens.Subtract))
                    {
                        operationResult = _advancedOperations.Subtract(operand2, operand1);
                    }
                    else if (token.TokenValue.Equals(Tokens.Divide))
                    {
                        operationResult = _advancedOperations.Divide(operand2, operand1);
                    }
                    else if (token.TokenValue.Equals(Tokens.Percentage))
                    {
                        operationResult = _advancedOperations.Percentage(operand1, operand2);
                    }
                    else if (token.TokenValue.Equals(Tokens.Exponent))
                    {
                        operationResult = _advancedOperations.Pow(operand2, operand1);
                    }
                    stack.Push(operationResult.ToString());
                }
                else
                {
                    if (stack.Count < 1)
                    {
                        throw new Exception(ExceptionStrings.Evaluation_Exception);
                    }
                    operand1 = Convert.ToDouble(stack.Pop());

                    if (token.TokenValue.Equals(Tokens.Subtract))
                    {
                        operationResult = _advancedOperations.Negative(operand1);
                    }
                    else if (token.TokenValue.Equals(Tokens.Tan))
                    {
                        operationResult = _advancedOperations.Tan(operand1);
                    }
                    else if (token.TokenValue.Equals(Tokens.Sqrt))
                    {
                        operationResult = _advancedOperations.Sqrt(operand1);
                    }
                    else if (token.TokenValue.Equals(Tokens.Sin))
                    {
                        operationResult = _advancedOperations.Sin(operand1);
                    }
                    else if (token.TokenValue.Equals(Tokens.Cos))
                    {
                        operationResult = _advancedOperations.Cos(operand1);
                    }
                    else if (token.TokenValue.Equals(Tokens.Log))
                    {
                        operationResult = _advancedOperations.Log(operand1);
                    }
                    else if (token.TokenValue.Equals(Tokens.Inv))
                    {
                        operationResult = _advancedOperations.Inv(operand1);
                    }
                    stack.Push(operationResult.ToString());
                }
            }

            return Convert.ToDouble(stack.Pop());
        }


    }

}
