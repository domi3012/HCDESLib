using System;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Events;
using RailCargo.HCCM.Requests;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Activities
{
    public class ActivityShuntingWagon : Activity
    {
        private readonly EntityWagon _wagon;

        public ActivityShuntingWagon(ControlUnit parentControlUnit, string activityType, bool preEmptable, EntityWagon wagon) : base(
            parentControlUnit, activityType, preEmptable)
        {
            _wagon = wagon;
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            //_wagon.Silo.CurrentCapactiy++;
            _wagon.Silo.WagonList.Add(_wagon);
            var waitingInSilo =
                new ActivityWaitingInSilo(ParentControlUnit, Constants.ACTIVITY_WAITING_IN_SILO, true, _wagon);
            EndEvent.SequentialEvents.Add(waitingInSilo.StartEvent);
        }

        public override string ToString()
        {
            return Constants.ACTIVITY_SHUNTING_WAGON;
        }

        public override Activity Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get { return new Entity[] { _wagon }; } }
    }
}