using System;
using System.Linq;
using RailCargo.HCCM.ControlUnits;
using SimulationCore.SimulationClasses;
using RailCargo.HCCM.Input;
using RailCargo.HCCM.staticVariables;

namespace RailCargo
{
    public class TrainNetworkSimulation : SimulationModel
    {
        public TrainNetworkSimulation(DateTime startTime, DateTime endTime) : base(startTime, endTime)
        {
            //Defining the inputs
            var inputPath = new InputPath();
            var inputTimeTable = new InputTimeTable();
            // Initialize all control units
            var routingPath = new RoutingPath("CU_RoutingPath", null,
                this, inputPath);
            var shuntingYards = new ShuntingYard[10]; //TODO only test
            for (var i = 0; i < 10; i++)
            {
                shuntingYards[i] = new ShuntingYard(i.ToString(), routingPath, this);
                AllShuntingYards.Instance.SetYards(i.ToString(), shuntingYards[i]);
            }

            var network = new Network("CU_NETWORK", routingPath, this, inputTimeTable);

            var bookingSystem = new BookingSystem("CU_BOOKINGSYSTEM", routingPath, this);
            

            routingPath.SetChildControlUnits(shuntingYards);


            _rootControlUnit = routingPath;
        }

        public override void CustomInitializeModel()
        {
        }

        public override string GetModelString()
        {
            throw new NotImplementedException();
        }
    }
}