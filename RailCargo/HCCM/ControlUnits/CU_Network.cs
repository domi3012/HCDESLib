using System;
using System.Collections.Generic;
using System.Linq;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Events;
using RailCargo.HCCM.Input;
using RailCargo.HCCM.Requests;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.ControlUnits
{
    public class CU_Network : ControlUnit
    {
        //private readonly InputTimeTable _timeTable;
        // TODO change back to InputTimeTable
        // private readonly List<Tuple<String, String, int, int, List<EntityWagon>>> _timeTable = new List<Tuple<String, String, int, int, List<EntityWagon>>>
        //     { Tuple.Create("1", "2", 1, 2, new List<EntityWagon>()), Tuple.Create("2", "3", 1, 2, new List<EntityWagon>()), Tuple.Create("4", "2", 2, 3, new List<EntityWagon>()) };

        public CU_Network(string name, ControlUnit parentControlUnit, SimulationModel parentSimulationModel
            ) : base(name,
            parentControlUnit, parentSimulationModel)
        {
            //_timeTable = inputTimeTable;
        }

        protected override void CustomInitialize(DateTime startTime, ISimulationEngine simEngine)
        {
        }

        protected override bool PerformCustomRules(DateTime time, ISimulationEngine simEngine)
        {
            var requestsForDeparture =
                RAEL.Where(p => p.Activity == Constants.REQUEST_FOR_DEPARTURE).Cast<RequestForDeparture>().ToList();
            foreach (var request in requestsForDeparture)
            {
                //get information from booking system if allowed to drive
                var allowedToDrive = true;
                if (allowedToDrive)
                {
                    ((EntityTrain)request.Origin[0]).StopCurrentActivities(time, simEngine);
                    RemoveRequest(request);
                }
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