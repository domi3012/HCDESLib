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
            List<string> locations = new List<string>();
            var inputTimeTable = new InputTimeTable();
            inputTimeTable.Trains.ForEach(x =>
            {
                locations.Add(x.EndLocation);
                locations.Add(x.StartLocation);
            });
            locations = locations.Distinct().ToList();
            var bookingSystem = new CuBookingSystem("CU_BOOKINGSYSTEM", null, this, inputTimeTable);
            var network = new CuNetwork("CU_NETWORK", bookingSystem, this);
            var shuntingYards = new ControlUnit[locations.Count]; //TODO only test
            var index = 0;
            foreach (var x in locations)
            {
                var tmp = new CuShuntingYard(x, network, this);
                shuntingYards[index++] = tmp;
                AllShuntingYards.Instance.SetYards(x, tmp);
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