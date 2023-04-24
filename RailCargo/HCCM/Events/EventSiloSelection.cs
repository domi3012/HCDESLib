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
        private readonly EntitySilo _silo;

        public EntityTrain Train => _train;

        public EntitySilo Silo => _silo;

        public EventSiloSelection(ControlUnit parentControlUnit, EntityTrain train) : base(EventType.Standalone,
            parentControlUnit)
        {
            _train = train;
            _silo = null;
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            var entitySilo = new EntitySilo(_train.EndLocation, Constants.CAPACITY_SILO);
            _silo.Train = _train;
            ParentControlUnit.AddEntity(entitySilo);
            EventFreeSilo freeSilo = new EventFreeSilo(EventType.Standalone, ParentControlUnit, entitySilo);
            SequentialEvents.Add(freeSilo);
            var activityWagonCollection =
                new ActivityWagonCollection(ParentControlUnit, Constants.ACTIVITY_WAGON_COLLECTION, true, entitySilo);
            Train.AddActivity(activityWagonCollection);
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