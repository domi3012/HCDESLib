using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using RailCargo.HCCM.ControlUnits;
using SimulationCore.SimulationClasses;
using RailCargo.HCCM.Input;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;


namespace RailCargo
{
    public class TrainNetworkSimulation : SimulationModel
    {
        private readonly CuNetwork network;
        private readonly CuBookingSystem bookingSystem;

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
            bookingSystem = new CuBookingSystem("CU_BOOKINGSYSTEM", null, this, inputTimeTable);
            network = new CuNetwork("CU_NETWORK", bookingSystem, this);
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

        public override void CreateSimulationResultsAfterStop()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Use this for EPPlus 5.0 onwards
            using (ExcelPackage package = new ExcelPackage(new FileInfo(@"C:\Users\koenig11\RiderProjects\HCDESLib\RailCargo\result.xlsx")))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                worksheet.Cells[1, 1].Value = "WagonId";
                worksheet.Cells[1, 2].Value = "Timedelta";
                worksheet.Cells[1, 3].Value = "Calculated Time";
                worksheet.Cells[1, 4].Value = "Actual Time";
                var counter = 2;
                foreach (var wagon in bookingSystem.AllWagons)
                {
                    worksheet.Cells[counter, 1].Value = wagon.WagonId.ToString();  // Writes "Hello" to cell A1
                    worksheet.Cells[counter, 2].Value = wagon.TimeDelta.ToString();
                    worksheet.Cells[counter, 3].Value = wagon.EndTime.ToString();
                    worksheet.Cells[counter, 4].Value = wagon.RealTime.ToString();
                    counter += 1;
                }
                package.Save();
            }
        }
    }
}