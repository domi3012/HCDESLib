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
    public class CU_BookingSystem : ControlUnit
    {
        private readonly InputTimeTable _input;

        public CU_BookingSystem(string name, ControlUnit parentControlUnit, SimulationModel parentSimulationModel,
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
                    Match match = Regex.Match(x, @"\((\d+);(.*)\)");
                    var id = int.Parse(match.Groups[1].Value);
                    List<string> nodes = new List<string>(match.Groups[2].Value.Split(','));
                    var last_node = nodes.Last();
                    nodes.Remove(last_node);
                    if (nodes.Count != 0)
                    {
                        nodes.RemoveAt(0);
                    }
                    wagons.Add(new EntityWagon(id, nodes, last_node));
                });

                EntityTrain scheduledEntityTrain = new EntityTrain(train.Id, train.TrainType, train.Start, train.End,
                    train.Departure, train.Arrival, wagons, train.StartingNode);
                EventTrainCreation eventTrainCreation = new EventTrainCreation(this, scheduledEntityTrain, "VBF");
                //TODO change to depatureTime - placeholder
                simEngine.AddScheduledEvent(eventTrainCreation, train.Departure.AddHours(-1));
                
                //Trigger Event for departure time will arrive
                var trainDepartureTimeWillArriveIn =
                    new EventTrainDepartureTimeWillArriveIn(EventType.Standalone, ChildControlUnits.First(), scheduledEntityTrain);
                
                simEngine.AddScheduledEvent(trainDepartureTimeWillArriveIn, train.Departure.AddMinutes(-30));
                
                
                //Event Trigger for time arrived
                EventTrainDepartureTimeArrived trainDepartureTimeArrived =
                    new EventTrainDepartureTimeArrived(EventType.Standalone, ChildControlUnits.First(),
                        scheduledEntityTrain);
                simEngine.AddScheduledEvent(trainDepartureTimeArrived, train.Departure);
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