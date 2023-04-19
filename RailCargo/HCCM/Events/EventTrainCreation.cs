using System;
using RailCargo.HCCM.Activities;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class EventTrainCreation : Event
    {
        private readonly EntityTrain _entityTrain;

        public EventTrainCreation(ControlUnit parentControlUnit, EntityTrain entityTrain) : base(EventType.Standalone, parentControlUnit)
        {
            _entityTrain = entityTrain;
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            AllShuntingYards.Instance.GetYards(_entityTrain.StartLocation)
                .AddRequest(new Requests.RequestForSilo(Constants.REQUEST_FOR_SILO, _entityTrain, time));
            //zug wartet hier
            var waitingForSilo = new ActivitiyWaitingForSilo(ParentControlUnit,
                Constants.ACTIVITY_WAITING_FOR_SILO, true, _entityTrain);
            _entityTrain.AddActivity(waitingForSilo);
            SequentialEvents.Add(waitingForSilo.StartEvent);
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