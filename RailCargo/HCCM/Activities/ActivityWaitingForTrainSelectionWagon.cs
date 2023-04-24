using System;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Activities
{
    public class ActivityWaitingForTrainSelectionWagon : Activity
    {
        private readonly EntityWagon _wagon;

        public ActivityWaitingForTrainSelectionWagon(ControlUnit parentControlUnit, string activityType,
            bool preEmptable, EntityWagon wagon) : base(parentControlUnit, activityType, preEmptable)
        {
            _wagon = wagon;
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
            //throw new NotImplementedException();
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            //throw new NotImplementedException();
            ActivityShuntingWagon shuntingWagon =
                new ActivityShuntingWagon(ParentControlUnit, Constants.ACTIVITY_SHUNTING_WAGON, false, _wagon);
            //TODO change to acutal time
            simEngine.AddScheduledEvent(shuntingWagon.EndEvent, DateTime.Now);
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