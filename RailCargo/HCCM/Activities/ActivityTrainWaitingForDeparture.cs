using System;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Events;
using RailCargo.HCCM.Requests;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Activities
{
    public class ActivityTrainWaitingForDeparture : Activity
    {
        private readonly EntityTrain _train;

        public EntityTrain Train => _train;

        public ActivityTrainWaitingForDeparture(ControlUnit parentControlUnit, string activityType, bool preEmptable, EntityTrain train) : base(parentControlUnit, activityType, preEmptable)
        {
            _train = train;
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
            RequestForDeparture requestForDeparture =
                new RequestForDeparture(Constants.REQUEST_FOR_DEPARTURE, _train, time);
            ParentControlUnit.ParentControlUnit.AddRequest(requestForDeparture);
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            var trainDrive =
                new ActivityTrainDrive(ParentControlUnit, Constants.ACTIVITY_TRAIN_DRIVE, false, _train);
            EndEvent.SequentialEvents.Add(trainDrive.StartEvent);
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override Activity Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get; }
    }
}