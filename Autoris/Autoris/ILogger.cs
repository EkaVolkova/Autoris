using Autoris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris
{
    public interface ILogger
    {
        void WriteEvent(Event eventMessage);
        void WriteError(Error errorMessage);
    }
}
