using System;
using RailCargo.HCCM.ControlUnits;
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
            simEngine.AddScheduledEvent(EndEvent, time.AddMinutes(_train.FormationsTime).AddTicks(-1));
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            //Request for Ausfahrt
            //Helper.Print("Calculated wagons " + _train.TrainId.ToString() + " -> " + _train.ArrivalStation);
            if (_train.TrainId == 45854)
            {
                Helper.Print("wtf");
            }
            ((CuShuntingYard)ParentControlUnit).calculatePossibleWagons(_train, _train.Silo);
            _train.ActualWagonList.ForEach(x => x.StopCurrentActivities(time, simEngine));
            //foreach (var wagon in _train.Wagons)
            //{
            //    Helper.Print(wagon.WagonId.ToString());
            //}
            //Helper.Print("Actural wagons "+ _train.TrainId.ToString() + " -> " + _train.ArrivalStation);
            //foreach (var wagon in _train.ActualWagonList)
            //{
            //    Helper.Print(wagon.WagonId.ToString());
            //}
            var networkCu = ParentControlUnit.ParentControlUnit;
            RequestForDeparture requestForDeparture =
                new RequestForDeparture(Constants.RequestForDeparture, _train, time);
            networkCu.AddRequest(requestForDeparture);
            var trainWaitingForDeparture =
                new ActivityTrainWaitingForDeparture(networkCu, Constants.ActivityWaitingForDeparture, false, _train);
            EndEvent.SequentialEvents.Add(trainWaitingForDeparture.StartEvent);
        }

        public override string ToString()
        {
            return Constants.ActivityDriveToDepartureArea;
        }

        public override Activity Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get { return new Entity[] { _train }; } }
    }
}