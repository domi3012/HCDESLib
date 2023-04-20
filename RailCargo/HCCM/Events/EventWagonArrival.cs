using System;
using RailCargo.HCCM.Activities;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Requests;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class EventWagonArrival : Event
    {
        private readonly EntityTrain _train;

        public EventWagonArrival(EventType type, ControlUnit parentControlUnit, EntityTrain train) : base(type,
            parentControlUnit)
        {
            _train = train;
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            var wagonList = _train.WagonList;
            foreach (var wagon in wagonList)
            {
                var arrivedInEndDestination = false;
                if (arrivedInEndDestination)
                {
                    EventWagonArrivalInEndDestination wagonArrivalInEndDestination =
                        new EventWagonArrivalInEndDestination(EventType.Standalone, ParentControlUnit);
                    SequentialEvents.Add(wagonArrivalInEndDestination);
                    continue;
                }

                ActivityWaitingForTrainSelectionSilo waitingForTrainSelectionSilo =
                    new ActivityWaitingForTrainSelectionSilo(ParentControlUnit,
                        Constants.ACTIVITY_WAITING_FOR_TRAIN_SELECTION_SILO, true);
                wagon.AddActivity(waitingForTrainSelectionSilo);
                SequentialEvents.Add(waitingForTrainSelectionSilo.StartEvent);

                RequestSorting requestSorting = new RequestSorting(Constants.REQUEST_FOR_SORTING, wagon, time);
                ParentControlUnit.AddRequest(requestSorting);
            }
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