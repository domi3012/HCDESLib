using System;
using System.Collections.Generic;
using System.Linq;
using RailCargo.HCCM.Activities;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.Events;
using RailCargo.HCCM.Requests;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.ControlUnits
{
    public class CU_ShuntingYard : ControlUnit
    {
        public CU_ShuntingYard(string name, ControlUnit parentControlUnit, SimulationModel parentSimulationModel) :
            base(
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
                // create when silo is possible to create TODO change to actual number
                var siloCreationPossible = true;
                if (siloCreationPossible)
                {
                    //Some need to check which direction the silo has and what about multiple silos in the same direction
                    var siloSelection = new EventSiloSelection(this, (EntityTrain)request.Origin[0]);
                    siloSelection.Trigger(time, simEngine);
                    ((EntityTrain)request.Origin[0]).StopCurrentActivities(time, simEngine);
                    //maybe start wagon collection for train here
                    
                    //siloSelection.Silo.StopCurrentActivities(time, simEngine); here the only activity is shuntingwagoons
                    RemoveRequest(request);
                }
            }

            var requestsForSorting = RAEL.Where(p => p.Activity == Constants.REQUEST_FOR_SORTING).Cast<RequestSorting>()
                .ToList();
            foreach (var request in requestsForSorting)
            {
                var isSiloAvailable = true;
                if (isSiloAvailable)
                {
                    var wagon = (EntityWagon)request.Origin[0];
                    wagon.StopCurrentActivities(time, simEngine);
                    //var shuntingWagon = new Acitvity
                    // is it allowed to shunt here
                    RemoveRequest(request);
                }
                //Create a request for silo 
            }

            var requestForSiloStatus = RAEL.Where(p => p.Activity == Constants.REQUEST_FOR_SILO_STATUS)
                .Cast<RequestCheckSiloStatus>().ToList();
            foreach (var request in requestForSiloStatus)
            {
                //how to initialy set the wagon to full?
                var silo = (EntitySilo)request.Origin[0];
                var currentQuantity = silo.CurrentCapactiy;
                var maxQuantity = silo.Capacity;
                if (maxQuantity == currentQuantity)
                {
                    silo.StopCurrentActivities(time, simEngine);
                    RemoveRequest(request);
                }
            }
            
            var requestForDepatureArea = RAEL.Where(p => p.Activity == Constants.REQUEST_FOR_DEPARTURE_AREA)
                .Cast<RequestForDepartureArea>().ToList();
            foreach (var request in requestForDepatureArea)
            {
                var train = (EntityTrain)request.Origin[0];
                //TODO only finish first activity
                train.GetCurrentActivities()[0].EndEvent.Trigger(time, simEngine);
                //train.StopCurrentActivities(time, simEngine);
                RemoveRequest(request);
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