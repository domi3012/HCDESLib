using System;
using System.Collections.Generic;
using RailCargo.HCCM.Entities;

namespace RailCargo.HCCM.Input
{
    public class TrainMovement
    {
        public TrainMovement(int id, string start, string end, DateTime departure, DateTime arrival, List<String> wagons)
        {
            Id = id;
            Start = start;
            End = end;
            Departure = departure;
            Arrival = arrival;
            Wagons = wagons;
        }

        public TrainMovement(int id, string start, string end, DateTime departure, DateTime arrival)
        {
            Id = id;
            Start = start;
            End = end;
            Departure = departure;
            Arrival = arrival;
        }

        public int Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public List<String> Wagons { get; set; }
    }
}