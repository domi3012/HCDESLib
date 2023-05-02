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
                if (_train.EndLocation == wagon.EndDestination)
                {
                    EventWagonArrivalInEndDestination wagonArrivalInEndDestination =
                        new EventWagonArrivalInEndDestination(EventType.Standalone, ParentControlUnit);
                    SequentialEvents.Add(wagonArrivalInEndDestination);
                    continue;
                }

                var affectedShuntingYard = AllShuntingYards.Instance.GetYards(_train.EndLocation);
                //TODO dont think that we need to make a request as silo are already existing
                // RequestForSilo requestForSilo = new RequestForSilo(Constants.REQUEST_FOR_SILO, wagon, time);
                // affectedShuntingYard.AddRequest(requestForSilo);
                var waitingForTrainSelectionWagon =
                    new ActivityWaitingForTrainSelectionWagon(affectedShuntingYard,
                        Constants.ACTIVITY_WAITING_FOR_TRAIN_SELECTION_WAGON, true, wagon);
                //wagon.AddActivity(waitingForTrainSelectionWagon);
                SequentialEvents.Add(waitingForTrainSelectionWagon.StartEvent);
                //Should wait now in arrival area
                var requestSorting = new RequestSorting(Constants.REQUEST_FOR_SORTING, wagon, time);
                affectedShuntingYard.AddRequest(requestSorting);
            }
        }

        public override string ToString()
        {
            return "Event_Wagon_Arrival";
        }

        public override Event Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get { return new Entity[] { _train }; } }
    }
}