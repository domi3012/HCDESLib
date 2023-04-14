using System;
using SimulationCore.HCCMElements;
namespace RailCargo.HCCM.Requests
{
    public class DepartureAllowed : ActivityRequest
    {
        public DepartureAllowed(string activity, Entity[] origin, DateTime time) : base(activity, origin, time)
        {
        }

        public DepartureAllowed(string activity, Entity origin, DateTime time) : base(activity, origin, time)
        {
        }
    }
}