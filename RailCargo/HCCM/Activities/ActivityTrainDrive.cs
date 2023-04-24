using System;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Events;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Activities
{
    public class ActivityTrainDrive : Activity
    {
        private readonly EntityTrain _train;

        public ActivityTrainDrive(ControlUnit parentControlUnit, string activityType, bool preEmptable,
            EntityTrain train) : base(parentControlUnit, activityType, preEmptable)
        {
            _train = train;
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
            var destinationTyp = "BB";
            var destination = "Vienna";
            EventTrainArrival trainArrival = new EventTrainArrival(EventType.Standalone, ParentControlUnit, _train,
                destinationTyp, destination);
            //TODO get drive time from cu network or bookingsystem
            var timeToDrive = DateTime.Now;
            simEngine.AddScheduledEvent(trainArrival, time);
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
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