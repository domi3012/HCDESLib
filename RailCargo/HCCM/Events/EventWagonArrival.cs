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
            var wagonList = _train.ActualWagonList;
            foreach (var wagon in wagonList)
            {
                if (_train.ArrivalStation == wagon.EndLocation)
                {
                    EventWagonArrivalInEndDestination wagonArrivalInEndDestination =
                        new EventWagonArrivalInEndDestination(EventType.Standalone, ParentControlUnit, wagon);
                    SequentialEvents.Add(wagonArrivalInEndDestination);
                    continue;
                }

                var affectedShuntingYard = AllShuntingYards.Instance.GetYards(_train.ArrivalStation);
                //TODO dont think that we need to make a request as silo are already existing
                // RequestForSilo requestForSilo = new RequestForSilo(Constants.REQUEST_FOR_SILO, wagon, time);
                // affectedShuntingYard.AddRequest(requestForSilo);
                var waitingForTrainSelectionWagon =
                    new ActivityWaitingForTrainSelectionWagon(affectedShuntingYard,
                        Constants.ActivityWaitingForTrainSelectionWagon, true, wagon);
                //wagon.AddActivity(waitingForTrainSelectionWagon);
               simEngine.AddScheduledEvent(waitingForTrainSelectionWagon.StartEvent, time.AddMinutes(_train.DisassembleTime));

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