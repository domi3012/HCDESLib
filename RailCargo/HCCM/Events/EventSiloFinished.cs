using System;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class EventSiloFinished : Event
    {
        public EventSiloFinished(EventType type, ControlUnit parentControlUnit) : base(type, parentControlUnit)
        {
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override Event Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get; }
    }
}