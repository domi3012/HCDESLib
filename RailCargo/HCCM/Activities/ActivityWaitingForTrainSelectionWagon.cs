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
            _wagon.Silo.WagonList.Add(_wagon);
            ActivityShuntingWagon shuntingWagon =
                new ActivityShuntingWagon(ParentControlUnit, Constants.ACTIVITY_SHUNTING_WAGON, false, _wagon);
            //TODO how long does the shunting need?
            simEngine.AddScheduledEvent(shuntingWagon.EndEvent, time.AddMinutes(15));
        }

        public override string ToString()
        {
            return Constants.ACTIVITY_WAITING_FOR_TRAIN_SELECTION_WAGON;
        }

        public override Activity Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get { return new Entity[] { _wagon }; } }
    }
}