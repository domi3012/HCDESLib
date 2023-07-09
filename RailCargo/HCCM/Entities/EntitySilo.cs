using System;
using System.Collections.Generic;
using System.Linq;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Entities
{
    public class EntitySilo : Entity, IActiveEntity
    {
        private static int _sIdentifier;
        private readonly string _destination;
        private List<Activity> _currentActivities = new List<Activity>();
        private List<EntityWagon> _wagonList = new List<EntityWagon>();

        public List<EntityWagon> WagonList
        {
            get => _wagonList;
            set => _wagonList = value;
        }

        private int _currentCapactiy;
        private readonly int _maxLength;
        private readonly int _maxWeight;
        private readonly int _currentLength;
        private readonly int _currentWeight;

        public string Destination => _destination;

        public int MaxLength => _maxLength;

        public int MaxWeight => _maxWeight;
        
        public int CurrentLength => _currentLength;

        public int CurrentWeight => _currentWeight;


        public EntityTrain Train { get; set; }

        public EntitySilo(string destination, int maxLength, int maxWeight) : base(++_sIdentifier)
        {
            _destination = destination;
            _maxLength = maxLength;
            _maxWeight = maxWeight;
            _currentLength = 0;
            _currentWeight = 0;
        }

        public override string ToString()
        {
            return "EntitySilo" + Identifier;
        }

        public override Entity Clone()
        {
            return new EntitySilo(_destination, _maxLength, _maxWeight);
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