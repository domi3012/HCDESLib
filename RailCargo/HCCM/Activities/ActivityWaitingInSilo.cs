using System;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Activities
{
    public class ActivityWaitingInSilo : Activity
    {
        public ActivityWaitingInSilo(ControlUnit parentControlUnit, string activityType, bool preEmptable) : base(parentControlUnit, activityType, preEmptable)
        {
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
            throw new NotImplementedException();
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Constants.ACTIVITY_WAITING_IN_SILO;
        }

        public override Activity Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get; }
    }
}