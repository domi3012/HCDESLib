using System;
using System.Collections.Generic;
using System.Linq;
using RailCargo.HCCM.Activities;
using RailCargo.HCCM.Events;
using RailCargo.HCCM.Requests;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.ControlUnits
{
    public class ShuntingYard : ControlUnit
    {
        public ShuntingYard(string name, ControlUnit parentControlUnit, SimulationModel parentSimulationModel) : base(
            name, parentControlUnit, parentSimulationModel)
        {
        }

        protected override void CustomInitialize(DateTime startTime, ISimulationEngine simEngine)
        {
            Console.WriteLine(Name);
        }

        protected override bool PerformCustomRules(DateTime time, ISimulationEngine simEngine)
        {
            List<RequestForSilo> requestsForSilo =
                RAEL.Where(p => p.Activity == Constants.REQUESTFORSILO).Cast<RequestForSilo>().ToList();
            foreach (RequestForSilo request in requestsForSilo)
            {
                RemoveRequest(request);
                WaitingForSilo waitingForSilo = new WaitingForSilo(this,
                    Constants.WAITINGFORSILO, true);
                waitingForSilo.StartEvent.Trigger(time, simEngine);
            }

            return false;
        }

        public override Event EntityEnterControlUnit(DateTime time, ISimulationEngine simEngine, Entity entity,
            IDelegate originDelegate)
        {
            throw new NotImplementedException();
        }

        public override void EntityLeaveControlUnit(DateTime time, ISimulationEngine simEngine, Entity entity,
            IDelegate originDelegate)
        {
            throw new NotImplementedException();
        }
    }
}