using System;
using RailCargo.HCCM.Activities;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class EventSiloSelection : Event
    {
        private readonly EntityTrain _train;

        public EventSiloSelection(ControlUnit parentControlUnit, EntityTrain train) : base(EventType.Standalone,
            parentControlUnit)
        {
            _train = train;
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            EntitySilo entitySilo = new EntitySilo(_train.EndLocation, Constants.CAPACITY_SILO);
            ParentControlUnit.AddEntity(entitySilo);
            var activityWagonCollection =
                new ActivityWagonCollection(ParentControlUnit, entitySilo, Constants.ACTIVITY_WAGON_COLLECTION, true);
            SequentialEvents.Add(activityWagonCollection.StartEvent);
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