using System;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Requests;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Activities
{
    public class ActivityDriveToDepartureArea : Activity
    {
        private readonly EntityTrain _train;

        public ActivityDriveToDepartureArea(ControlUnit parentControlUnit, EntityTrain train, string activityType, bool preEmptable) : base(parentControlUnit, activityType, preEmptable)
        {
            _train = train;
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
            //TODO is 10 minutes accurate
            simEngine.AddScheduledEvent(EndEvent, time.AddMinutes(10));
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            //Request for Ausfahrt
            var networkCU = ParentControlUnit.ParentControlUnit;
            RequestForDeparture requestForDeparture =
                new RequestForDeparture(Constants.REQUEST_FOR_DEPARTURE, _train, time);
            networkCU.AddRequest(requestForDeparture);
            var trainWaitingForDeparture =
                new ActivityTrainWaitingForDeparture(networkCU, Constants.ACTIVITY_WAITING_FOR_DEPARTURE, false, _train);
            EndEvent.SequentialEvents.Add(trainWaitingForDeparture.StartEvent);
        }

        public override string ToString()
        {
            return Constants.ACTIVITY_DRIVE_TO_DEPARTURE_AREA;
        }

        public override Activity Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get { return new Entity[] { _train }; } }
    }
}