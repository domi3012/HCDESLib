using System;
using System.Linq;
using RailCargo.HCCM.ControlUnits;
using SimulationCore.SimulationClasses;
using RailCargo.HCCM.Input;

namespace RailCargo
{
    public class TrainNetworkSimulation : SimulationModel
    {
        public TrainNetworkSimulation(DateTime startTime, DateTime endTime) : base(startTime, endTime)
        {
            var inputTimeTable = new InputTimeTable();
            var network = new Network("CU_NETWORK", null,
                this, inputTimeTable);
            var shuntingYards = new ShuntingYard[10]; //TODO only test
            for (var i = 0; i < 10; i++)
            {
                shuntingYards[i] = new ShuntingYard(i.ToString(), network, this);
            }
            network.SetChildControlUnits(shuntingYards);
            
            _rootControlUnit = network;
            
        }

        public override void CustomInitializeModel()
        {
            throw new NotImplementedException();
        }

        public override string GetModelString()
        {
            throw new NotImplementedException();
        }
    }
}