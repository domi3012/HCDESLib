using System;
using RailCargo.HCCM.Activities;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class EventFreeSilo : Event
    {
        private readonly EntitySilo _silo;

        public EventFreeSilo(EventType type, ControlUnit parentControlUnit, EntitySilo silo) : base(type,
            parentControlUnit)
        {
            _silo = silo;
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            var waitingForTrainSelectionSilo = new ActivityWaitingForTrainSelectionSilo(ParentControlUnit,
                Constants.ACTIVITY_WAITING_FOR_TRAIN_SELECTION_SILO, false, _silo);
            _silo.AddActivity(waitingForTrainSelectionSilo);
            // As the train is already selected, we can assign it instantly
            SequentialEvents.Add(waitingForTrainSelectionSilo.EndEvent);
            
        }

        public override string ToString()
        {
            return "Event_Free_Silo";
        }

        public override Event Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get { return new Entity[] { _silo }; } }
    }
}