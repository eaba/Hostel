using Shared;
using System.Collections.Generic;

namespace Hostel.Command.Create
{
    public sealed class CreatePerson : Message, ICommand
    {
        public string Commander { get; }
        public string CommandId { get; }
        public Dictionary<string, string> Payload { get; }
        public string ReplyToQueue { get; }

        public CreatePerson(string replytoqueue, string commander, string commandid, Dictionary<string, string> payload)
        {
            Commander = commander;
            CommandId = commandid;
            Payload = payload;
            ReplyToQueue = replytoqueue;
        }
    }
}
