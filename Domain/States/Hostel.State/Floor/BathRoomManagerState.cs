using Shared;
using System;

namespace Hostel.State.Floor
{
    public class BathRoomManagerState : Message, IState<BathRoomManagerState>
    {
        public Guid FloorId { get; }
        public string FlooTag { get; }
        public static readonly BathRoomManagerState Empty = new BathRoomManagerState();

        public BathRoomManagerState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
