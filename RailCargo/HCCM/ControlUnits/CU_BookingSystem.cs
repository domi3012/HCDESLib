using System;
using System.Collections.Generic;
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
                train.Wagons.ForEach(x => wagons.Add(new EntityWagon(int.Parse(x))));
                
                EntityTrain scheduledEntityTrain = new EntityTrain(train.Id, train.Start, train.End,
                    train.Departure, train.Arrival, wagons);
                //TODO change to actual typ
                EventTrainCreation eventTrainCreation = new EventTrainCreation(this, scheduledEntityTrain, "VBF");
                //TODO change to depatureTime - placeholder
                simEngine.AddScheduledEvent(eventTrainCreation, train.Departure.AddHours(-1));
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