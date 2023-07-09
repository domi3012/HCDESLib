using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Events;
using RailCargo.HCCM.Input;
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
            foreach (var train in _input.Trains)
            {
                //need to initialize list beforehand
                List<EntityWagon> wagons = new List<EntityWagon>();
                train.Wagons.ForEach(x =>
                {
                    var wagonId = Int64.Parse(x.WagonId);
                    var wagonLength = x.WagonLength;
                    var wagonMass = x.WagonMass;
                    var destinationRpc = x.DestinationRpc;
                    var endLocation = x.EndLocation;

                    wagons.Add(new EntityWagon(wagonId, wagonLength, wagonMass, endLocation, destinationRpc));
                });

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
                var startTrain = train.StartTrain;


                var scheduledEntityTrain = new EntityTrain(trainId, startStation, departureTime, arrivalStation,
                    arrivalTime, formationsTime, disassembleTime,
                    rpc_codes, trainWeight, trainLenght, startTrain, wagons);
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