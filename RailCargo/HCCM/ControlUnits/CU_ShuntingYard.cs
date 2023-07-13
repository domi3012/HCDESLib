using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private Dictionary<int, List<EntityTrain>> _trains = new Dictionary<int, List<EntityTrain>>();
        private Dictionary<string, List<EntityWagon>> _waitingWagons = new Dictionary<string, List<EntityWagon>>();

        public Dictionary<int, List<EntityTrain>> Trains
        {
            get => _trains;
            set => _trains = value;
        }

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
                var from = rpc_code[0].Replace("-", "");
                var until = rpc_code[1].Replace("-", "");
                if (rpc_code_to_check.Length == 4)
                {
                    var check_rpc = int.Parse(rpc_code_to_check);
                    var from_int = int.Parse(from.Substring(0, 4));
                    var until_int = int.Parse(until.Substring(0, 4));
                    if (from_int <= check_rpc && check_rpc >= until_int)
                    {
                        return true;
                    }
                }

                rpc_code_to_check = rpc_code_to_check.Replace("-", "");
                if (int.Parse(from) <= int.Parse(rpc_code_to_check) && int.Parse(rpc_code_to_check) >= int.Parse(until))
                {
                    return true;
                }
            }

            return false;
        }

        // TODO if to much wagon in silo + train the wagon
        // get shifted through code to a silo which is created later in time at the first
        // location to contain FIFO
        protected override bool PerformCustomRules(DateTime time, ISimulationEngine simEngine)
        {
            var requestForTrainContinuation =
                RAEL.Where(p => p.Activity == Constants.RequestTrainContinuation).Cast<RequestTrainContinuation>()
                    .ToList();
            foreach (var request in requestForTrainContinuation)
            {
                var train = (EntityTrain)request.Origin[0];
                var trainId = train.TrainId;
                var possibleTrains = (Trains.ContainsKey(trainId)) ? Trains[trainId] : null;
                if (possibleTrains != null)
                {
                    possibleTrains = possibleTrains.Where(x => Math.Abs((x.DeparturTime - train.ArrivalTime).TotalDays) < 1).ToList();
                }
                RemoveRequest(request);
                if (possibleTrains != null && possibleTrains.Count == 0) continue;
                var possibleTrain = possibleTrains?[0];
                foreach (var wagon in train.ActualWagonList)
                {
                    if (wagon.EndLocation == Name)
                    {
                        EventWagonArrivalInEndDestination wagonArrivalInEndDestination =
                            new EventWagonArrivalInEndDestination(EventType.Standalone, ParentControlUnit, wagon);
                        wagonArrivalInEndDestination.Trigger(time, simEngine);
                        continue;
                    }

                    if (possibleTrain != null && check_rpcs(possibleTrain.RpcCodes, wagon.DestinationRpc))
                    {
                        possibleTrain.ActualWagonList.Add(wagon);
                        wagon.CurrentTrain = possibleTrain;
                        continue;
                    }
                    var waitingForTrainSelectionWagon =
                        new ActivityWaitingForTrainSelectionWagon(this,
                            Constants.ActivityWaitingForTrainSelectionWagon, true, wagon, time);
                    simEngine.AddScheduledEvent(waitingForTrainSelectionWagon.StartEvent,
                        time.AddMinutes(train.DisassembleTime));
                }
            }

            var requestsForSilo =
                RAEL.Where(p => p.Activity == Constants.RequestForSilo).Cast<RequestForSilo>().ToList();
            foreach (RequestForSilo request in requestsForSilo)
            {
                var siloCreationPossible = true;
                var train = (EntityTrain)request.Origin[0];
                if (siloCreationPossible)
                {
                    //Some need to check which direction the silo has and what about multiple silos in the same direction
                    var waitingWagons = new List<EntityWagon>();
                    if (_waitingWagons.ContainsKey(train.ArrivalStation))
                    {
                        waitingWagons = _waitingWagons[train.ArrivalStation];
                        _waitingWagons.Remove(train.ArrivalStation);
                    }

                    var siloSelection = new EventSiloSelection(this, train, waitingWagons);
                    siloSelection.Trigger(time, simEngine);
                    //train.GetCurrentActivities().First().EndEvent.Trigger(time, simEngine);
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
                var wagon_rpc_code = wagon.DestinationRpc;
                var has_train = false;
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
                            // TODO if inaccurate round up
                            if (currentLength + wagon.WagonLength <= maxLength &&
                                (currentWeight + wagon.WagonMass) <= (maxWeight))
                            {
                                has_train = true;
                                wagon.Silo = silo;
                                wagon.StopCurrentActivities(time, simEngine);
                                RemoveRequest(request);
                                break;
                            }
                        }
                    }
                }

                if (!has_train)
                {
                    // Not really happy with the solution but problem is that we dont know if a train will create a silo into right direction
                    if ((time - ((RequestSorting)request).ArrivalTime).TotalDays >= 1)
                    {
                        wagon.StopCurrentActivities(((RequestSorting)request).ArrivalTime, simEngine);
                        RemoveRequest(request);
                        var wagonArrived = new EventWagonArrivalInEndDestination(EventType.Standalone, this, wagon);
                        wagonArrived.Trigger(((RequestSorting)request).ArrivalTime, simEngine);
                    }
                }
            }
            //TODO think about this silo status
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

                // TODO check when the condition is really achieved

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
                //Idea close silo, and create a request with the wagons on the closed silo, which is appended to the train when departure time has arrived
                var train = (EntityTrain)request.Origin[0];
                train = Trains[train.TrainId].Where(x => x == train).ToList()[0];
                var silo = _silos[train.ArrivalStation].First();
                var train_length = train.TrainLength;
                var train_weight = train.TrainWeight;
                var current_weight = 0;
                var current_length = 0;
                train.ActualWagonList.ForEach(x =>
                {
                    if (current_length > train_length || current_weight > train_weight)
                    {
                        if (!_waitingWagons.ContainsKey(train.ArrivalStation))
                        {
                            _waitingWagons[train.ArrivalStation] = new List<EntityWagon>();
                        }
                        _waitingWagons[train.ArrivalStation].Add(x);
                        return;
                    }
                    current_weight += x.WagonMass;
                    current_length += x.WagonLength;
                });
                silo.WagonList.ForEach(x =>
                {
                    if (current_length > train_length || current_weight > train_weight)
                    {
                        if (!_waitingWagons.ContainsKey(train.ArrivalStation))
                        {
                            _waitingWagons[train.ArrivalStation] = new List<EntityWagon>();
                        }
                        _waitingWagons[train.ArrivalStation].Add(x);
                        return;
                    }
                    current_weight += x.WagonMass;
                    current_length += x.WagonLength;
                    train.ActualWagonList.Add(x);
                });

                silo.StopCurrentActivities(time, simEngine);
                //TODO only finish first activity
                Helper.Print("Train to " + train.ArrivalStation + " "+ train.ArrivalTime);

                //if (train.ActualWagonList.Count != train.Wagons.Count)
                //{
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
                // }
                //TODO should we really keep the drive to departure area or instantly send request to cu network 
                //or only close the silo and append wagon only to train when time arrived
                train.GetCurrentActivities().First().EndEvent.Trigger(time, simEngine);
                if (_silos[train.ArrivalStation].Count != 0) _silos[train.ArrivalStation].Remove(silo);
                if (_silos[train.ArrivalStation].Count == 0) _silos.Remove(train.ArrivalStation);
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