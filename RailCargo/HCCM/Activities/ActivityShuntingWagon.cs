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
        private readonly EntitySilo _silo;

        public ActivityShuntingWagon(ControlUnit parentControlUnit, string activityType, bool preEmptable, EntitySilo silo) : base(
            parentControlUnit, activityType, preEmptable)
        {
            _silo = silo;
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
            //throw new NotImplementedException();
            var checkSiloStatus =
                new RequestCheckSiloStatus(Constants.REQUEST_FOR_SILO_STATUS, _silo, time);
            ParentControlUnit.AddRequest(checkSiloStatus);
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            EventSiloFinished siloFinished = new EventSiloFinished(EventType.Standalone, ParentControlUnit);
            EndEvent.SequentialEvents.Add(siloFinished);
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