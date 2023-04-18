using System;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Events;
using RailCargo.HCCM.Requests;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.MathTool.Distributions;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Activities
{
    public class ActivitiyWaitingForSilo : Activity
    {
        private readonly EntityTrain _entityTrain;

        public ActivitiyWaitingForSilo(ControlUnit parentControlUnit, string activityType, bool preEmptable, Entity train) : base(
            parentControlUnit, activityType, preEmptable)
        {
            _entityTrain = (EntityTrain)train;
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
            // simEngine.AddScheduledEvent(EndEvent,
            //     time + TimeSpan.FromMinutes(Distributions.Instance.Exponential(Constants.TIMETOWAITFORSILO)));
            RequestForSilo requestForSilo = new RequestForSilo(ActivityName, _entityTrain, time);
            ParentControlUnit.AddRequest(requestForSilo);
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            EventSiloSelection eventSiloSelection = new EventSiloSelection(ParentControlUnit);
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override Activity Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get; }
    }
}