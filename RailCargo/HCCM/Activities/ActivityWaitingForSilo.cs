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
    public class ActivityWaitingForSilo : Activity
    {
        private readonly EntityTrain _entityTrain;

        public ActivityWaitingForSilo(ControlUnit parentControlUnit, string activityType, bool preEmptable,
            Entity train) : base(
            parentControlUnit, activityType, preEmptable)
        {
            _entityTrain = (EntityTrain)train;
        }

        public override void StateChangeStartEvent(DateTime time, ISimulationEngine simEngine)
        {
        }

        public override void StateChangeEndEvent(DateTime time, ISimulationEngine simEngine)
        {
            //EventSiloSelection eventSiloSelection = new EventSiloSelection(ParentControlUnit);
            Console.WriteLine("Received successfully a silo");
        }

        public override string ToString()
        {
            return Constants.ACTIVITY_WAITING_FOR_SILO;
        }

        public override Activity Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities
        {
            get { return new Entity[] { _entityTrain }; }
        }
    }
}