using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RailCargo.HCCM.Activities;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Events;
using RailCargo.HCCM.Input;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.ControlUnits
{
    public class CuBookingSystem : ControlUnit
    {
        private readonly InputTimeTable _input;

        public CuBookingSystem(string name, ControlUnit parentControlUnit, SimulationModel parentSimulationModel,
            InputTimeTable input) : base(name, parentControlUnit, parentSimulationModel)
        {
            _input = input;
        }
            
        protected override void CustomInitialize(DateTime startTime, ISimulationEngine simEngine)
        {
            Console.WriteLine("We are generating the train creation");
            var ticks = 0;
            foreach (var wagon in _input.Wagons)
            {
                var wagonId = Int64.Parse(wagon.WagonId);
                var wagonLength = wagon.WagonLength;
                var wagonMass = wagon.WagonMass;
                var destinationRpc = wagon.DestinationRpc;
                var startLocation = wagon.StartLocation;
                var endLocation = wagon.EndLocation;
                var endTime = wagon.EndTime;
                var acceptanceDate = wagon.AcceptanceDate;
                var wagonEntity = new EntityWagon(wagonId, wagonLength, wagonMass, startLocation, endLocation,
                    destinationRpc, endTime,
                    acceptanceDate);
                var affectedShuntingYard = AllShuntingYards.Instance.GetYards(startLocation);
                var waitingForTrainSelectionWagon =
                    new ActivityWaitingForTrainSelectionWagon(affectedShuntingYard,
                        Constants.ActivityWaitingForTrainSelectionWagon, true, wagonEntity, wagonEntity.AcceptanceDate);
                //wagon.AddActivity(waitingForTrainSelectionWagon);
                //Because request get scheduled differently
                simEngine.AddScheduledEvent(waitingForTrainSelectionWagon.StartEvent, wagonEntity.AcceptanceDate.AddTicks(ticks));
                ticks += 1;
            }

            foreach (var train in _input.Trains)
            {
                //need to initialize list beforehand
                var trainId = train.Id;
                var startStation = train.StartLocation;
                var arrivalStation = train.EndLocation;
                var departureTime = DateTime.Parse(train.DepartureTime);
                var arrivalTime = DateTime.Parse(train.ArrivalTime);
                var formationsTime = train.FormationsTime;
                var disassembleTime = train.DisassembleTime;
                var rpc_codes = train.RpcCodes;
                var trainWeight = train.TrainWeight;
                var trainLenght = train.TrainLength;
                var endTrain = train.EndTrain;
                var append = train.Append;
                var pop = train.Pop;
                var startTrain = train.StartTrain;
                List<EntityWagon> wagons = new List<EntityWagon>();
                train.Wagons.ForEach(x =>
                {
                    var wagonId = Int64.Parse(x.WagonId);
                    var wagonLength = x.WagonLength;
                    var wagonMass = x.WagonMass;
                    var destinationRpc = x.DestinationRpc;
                    var startLocation = x.StartLocation;
                    var endLocation = x.EndLocation;
                    var endTime = x.EndTime;
                    var acceptanceDate = x.AcceptanceDate;

                    wagons.Add(new EntityWagon(wagonId, wagonLength, wagonMass, startLocation, endLocation,
                        destinationRpc, endTime,
                        acceptanceDate));
                });
                var scheduledEntityTrain = new EntityTrain(trainId, startStation, departureTime, arrivalStation,
                    arrivalTime, formationsTime, disassembleTime,
                    rpc_codes, trainWeight, trainLenght, startTrain, endTrain, wagons, append, pop);
                
                addTrainToShuntingYard(startStation, trainId, scheduledEntityTrain);

                var eventTrainCreation = new EventTrainCreation(this, scheduledEntityTrain);

                var trainCreationTime = departureTime.AddHours(-1).AddMinutes(-formationsTime);
                simEngine.AddScheduledEvent(eventTrainCreation, trainCreationTime);

                //Trigger Event for departure time will arrive
                var trainDepartureTimeWillArriveIn =
                    new EventTrainDepartureTimeWillArriveIn(EventType.Standalone, ChildControlUnits.First(),
                        scheduledEntityTrain);

                simEngine.AddScheduledEvent(trainDepartureTimeWillArriveIn, departureTime.AddMinutes(-formationsTime));


                //Event Trigger for time arrived
                EventTrainDepartureTimeArrived trainDepartureTimeArrived =
                    new EventTrainDepartureTimeArrived(EventType.Standalone, ChildControlUnits.First(),
                        scheduledEntityTrain);
                simEngine.AddScheduledEvent(trainDepartureTimeArrived, departureTime);
                // EventTrainDepartureTimeArrived trainDepartureTimeArrived =
                //     new EventTrainDepartureTimeArrived(EventType.Standalone, this.ChildControlUnits[0],
                //         scheduledEntityTrain);
                // simEngine.AddScheduledEvent(trainDepartureTimeArrived, train.Departure);
            }
        }

        private static void addTrainToShuntingYard(string startStation, int trainId, EntityTrain scheduledEntityTrain)
        {
            var affectedShuntingYard = AllShuntingYards.Instance.GetYards(startStation);
            if (!affectedShuntingYard.Trains.ContainsKey(trainId))
            {
                affectedShuntingYard.Trains[trainId] = new List<EntityTrain>();
            }

            affectedShuntingYard.Trains[trainId].Add(scheduledEntityTrain);
        }

        protected override bool PerformCustomRules(DateTime time, ISimulationEngine simEngine)
        {
            //throw new NotImplementedException();
            return false;
        }

        public override Event EntityEnterControlUnit(DateTime time, ISimulationEngine simEngine, Entity entity,
            IDelegate originDelegate)
        {
            throw new NotImplementedException();
        }

        public override void EntityLeaveControlUnit(DateTime time, ISimulationEngine simEngine, Entity entity,
            IDelegate originDelegate)
        {
            throw new NotImplementedException();
        }
    }
}