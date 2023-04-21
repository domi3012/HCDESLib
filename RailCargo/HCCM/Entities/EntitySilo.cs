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
        private int _capacity;
        private List<Activity> _currentActivities;
        private int _currentCapactiy;

        public int Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }

        public int CurrentCapactiy
        {
            get => _currentCapactiy;
            set => _currentCapactiy = value;
        }

        public EntitySilo(string destination, int capacity) : base(++s_identifier)
        {
            _destination = destination;
            _capacity = capacity;
            _currentCapactiy = 0;
        }

        public override string ToString()
        {
            return "EntitySilo" + Identifier;
        }

        public override Entity Clone()
        {
            return new EntitySilo(_destination, _capacity);
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