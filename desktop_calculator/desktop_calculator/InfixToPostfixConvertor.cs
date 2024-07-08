using System.Collections.Generic;

namespace desktop_calculator
{
    public class InfixToPostfixConvertor : IExpressionConvertor
    {

        public int OperatorPrecedence(string operatorToken)
        {

            //switch (c)
            //{
            //    case Tokens.Pow :
            //            return 5;  //can not be used because a constant value of type string is expected in the case here it is variable.

            if (operatorToken.Equals(Tokens.Exponent) || operatorToken.Equals(Tokens.Inv) || operatorToken.Equals(Tokens.Sqrt))
            {
                return 5;
            }
            else if (operatorToken.Equals(Tokens.Tan) || operatorToken.Equals(Tokens.Sin) || operatorToken.Equals(Tokens.Cos) || operatorToken.Equals(Tokens.Log))
            {
                return 4;
            }
            else if (operatorToken.Equals(Tokens.Divide) || operatorToken.Equals(Tokens.Multiply))
                return 3;
            else if (operatorToken.Equals(Tokens.Add) || operatorToken.Equals(Tokens.Subtract))
                return 2;
            else if (operatorToken.Equals(Tokens.Percentage))
                return 1;
            else
                return -1;
        }

        public List<Token> ConvertExpression(List<Token> tokens)
        {
            Stack<Token> stack = new Stack<Token>();
            List<Token> result = new List<Token>();

            foreach (Token token in tokens)
            {
                if (token.TokenType.Equals(TokenType.Operand))
                {
                    result.Add(token);
                }
                else if (token.TokenType.Equals(TokenType.OpenBracket))
                {
                    stack.Push(token);
                }
                else if (token.TokenType.Equals(TokenType.CloseBracket))
                {
                    while (stack.Count > 0 && !stack.Peek().TokenType.Equals(TokenType.OpenBracket))
                    {
                        result.Add(stack.Pop());
                    }
                    stack.Pop();
                }
                else if (token.TokenType.Equals(TokenType.Operator))
                {
                    while (stack.Count > 0
                        && (OperatorPrecedence(token.TokenValue) <= OperatorPrecedence(stack.Peek().TokenValue))
                        )
                    {
                        result.Add(stack.Pop());
                    }
                    stack.Push(token);

                }
            }
            while (stack.Count > 0)
            {
                result.Add(stack.Pop());
            }

            return result;
        }

    }

}
