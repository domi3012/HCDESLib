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
        private readonly string _endLocation;
        private readonly DateTime _departureTime;

        public string StartLocation => _startLocation;
        

        public string EndLocation => _endLocation;

        public DateTime DepartureTime => _departureTime;

        public DateTime ArrivalTime => _arrivalTime;

        private readonly DateTime _arrivalTime;
        private List<Activity> _currentActivities = new List<Activity>();
        private readonly List<EntityWagon> _wagonList;
        private List<EntityWagon> _actual_wagonList = new List<EntityWagon>();
        //TODO somehow say it is the initial train
        private bool is_Starting_Train = false;

        public bool IsStartingTrain
        {
            get => is_Starting_Train;
            set => is_Starting_Train = value;
        }

        public List<EntityWagon> ActualWagonList
        {
            get => _actual_wagonList;
            set => _actual_wagonList = value;
        }

        private readonly int _id;
        private readonly string _trainType;

        public string TrainType => _trainType;

        public List<EntityWagon> WagonList => _wagonList;

        public EntityTrain(int id, string trainType, string startLocation, string endLocation,
            DateTime departureTime, DateTime arrivalTime, List<EntityWagon> wagonList, bool isStartingTrain) : base(id)
        {
            _id = id;
            _trainType = trainType;
            _startLocation = startLocation;
            _endLocation = endLocation;
            _departureTime = departureTime;
            _arrivalTime = arrivalTime;
            _wagonList = wagonList;
            is_Starting_Train = isStartingTrain;
            if (isStartingTrain) ActualWagonList = wagonList;
        }

        public override string ToString()
        {
            return "EntityTrain";
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