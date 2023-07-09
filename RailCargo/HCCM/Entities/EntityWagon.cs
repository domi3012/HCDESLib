using System;
using System.Collections.Generic;
using System.Linq;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Entities
{
    public class EntityWagon : Entity, IActiveEntity
    {
        private static int _sIdentifier = 0;
        private List<Activity> _currentActivies = new List<Activity>();
        private EntitySilo _silo = null;
        private readonly string _wagonLength;
        private readonly string _wagonMass;
        private readonly string _destinationRpc;
        private readonly string _endLocation;

        public string WagonLength => _wagonLength;

        public string WagonMass => _wagonMass;

        public long WagonId => _wagonId;

        private readonly long _wagonId;

        

        public EntitySilo Silo
        {
            get => _silo;
            set => _silo = value;
        }

        public EntityWagon(long wagonId, string wagonLength, string wagonMass,string endLocation, string destinationRcp) : base(_sIdentifier++)
        {
            _wagonId = wagonId;
            _wagonLength = wagonLength;
            _wagonMass = wagonMass;
            _endLocation = endLocation;
            _destinationRpc = destinationRcp;
        }

        public List<Activity> CurrentActivies => _currentActivies;

        public string DestinationRpc => _destinationRpc;

        public string EndLocation => _endLocation;

        public override string ToString()
        {
            return "Entity_Wagon";
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
            while (_currentActivies.Count > 0)
            {
                _currentActivies.First().EndEvent.Trigger(time, simEngine);
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