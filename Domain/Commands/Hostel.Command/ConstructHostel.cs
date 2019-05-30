using Hostel.Model;
using Shared;
using System;

namespace Hostel.Command
{
    public sealed class ConstructHostel : Message, ICommand
    {
        public Construction Construction { get; }
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }

        public ConstructHostel(Construction construction)
        {
            Construction = construction;
            CommandId = Guid.NewGuid().ToString();
            Commander = Guid.NewGuid().ToString();
        }
    }
}
