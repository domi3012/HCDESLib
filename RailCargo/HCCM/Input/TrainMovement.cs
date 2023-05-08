using System;
using System.Collections.Generic;
using RailCargo.HCCM.Entities;

namespace RailCargo.HCCM.Input
{
    public class TrainMovement
    {
        public TrainMovement(int id, string trainType, string start, string end, DateTime departure, DateTime arrival, List<String> wagons, bool startingNode)
        {
            Id = id;
            TrainType = trainType;
            Start = start;
            End = end;
            Departure = departure;
            Arrival = arrival;
            Wagons = wagons;
            StartingNode = startingNode;
        }

        public TrainMovement(int id, string trainType, string start, string end, DateTime departure, DateTime arrival, bool startingNode)
        {
            Id = id;
            TrainType = trainType;
            Start = start;
            End = end;
            Departure = departure;
            Arrival = arrival;
            StartingNode = startingNode;
        }

        public int Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public List<String> Wagons { get; set; }
        public string TrainType { get; set; }

        public bool StartingNode { get; set; }
    }
}