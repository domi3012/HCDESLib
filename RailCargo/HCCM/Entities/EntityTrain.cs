using System;
using System.Collections.Generic;
using System.Linq;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Entities
{
    public class EntityTrain : Entity, IActiveEntity
    {
        private readonly string _startLocation;
        private static int _identifier = 1;
        private readonly string _endLocation;
        private readonly int _departureTime;

        public string StartLocation => _startLocation;

        public static int Identifier => _identifier;

        public string EndLocation => _endLocation;

        public int DepartureTime => _departureTime;

        public int ArrivalTime => _arrivalTime;

        private readonly int _arrivalTime;
        private List<Activity> _currentActivities;
        private readonly List<EntityWagon> _wagonList;

        public List<EntityWagon> WagonList => _wagonList;

        public EntityTrain(String startLocation, String endLocation,
            int departureTime, int arrivalTime, List<EntityWagon> wagonList) : base(_identifier++)
        {
            _startLocation = startLocation;
            _endLocation = endLocation;
            _departureTime = departureTime;
            _arrivalTime = arrivalTime;
            _wagonList = wagonList;

        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

        public override Entity Clone()
        {
            throw new System.NotImplementedException();
        }

        public List<Activity> GetCurrentActivities()
        {
            return _currentActivities;
        }

        public void StopCurrentActivities(DateTime time, ISimulationEngine simEngine)
        {
            while (_currentActivities.Count > 0)
            {
                _currentActivities.First().EndEvent.Trigger(time, simEngine);
            }
        }

        public void StopWaitingActivity(DateTime time, ISimulationEngine simEngine)
        {
            throw new NotImplementedException();
        }

        public Event StartWaitingActivity(IDynamicHoldingEntity waitingArea = null)
        {
            throw new NotImplementedException();
        }

        public void AddActivity(Activity activity)
        {
            _currentActivities.Add(activity);
        }

        public void RemoveActivity(Activity activity)
        {
            _currentActivities.Remove(activity);
        }

        public bool IsWaiting()
        {
            throw new NotImplementedException();
        }

        public bool IsWaitingOrPreEmptable()
        {
            throw new NotImplementedException();
        }

        public bool IsInOnlyActivity(string activity)
        {
            throw new NotImplementedException();
        }
    }
}