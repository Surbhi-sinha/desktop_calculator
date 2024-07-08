using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_calculator
{
    public class Tokeniser : ITokeniser<Token>
    {
        public List<Token> Tokenise(string expression)
        {
            char[] expressionChars = expression.ToCharArray();
            List<Token> tokens = new List<Token>();
            try
            {
                for (int i = 0; i < expressionChars.Length; i++)
                {
                    char character = expressionChars[i];
                    if ((character >= Constants.IntChar0 && character <= Constants.IntChar9) || character == Constants.Period)
                    {
                        int j = i, k = i - 1;
                        string number = "";

                        while (j < expression.Length && (char.IsDigit(expression[j]) || expression[j] == Constants.Period))
                        {
                            char current = expression[j];
                            number += current;
                            j++;
                        }
                        i = j - 1;

                        Token token;

                        if (k > 0 && expression[k].Equals(Tokens.Subtract[0]) && expression[k - 1].Equals(Tokens.OpenBracket[0]))
                        {
                            tokens.Remove(tokens[tokens.Count - 1]); //handling the (negative numbers in the expression)
                            token = new Token(TokenType.Operand, Constants.AssociativityNone, Tokens.Subtract + number.ToString(), false);
                        }
                        else
                        {
                            token = new Token(TokenType.Operand, Constants.AssociativityNone, number.ToString(), false);
                        }
                        tokens.Add(token);
                    }
                    else if (Constants.SpecialChars.Contains(character.ToString()))
                    {
                        Token token;
                        if (character.Equals(Tokens.Exponent[0]))
                        {
                            token = new Token(TokenType.Operator, Constants.AssociativityRight, character.ToString(), false); ;
                        }
                        else if (character.Equals(Tokens.Percentage[0]))
                        {
                            token = new Token(TokenType.Operator, Constants.AssociativityLeft, character.ToString(), false);
                        }
                        else if (character.Equals(Tokens.Multiply[0]))
                        {
                            token = new Token(TokenType.Operator, Constants.AssociativityLeft, character.ToString(), false);
                        }
                        else if (character.Equals(Tokens.Divide[0]))
                        {
                            token = new Token(TokenType.Operator, Constants.AssociativityLeft, character.ToString(), false);
                        }
                        else if (character.Equals(Tokens.Add[0]))
                        {
                            token = new Token(TokenType.Operator, Constants.AssociativityLeft, character.ToString(), false);
                        }
                        else if (character.Equals(Tokens.Subtract[0]))
                        {
                            if (i == 0)
                            {
                                token = new Token(TokenType.Operator, Constants.AssociativityLeft, character.ToString(), true);
                            }
                            else
                            {
                                token = new Token(TokenType.Operator, Constants.AssociativityLeft, character.ToString(), false);
                            }
                        }
                        else if (character.Equals(Tokens.OpenBracket[0]))
                        {
                            token = new Token(TokenType.OpenBracket, Constants.AssociativityLeft, character.ToString(), false);
                        }
                        else if (character.Equals(Tokens.CloseBracket[0]))
                        {
                            token = new Token(TokenType.CloseBracket, Constants.AssociativityLeft, character.ToString(), false);
                        }

                        else
                        {
                            throw new Exception(ExceptionStrings.Tokenisation_Exception);
                        }
                        tokens.Add(token);
                    }
                    else if (character >= Constants.LowerCaseA && character <= Constants.LowerCaseZ || character >= Constants.UpperCaseA && character <= Constants.UpperCaseZ)
                    {

                        int j = i + 1;
                        string function = expression[i].ToString();
                        while (!char.IsDigit(expression[j]) && !expression[j].Equals(Tokens.CloseBracket.ToCharArray()[0]) && !expression[j].Equals(Tokens.OpenBracket.ToCharArray()[0]))
                        {
                            char current = expression[j];
                            function += current;
                            j++;
                        }
                        i = j - 1;

                        tokens.Add(new Token(TokenType.Operator, Constants.AssociativityLeft, function, true));

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionStrings.Tokenisation_Exception, ex);
            }
            return tokens;
        }
    }

}
