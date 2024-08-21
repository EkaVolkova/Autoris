
using Autoris.BLL.Middleware;

namespace Autoris.BLL.Middleware
{
    public interface ILogger
    {
        void WriteEvent(Event eventMessage);
        void WriteError(Error errorMessage);
    }
}
