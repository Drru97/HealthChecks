using System.Collections.Generic;

namespace HealthChecks.Server.Services
{
    public interface ICommandOutputParser
    {
        string Parse(IList<string> lines, string attribute);
    }
}
