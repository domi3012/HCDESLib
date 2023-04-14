using System;
using SimulationCore.HCCMElements;
namespace RailCargo.HCCM.Requests
{
    public class RequestSorting : ActivityRequest
    {
        public RequestSorting(string activity, Entity[] origin, DateTime time) : base(activity, origin, time)
        {
        }

        public RequestSorting(string activity, Entity origin, DateTime time) : base(activity, origin, time)
        {
        }
    }
}