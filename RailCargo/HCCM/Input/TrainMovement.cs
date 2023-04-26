using System;
using System.Collections.Generic;
using RailCargo.HCCM.Entities;

namespace RailCargo.HCCM.Input
{
    public class TrainMovement
    {
        public int Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public List<EntityWagon> Wagons { get; set; }
    }
}