using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Interfaces
{
    public interface IStringTranslater
    {
        string TranslaterIdentifier { get; }

        Task<string> TranslateTo(string input);
    }
}
