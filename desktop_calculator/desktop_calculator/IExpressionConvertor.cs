using System.Collections.Generic;

namespace desktop_calculator
{
    public interface IExpressionConvertor
    {
        List<Token> ConvertExpression(List<Token> token);
    }

}
