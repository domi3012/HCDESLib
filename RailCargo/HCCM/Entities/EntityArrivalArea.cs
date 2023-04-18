using System.Collections.Generic;
using SimulationCore.HCCMElements;
namespace RailCargo.HCCM.Entities
{
    public class EntityArrivalArea : Entity, IDynamicHoldingEntity
    {
        private static int s_identifier;
        private List<Entity> _holdedEntities = new List<Entity>();
        
        public EntityArrivalArea() : base(++s_identifier)
        {
        }

        public override string ToString()
        {
            return "EntityArrivalArea" + Identifier;
        }

        public override Entity Clone()
        {
            return new EntityArrivalArea();
        }

        public List<Entity> HoldedEntities
        {
            get => _holdedEntities;
            set => _holdedEntities = value;
        }
    }
}