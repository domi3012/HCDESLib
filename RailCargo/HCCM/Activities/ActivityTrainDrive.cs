using System;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Events;
using RailCargo.HCCM.staticVariables;
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
            //TODO change to actual values
            var destinationTyp = "VBF";
            var destination = "Vienna";
            var affectedShuntingYard = AllShuntingYards.Instance.GetYards(_train.EndLocation);
            EventTrainArrival trainArrival = new EventTrainArrival(EventType.Standalone, affectedShuntingYard, _train,
                destinationTyp, _train.EndLocation);
            //TODO get drive time from cu network or bookingsystem
            var timeToDrive = _train.ArrivalTime;
            simEngine.AddScheduledEvent(trainArrival, timeToDrive);
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            Console.WriteLine("test");
        }

        public override string ToString()
        {
            return Constants.ACTIVITY_TRAIN_DRIVE;
        }

        public override Activity Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get { return new Entity[] { _train }; } }
    }
}