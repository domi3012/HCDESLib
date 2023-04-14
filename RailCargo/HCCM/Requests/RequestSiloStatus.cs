using System;
using SimulationCore.HCCMElements;
namespace RailCargo.HCCM.Requests
{
    public class CheckSiloStatus : ActivityRequest
    {
        public CheckSiloStatus(string activity, Entity[] origin, DateTime time) : base(activity, origin, time)
        {
        }

        public CheckSiloStatus(string activity, Entity origin, DateTime time) : base(activity, origin, time)
        {
        }
    }
}