using System.Collections.Generic;

namespace IdentityServer.Host.Commands
{
    public sealed class AddHostelClaim
    {
        public string Command { get; }
        public string Commander { get; }
        public string CommandId { get; }
        public Dictionary<string, string> Payload { get; }
        public AddHostelClaim(string command, string commander, string commandid, Dictionary<string, string> payload)
        {
            Command = command;
            Commander = commander;
            CommandId = commandid;
            Payload = payload;
        }
    }
}
