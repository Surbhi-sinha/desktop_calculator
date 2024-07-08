using System.Collections.Generic;

namespace desktop_calculator
{
    public interface ITokeniser<T>
    {
        List<T> Tokenise(string expression);
    }

}
