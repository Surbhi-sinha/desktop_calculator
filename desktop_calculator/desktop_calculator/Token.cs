
namespace desktop_calculator
{
    public enum TokenType
    {
        Operator, Operand, OpenBracket, CloseBracket
    }
    public class Token
    {

        public TokenType TokenType { get; }
        public char TokenAssociativity { get; }
        public string TokenValue { get; }
        public bool IsUnary { get; }

        public Token(TokenType tokenType, char tokenAssociativity, string tokenValue, bool isBinary)
        {
            TokenType = tokenType;
            TokenAssociativity = tokenAssociativity;
            TokenValue = tokenValue;
            IsUnary = isBinary;
        }
    }

}
