using System;
using System.Collections.Generic;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Entities
{
    public class EntityWagon : Entity, IActiveEntity
    {
        private List<Activity> _currentActivies;
        private EntitySilo _silo = null;

        public EntitySilo Silo
        {
            get => _silo;
            set => _silo = value;
        }

        public EntityWagon(int identifier) : base(identifier)
        {
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
            return _currentActivies;
        }

        public void StopCurrentActivities(DateTime time, ISimulationEngine simEngine)
        { 
            //TODO other function with while should i not delte them
            foreach (var activity in _currentActivies)
            {
                activity.EndEvent.Trigger(time, simEngine);
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
            _currentActivies.Add(activity);
        }

        public void RemoveActivity(Activity activity)
        {
            _currentActivies.Remove(activity);
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