using System;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class TrainCreation : Event
    {
        private readonly Train _train;

        public TrainCreation(ControlUnit parentControlUnit, Train train) : base(EventType.Standalone, parentControlUnit)
        {
            _train = train;
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            AllShuntingYards.Instance.GetYards(_train.StartLocation)
                .AddRequest(new Requests.RequestForSilo(Constants.REQUESTFORSILO, _train, time));
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override Event Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities { get; }
    }
}