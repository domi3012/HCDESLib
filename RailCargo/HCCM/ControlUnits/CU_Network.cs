using System;
using System.Collections.Generic;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Events;
using RailCargo.HCCM.Input;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.ControlUnits
{
    public class CU_Network : ControlUnit
    {
        //private readonly InputTimeTable _timeTable;
        // TODO change back to InputTimeTable
        private readonly List<Tuple<String, String, int, int>> _timeTable = new List<Tuple<String, String, int, int>>
            { Tuple.Create("1", "2", 1, 2), Tuple.Create("2", "3", 1, 2), Tuple.Create("4", "2", 2, 3) };

        public CU_Network(string name, ControlUnit parentControlUnit, SimulationModel parentSimulationModel,
            InputTimeTable inputTimeTable) : base(name,
            parentControlUnit, parentSimulationModel)
        {
            //_timeTable = inputTimeTable;
        }

        protected override void CustomInitialize(DateTime startTime, ISimulationEngine simEngine)
        {
            Console.WriteLine("We are generating the train creation");
            foreach (var train in _timeTable)
            {
                EntityTrain scheduledEntityTrain = new EntityTrain(train.Item1, train.Item2,
                    train.Item3, train.Item4);
                EventTrainCreation eventTrainCreation = new EventTrainCreation(this, scheduledEntityTrain);
                //TODO change to depatureTime - placeholder
                simEngine.AddScheduledEvent(eventTrainCreation, DateTime.Now);
            }
        }

        protected override bool PerformCustomRules(DateTime time, ISimulationEngine simEngine)
        {
            throw new NotImplementedException();
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