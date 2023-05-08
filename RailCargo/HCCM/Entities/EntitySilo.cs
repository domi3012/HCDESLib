using System;
using System.Collections.Generic;
using System.Linq;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Entities
{
    public class EntitySilo : Entity, IActiveEntity
    {
        private static int s_identifier;
        private readonly string _destination;
        private int _maxCapacity;
        private List<Activity> _currentActivities = new List<Activity>();
        private List<EntityWagon> _wagonList = new List<EntityWagon>();

        public List<EntityWagon> WagonList
        {
            get => _wagonList;
            set => _wagonList = value;
        }

        private int _currentCapactiy;

        public int MaxCapacity
        {
            get => _maxCapacity;
            set => _maxCapacity = value;
        }

        public int CurrentCapactiy
        {
            get => _currentCapactiy;
            set => _currentCapactiy = value;
        }

        public EntityTrain Train { get; set; }

        public EntitySilo(string destination, int maxCapacity) : base(++s_identifier)
        {
            _destination = destination;
            _maxCapacity = maxCapacity;
            _currentCapactiy = 0;
        }

        public override string ToString()
        {
            return "EntitySilo" + Identifier;
        }

        public override Entity Clone()
        {
            return new EntitySilo(_destination, _maxCapacity);
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