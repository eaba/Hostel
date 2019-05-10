using System.Collections.Immutable;

namespace LogShared
{
    public class Dto
    {
        public Dto(ImmutableDictionary<string, string> payload)
        {
            Payload = payload;
        }
        public ImmutableDictionary<string, string> Payload { get; private set; }
    }
}
