using System;
using SimulationCore.HCCMElements;
namespace RailCargo.HCCM.Requests
{
    public class RequestForSilo : ActivityRequest
    {
        public RequestForSilo(string activity, Entity train, DateTime time) : base(activity, train, time)
        {
        }
    }
}