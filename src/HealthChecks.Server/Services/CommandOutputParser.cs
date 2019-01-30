using System.Collections.Generic;
using System.Linq;

namespace HealthChecks.Server.Services
{
    public class CommandOutputParser : ICommandOutputParser
    {
        public string Parse(IList<string> lines, string attribute)
        {
            var matchingLine = lines.FirstOrDefault(l => lines.Contains(attribute));

            return string.IsNullOrWhiteSpace(matchingLine) ? "0" : matchingLine;
        }
    }
}
