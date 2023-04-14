using System;
using RailCargo.HCCM.Input;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.ControlUnits
{
    public class Network : ControlUnit
    {
        private readonly InputTimeTable _inputData;

        public Network(string name, ControlUnit parentControlUnit, SimulationModel parentSimulationModel,
            InputTimeTable inputTimeTable) : base(name, parentControlUnit, parentSimulationModel)
        {
            _inputData = inputTimeTable;
        }

        protected override void CustomInitialize(DateTime startTime, ISimulationEngine simEngine)
        {
            Console.WriteLine("BIBIBU");
        }

        protected override bool PerformCustomRules(DateTime time, ISimulationEngine simEngine)
        {
            throw new NotImplementedException();
        }

        public override Event EntityEnterControlUnit(DateTime time, ISimulationEngine simEngine, Entity entity, IDelegate originDelegate)
        {
            throw new NotImplementedException();
        }

        public override void EntityLeaveControlUnit(DateTime time, ISimulationEngine simEngine, Entity entity, IDelegate originDelegate)
        {
            throw new NotImplementedException();
        }
    }
}