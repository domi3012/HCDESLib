using System;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class EventWagonArrivalInEndDestination : Event
    {
        public EventWagonArrivalInEndDestination(EventType type, ControlUnit parentControlUnit) : base(type, parentControlUnit)
        {
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Event_Wagon_Arrival_In_Enddestination";
        }

        public override Event Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get; }
    }
}