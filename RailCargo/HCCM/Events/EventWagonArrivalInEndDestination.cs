using System;
using RailCargo.HCCM.Entities;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class EventWagonArrivalInEndDestination : Event
    {
        private readonly EntityWagon _wagon;

        public EventWagonArrivalInEndDestination(EventType type, ControlUnit parentControlUnit, EntityWagon wagon) : base(type, parentControlUnit)
        {
            _wagon = wagon;
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            Console.WriteLine("YEAH you did it");
        }

        public override string ToString()
        {
            return "Event_Wagon_Arrival_In_Enddestination";
        }

        public override Event Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities
        {
            get { return new Entity[] { _wagon }; }
        }
    }
}