using System;
using RailCargo.HCCM.Entities;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class EventSiloSelection : Event
    {
        public EventSiloSelection(ControlUnit parentControlUnit) : base(EventType.Standalone, parentControlUnit)
        {
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            EntitySilo entitySilo = new EntitySilo();
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