using Hostel.Model;
using Shared;
using System;

namespace Hostel.Command
{
    public sealed class ConstructHostel : Message, ICommand
    {
        public Construction Construction { get; }

        public string Commander => Guid.NewGuid().ToString();
        public string CommandId => Guid.NewGuid().ToString();

        public ConstructHostel(Construction construction)
        {
            Construction = construction;
        }
    }
}
