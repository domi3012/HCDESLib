using System;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Activities
{
    public class ActivityWagonCollection : Activity
    {
        private readonly EntitySilo _silo;

        public ActivityWagonCollection(ControlUnit parentControlUnit, string activityType, bool preEmptable,
            EntitySilo silo ) : base(parentControlUnit, activityType, preEmptable)
        {
            _silo = silo;
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
            //throw new NotImplementedException();
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            //throw new NotImplementedException();
            Console.WriteLine("BLAAA");
        }

        public override string ToString()
        {
            return Constants.ACTIVITY_WAGON_COLLECTION;
        }

        public override Activity Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get { return new Entity[] { _silo }; } }
    }
}