using System;
using RailCargo.HCCM.Events;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.MathTool.Distributions;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Activities
{
    public class WaitingForSilo : Activity
    {
        public WaitingForSilo(ControlUnit parentControlUnit, string activityType, bool preEmptable) : base(
            parentControlUnit, activityType, preEmptable)
        {
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
            simEngine.AddScheduledEvent(EndEvent,
                time + TimeSpan.FromMinutes(Distributions.Instance.Exponential(Constants.TIMETOWAITFORSILO)));
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            SiloSelection siloSelection = new SiloSelection(ParentControlUnit);
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override Activity Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get; }
    }
}