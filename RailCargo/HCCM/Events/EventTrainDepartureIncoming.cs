using System;
using RailCargo.HCCM.Activities;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Requests;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class EventTrainDepartureIncoming : Event
    {
        private readonly EntityTrain _train;

        public EventTrainDepartureIncoming(EventType type, EntityTrain train, ControlUnit parentControlUnit) : base(
            type, parentControlUnit)
        {
            _train = train;
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            ActivityDriveToDepartureArea driveToDepartureArea =
                new ActivityDriveToDepartureArea(ParentControlUnit, _train, Constants.ACTIVITY_DRIVE_TO_DEPARTURE_AREA, false);
            SequentialEvents.Add(driveToDepartureArea.StartEvent);
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