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
        private Dictionary<string, EntitySilo> _silos = new Dictionary<string, EntitySilo>();

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
            //TODO what is if silo is needed and the amount can not be defined initially? 
            // A => B
            var requestsForSilo =
                RAEL.Where(p => p.Activity == Constants.REQUEST_FOR_SILO).Cast<RequestForSilo>().ToList();
            foreach (RequestForSilo request in requestsForSilo)
            {
                var train = (EntityTrain)request.Origin[0];
                if (!_silos.ContainsKey(train.EndLocation))
                {
                    //Some need to check which direction the silo has and what about multiple silos in the same direction
                    var siloSelection = new EventSiloSelection(this, train);
                    siloSelection.Trigger(time, simEngine);
                    train.GetCurrentActivities().First().EndEvent.Trigger(time, simEngine);
                    _silos.Add(train.EndLocation, siloSelection.Silo);

                    //maybe start wagon collection for train here

                    RemoveRequest(request);
                }
            }

            var requestsForSorting = RAEL.Where(p => p.Activity == Constants.REQUEST_FOR_SORTING).Cast<RequestSorting>()
                .ToList();
            foreach (var request in requestsForSorting)
            {
                var wagon = (EntityWagon)request.Origin[0];
                var next_destination = wagon.EndDestination;
                if (wagon.IntermediateNodes.Count != 0)
                    next_destination = wagon.IntermediateNodes.First();
                if (_silos.ContainsKey(next_destination) &&
                    _silos[next_destination].WagonList.Count() < _silos[next_destination].MaxCapacity)
                {
                    if (wagon.IntermediateNodes.Count != 0) wagon.IntermediateNodes.RemoveAt(0);
                    wagon.Silo = _silos[next_destination];
                    wagon.StopCurrentActivities(time, simEngine);
                    RemoveRequest(request);
                }
            }

            var requestForSiloStatus = RAEL.Where(p => p.Activity == Constants.REQUEST_FOR_SILO_STATUS)
                .Cast<RequestCheckSiloStatus>().ToList();
            foreach (var request in requestForSiloStatus)
            {
                var silo = (EntitySilo)request.Origin[0];
                var currentQuantity = silo.WagonList.Count;
                if (silo.Train.IsStartingTrain)
                {
                    currentQuantity = silo.Train.WagonList.Count;
                }

                var maxQuantity = silo.MaxCapacity;
                if (maxQuantity == currentQuantity)
                {
                    silo.StopCurrentActivities(time, simEngine);
                    RemoveRequest(request);
                }
            }
            //TODO important what happens if two trains are in same direction 
            var iterateAgain = false;
            var requestForDepatureArea = RAEL.Where(p => p.Activity == Constants.REQUEST_FOR_DEPARTURE_AREA)
                .Cast<RequestForDepartureArea>().ToList();
            foreach (var request in requestForDepatureArea)
            {
                var train = (EntityTrain)request.Origin[0];
                if (!_silos.ContainsKey(train.EndLocation))
                {
                    iterateAgain = true;
                    break;
                }

                if (!train.IsStartingTrain) train.ActualWagonList = _silos[train.EndLocation].WagonList;
                _silos[train.EndLocation].StopCurrentActivities(time, simEngine);
                //TODO only finish first activity
                Helper.print("Train to " + train.EndLocation);
                
                Helper.print("Actual list ");
                foreach (var wagon in train.ActualWagonList)
                {
                    Helper.print(wagon.Identifier.ToString());
                }
                Helper.print("List ");
                foreach (var wagon in train.WagonList)
                {
                    Helper.print(wagon.Identifier.ToString());
                }
                train.GetCurrentActivities().First().EndEvent.Trigger(time, simEngine);
                _silos.Remove(train.EndLocation);
                RemoveRequest(request);
            }

            if (iterateAgain)
            {
                //not sure if allowed
                PerformCustomRules(time, simEngine);
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