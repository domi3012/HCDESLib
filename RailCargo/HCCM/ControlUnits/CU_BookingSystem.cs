using System;
using RailCargo.HCCM.Input;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.ControlUnits
{


    public class CU_BookingSystem : ControlUnit
    {
        private readonly InputTimeTable _input;

        public CU_BookingSystem(string name, ControlUnit parentControlUnit, SimulationModel parentSimulationModel, InputTimeTable input) : base(name, parentControlUnit, parentSimulationModel)
        {
            _input = input;
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