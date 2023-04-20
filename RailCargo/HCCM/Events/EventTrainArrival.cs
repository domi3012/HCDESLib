using System;
using RailCargo.HCCM.Entities;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class EventTrainArrival : Event
    {
        private readonly EntityTrain _train;

        public EventTrainArrival(EventType type, ControlUnit parentControlUnit, EntityTrain train) : base(type, parentControlUnit)
        {
            //TODO need to now where we arrived
            _train = train;
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            EventWagonArrival wagonArrival = new EventWagonArrival(EventType.Standalone, ParentControlUnit, _train);
            SequentialEvents.Add(wagonArrival);
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