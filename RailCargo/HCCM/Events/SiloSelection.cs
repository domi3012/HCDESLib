using System;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class SiloSelection : Event
    {
        public SiloSelection(ControlUnit parentControlUnit) : base(EventType.Standalone, parentControlUnit)
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