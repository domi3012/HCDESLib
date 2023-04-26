using System;
using System.Collections.Generic;
using System.Linq;
using RailCargo.HCCM.ControlUnits;
using SimulationCore.SimulationClasses;
using RailCargo.HCCM.Input;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;

namespace RailCargo
{
    public class TrainNetworkSimulation : SimulationModel
    {
        public TrainNetworkSimulation(DateTime startTime, DateTime endTime) : base(startTime, endTime)
        {
            //Defining the inputs
            //var inputPath = new InputPath();
            var inputTimeTable = new InputTimeTable();
            // Initialize all control units
            // var routingPath = new CU_RoutingPath("CU_RoutingPath", null,
            //     this, inputPath);
            var bookingSystem = new CU_BookingSystem("CU_BOOKINGSYSTEM", null, this, inputTimeTable);
            var network = new CU_Network("CU_NETWORK", bookingSystem, this);
            var shuntingYards = new ControlUnit[10]; //TODO only test
            for (var i = 0; i < 10; i++)
            {
                var tmp = new CU_ShuntingYard(i.ToString(), network, this);
                shuntingYards[i] = tmp;
                AllShuntingYards.Instance.SetYards(i.ToString(), tmp);
            }
            
            network.SetChildControlUnits(shuntingYards);

            bookingSystem.SetChildControlUnits(new ControlUnit[] { network });


            _rootControlUnit = bookingSystem;
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