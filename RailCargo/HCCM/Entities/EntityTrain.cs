using System;
using SimulationCore.HCCMElements;

namespace RailCargo.HCCM.Entities
{
    public class EntityTrain : Entity
    {
        private readonly string _startLocation;
        private static int s_identifier = 1;
        private readonly string _endLocation;
        private readonly int _departureTime;

        public string StartLocation => _startLocation;

        public static int Identifier1 => s_identifier;

        public string EndLocation => _endLocation;

        public int DepartureTime => _departureTime;

        public int ArrivalTime => _arrivalTime;

        private readonly int _arrivalTime;

        public EntityTrain(String startLocation, String endLocation,
            int departureTime, int arrivalTime) : base(s_identifier++)
        {
            _startLocation = startLocation;
            _endLocation = endLocation;
            _departureTime = departureTime;
            _arrivalTime = arrivalTime;
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

        public override Entity Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}