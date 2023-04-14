using System;
using SimulationCore.HCCMElements;
namespace RailCargo.HCCM.Requests
{
    public class RequestForSilo : ActivityRequest
    {
        public RequestForSilo(string activity, Entity[] origin, DateTime time) : base(activity, origin, time)
        {
        }

        public RequestForSilo(string activity, Entity origin, DateTime time) : base(activity, origin, time)
        {
        }
    }
}