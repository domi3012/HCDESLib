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
    public class CU_ShuntingYard : ControlUnit
    {
        public CU_ShuntingYard(string name, ControlUnit parentControlUnit, SimulationModel parentSimulationModel) : base(
            name, parentControlUnit, parentSimulationModel)
        {
        }

        protected override void CustomInitialize(DateTime startTime, ISimulationEngine simEngine)
        {
            Console.WriteLine(Name);
        }

        protected override bool PerformCustomRules(DateTime time, ISimulationEngine simEngine)
        {
            var requestsForSilo =
                RAEL.Where(p => p.Activity == Constants.REQUEST_FOR_SILO).Cast<RequestForSilo>().ToList();
            foreach (RequestForSilo request in requestsForSilo)
            {
                var siloSelection = new EventSiloSelection(this);
                if (true)
                {
                    RemoveRequest(request);
                } //silo zuordnen
            }


            var requestForSiloAssignment =
                RAEL.Where(p => p.Activity == Constants.WAITING_FOR_SILO).Cast<RequestForSilo>().ToList();
            foreach (RequestForSilo request in requestsForSilo)
            {
                RemoveRequest(request);
                //request.
                ActivitiyWaitingForSilo activitiyWaitingForSilo = new ActivitiyWaitingForSilo(this,
                    Constants.WAITING_FOR_SILO, true, request.Origin[0]);
                activitiyWaitingForSilo.StartEvent.Trigger(time, simEngine);
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