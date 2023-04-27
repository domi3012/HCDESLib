using SimulationCore.HCCMElements;
using SimulationCore.MathTool;
using SimulationCore.MathTool.Distributions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RailCargo.HCCM.Input
{
    public class InputTimeTable
    {
        List<TrainMovement> _trains = new List<TrainMovement>();

        public List<TrainMovement> Trains
        {
            get => _trains;
            set => _trains = value;
        }

        public InputTimeTable()
        {
            
            using (StreamReader reader = new StreamReader(@"C:\Users\koenig11\RiderProjects\HCDESLib\RailCargo\HCCM\Data\TimeTable.csv"))
            {
                string line; 

                while ((line = reader.ReadLine()) != null)
                {


                    var result = Regex.Split(line, ",(?![^<]*>)");

                    var id = int.Parse(result[0]);
                    var departure = result[1];
                    var destination = result[2];
                    var departureTime = DateTime.Parse(result[3]);
                    var arrivalTime = DateTime.Parse(result[4]);
                    var wagons = Regex.Split(Regex.Replace(result[5], "<|>", ""), ",");
                    Trains.Add(new TrainMovement(id, departure, destination, departureTime, arrivalTime, wagons.ToList()));

                }
            }
        }
    }
}