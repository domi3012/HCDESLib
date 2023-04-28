using System;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class EventTrainDepartureTimeWillArriveIn : Event
    {
        public EventTrainDepartureTimeWillArriveIn(EventType type, ControlUnit parentControlUnit) : base(type, parentControlUnit)
        {
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Event_Train_Departure_Time_Will_Arrive_In";
        }

        public override Event Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get; }
    }
}