using System.Collections.Generic;
using SimulationCore.HCCMElements;
namespace RailCargo.HCCM.Entities
{
    public class ArrivalArea : Entity, IDynamicHoldingEntity
    {
        private static int s_identifier;
        private List<Entity> _holdedEntities = new List<Entity>();
        
        public ArrivalArea() : base(++s_identifier)
        {
        }

        public override string ToString()
        {
            return "ArrivalArea" + Identifier;
        }

        public override Entity Clone()
        {
            return new ArrivalArea();
        }

        public List<Entity> HoldedEntities
        {
            get => _holdedEntities;
            set => _holdedEntities = value;
        }
    }
}