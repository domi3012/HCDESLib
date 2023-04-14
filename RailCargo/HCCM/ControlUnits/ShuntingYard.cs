using System;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.ControlUnits
{
    public class ShuntingYard : ControlUnit
    {
        public ShuntingYard(string name, ControlUnit parentControlUnit, SimulationModel parentSimulationModel) : base(name, parentControlUnit, parentSimulationModel)
        {
        }
        
        protected override void CustomInitialize(DateTime startTime, ISimulationEngine simEngine)
        {
            Console.WriteLine(Name);
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