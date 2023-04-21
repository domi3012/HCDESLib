using System;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Activities
{
    public class ActivityWaitingForTrainSelectionSilo : Activity 
    {
        private readonly EntitySilo _silo;

        public ActivityWaitingForTrainSelectionSilo(ControlUnit parentControlUnit, string activityType, bool preEmptable, EntitySilo silo) : base(parentControlUnit, activityType, preEmptable)
        {
            _silo = silo;
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
            //throw new NotImplementedException();
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            ActivityShuntingWagon shuntingWagon =
                new ActivityShuntingWagon(ParentControlUnit, Constants.ACTIVITY_SHUNTING_WAGON, false, _silo);
            _silo.AddActivity(shuntingWagon);
            EndEvent.SequentialEvents.Add(shuntingWagon.StartEvent);
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