using System;
using RailCargo.HCCM.Events;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Activities
{
    public class ActivityTrainWaitingForDeparture : Activity
    {
        public ActivityTrainWaitingForDeparture(ControlUnit parentControlUnit, string activityType, bool preEmptable) : base(parentControlUnit, activityType, preEmptable)
        {
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
            //NOTHING to do here
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            ActivityTrainDrive trainDrive =
                new ActivityTrainDrive(ParentControlUnit, Constants.ACTIVITY_TRAIN_DRIVE, false);
            EventTrainArrival trainArrival = new EventTrainArrival(EventType.Standalone, ParentControlUnit);
            //TODO get drive time from cu network or bookingsystem
            var timeToDrive = DateTime.Now;
            simEngine.AddScheduledEvent(trainArrival, time);
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