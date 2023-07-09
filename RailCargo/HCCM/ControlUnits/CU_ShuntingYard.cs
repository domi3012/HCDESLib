using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class CuShuntingYard : ControlUnit
    {
        private Dictionary<string, List<EntitySilo>> _silos = new Dictionary<string, List<EntitySilo>>();

        public CuShuntingYard(string name, ControlUnit parentControlUnit, SimulationModel parentSimulationModel) :
            base(
                name, parentControlUnit, parentSimulationModel)
        {
        }

        protected override void CustomInitialize(DateTime startTime, ISimulationEngine simEngine)
        {
            Console.WriteLine(Name);
        }

        private bool check_rpcs(List<List<string>> rpc_codes, string rpc_code_to_check)
        {
            foreach (var rpc_code in rpc_codes)
            {
                var from = rpc_code[0];
                var until = rpc_code[1];
                if (rpc_code_to_check.Length == 4)
                {
                    var check_rpc = int.Parse(rpc_code_to_check);
                    var from_int = int.Parse(from.Substring(0,4));
                    var until_int = int.Parse(until.Substring(0,4));
                    if (from_int <= check_rpc && check_rpc >= until_int)
                    {
                        return true;
                    }
                }
                from = from.Replace("-", "");
                until = from.Replace("-", "");
                rpc_code_to_check = rpc_code_to_check.Replace("-", "");
                if (int.Parse(from) <= int.Parse(rpc_code_to_check) && int.Parse(rpc_code_to_check) >= int.Parse(until))
                {
                    return true;
                }
            }
            return false;
        }

        protected override bool PerformCustomRules(DateTime time, ISimulationEngine simEngine)
        {
            // A => B
            var requestsForSilo =
                RAEL.Where(p => p.Activity == Constants.RequestForSilo).Cast<RequestForSilo>().ToList();
            foreach (RequestForSilo request in requestsForSilo)
            {
                var siloCreationPossible = true;
                var train = (EntityTrain)request.Origin[0];
                if (siloCreationPossible)
                {
                    //Some need to check which direction the silo has and what about multiple silos in the same direction
                    var siloSelection = new EventSiloSelection(this, train);
                    siloSelection.Trigger(time, simEngine);
                    train.GetCurrentActivities().First().EndEvent.Trigger(time, simEngine);
                    if (!_silos.ContainsKey(train.ArrivalStation))
                    {
                        _silos.Add(train.ArrivalStation, new List<EntitySilo>());
                    }

                    _silos[train.ArrivalStation].Add(siloSelection.Silo);

                    //maybe start wagon collection for train here

                    RemoveRequest(request);
                }
            }

            var requestsForSorting = RAEL.Where(p => p.Activity == Constants.RequestForSorting).Cast<RequestSorting>()
                .ToList();
            foreach (var request in requestsForSorting)
            {
                var wagon = (EntityWagon)request.Origin[0];
                var nextDestination = wagon.EndLocation;
                var wagon_rpc_code = wagon.DestinationRpc;
                foreach (var silos in _silos.Values)
                {
                    var possible_train = silos.First().Train;
                    var availableSilo = check_rpcs(possible_train.RpcCodes, wagon_rpc_code);
                    if (availableSilo)
                    {
                        foreach (var silo in silos)
                        {
                            var maxLength = silo.MaxLength;
                            var maxWeight = silo.MaxWeight;
                            var currentLength = silo.CurrentLength;
                            var currentWeight = silo.CurrentWeight;
                            if (currentLength + float.Parse(wagon.WagonLength, CultureInfo.InvariantCulture.NumberFormat) <= maxLength &&
                                (currentWeight + float.Parse(wagon.WagonMass, CultureInfo.InvariantCulture.NumberFormat)) <= (maxWeight*1000))
                            {
                                wagon.Silo = silo;
                                wagon.StopCurrentActivities(time, simEngine);
                                RemoveRequest(request);
                                break;
                            }
                        }
                    }
                }
            }

            var requestForSiloStatus = RAEL.Where(p => p.Activity == Constants.RequestForSiloStatus)
                .Cast<RequestCheckSiloStatus>().ToList();
            foreach (var request in requestForSiloStatus)
            {
                var silo = (EntitySilo)request.Origin[0];
                var siloMaxWeight = silo.MaxWeight;
                var siloMaxLength = silo.MaxLength;
                var siloCurrentWeight = silo.CurrentWeight;
                var siloCurrentLength = silo.CurrentLength;
                if (silo.Train.isStartTrain)
                {
                    siloCurrentWeight = siloMaxWeight;
                    siloCurrentLength = siloMaxLength;
                }

                
                if (siloCurrentLength == siloMaxLength || siloCurrentWeight == siloMaxLength)
                {
                    silo.StopCurrentActivities(time, simEngine);
                    RemoveRequest(request);
                }
            }

            var requestForDepatureArea = RAEL.Where(p => p.Activity == Constants.RequestForDepartureArea)
                .Cast<RequestForDepartureArea>().ToList();
            foreach (var request in requestForDepatureArea)
            {
                var train = (EntityTrain)request.Origin[0];
                var silo = _silos[train.ArrivalStation].First();
                if (!train.isStartTrain) train.ActualWagonList = silo.WagonList;
                silo.StopCurrentActivities(time, simEngine);
                //TODO only finish first activity
                Helper.Print("Train to " + train.ArrivalStation);

                Helper.Print("Actual list ");
                foreach (var wagon in train.ActualWagonList)
                {
                    Helper.Print(wagon.WagonId.ToString());
                }

                Helper.Print("List ");
                foreach (var wagon in train.Wagons)
                {
                    Helper.Print(wagon.WagonId.ToString());
                }

                train.GetCurrentActivities().First().EndEvent.Trigger(time, simEngine);
                if (_silos[train.ArrivalStation].Count != 0)
                    _silos[train.ArrivalStation].Remove(silo);
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