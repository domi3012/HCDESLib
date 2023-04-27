using System;
using RailCargo.HCCM.Activities;
using RailCargo.HCCM.Entities;
using RailCargo.HCCM.staticVariables;
using SimulationCore.HCCMElements;
using SimulationCore.SimulationClasses;

namespace RailCargo.HCCM.Events
{
    public class EventTrainCreation : Event
    {
        private readonly EntityTrain _entityTrain;
        private readonly string _departureTyp;

        public EventTrainCreation(ControlUnit parentControlUnit, EntityTrain entityTrain, string departureTyp) : base(
            EventType.Standalone, parentControlUnit)
        {
            _entityTrain = entityTrain;
            _departureTyp = departureTyp;
        }

        protected override void StateChange(DateTime time, ISimulationEngine simEngine)
        {
            switch (_departureTyp)
            {
                case "BB":
                    ActivityTrainPreparation trainPreparation =
                        new ActivityTrainPreparation(ParentControlUnit, Constants.ACTIVITY_TRAIN_PREPARATION, false);
                    //TODO change to acutal time
                    trainPreparation.StartEvent.Trigger(time, simEngine);
                    //GET train depature time somehow
                    EventTrainDepartureTimeArrived trainDepartureTimeArrived =
                        new EventTrainDepartureTimeArrived(EventType.Standalone, ParentControlUnit, _entityTrain);
                    simEngine.AddScheduledEvent(trainDepartureTimeArrived, DateTime.Today);
                    break;
                case "VBF":
                    //TODO overthink the concept of train creation
                    var affectedShuntingYard = AllShuntingYards.Instance.GetYards(_entityTrain.StartLocation);
                    affectedShuntingYard.AddRequest(new Requests.RequestForSilo(Constants.REQUEST_FOR_SILO,
                        _entityTrain, time));
                    //zug wartet hier
                    var waitingForSilo = new ActivityWaitingForSilo(affectedShuntingYard,
                        Constants.ACTIVITY_WAITING_FOR_SILO, true, _entityTrain);
                    _entityTrain.AddActivity(waitingForSilo);
                    SequentialEvents.Add(waitingForSilo.StartEvent);
                    break;
                default:
                    Console.WriteLine("Not implemented" + _departureTyp);
                    break;
            }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override Event Clone()
        {
            throw new NotImplementedException();
        }

        public override Entity[] AffectedEntities
        {
            get
            {
                return new Entity[]
                {
                    _entityTrain
                };
            }
        }
    }
}