using Shared;
using System;

namespace Hostel.State.Floor.Units
{
    public class BathRoomState : Message, IState<BathRoomState>
    {
        public Guid BathRoomId { get; }
        public string Tag { get; }
        public static readonly BathRoomState Empty = new BathRoomState();
        public BathRoomState Update(IEvent evnt)
        {
            throw new System.NotImplementedException();
        }
    }
}
