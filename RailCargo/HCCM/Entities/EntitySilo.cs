using System.Collections.Generic;
using SimulationCore.HCCMElements;
namespace RailCargo.HCCM.Entities
{
    public class EntitySilo : Entity, IDynamicHoldingEntity
    {
        private static int s_identifier;
        private List<Entity> _holdedEntities = new List<Entity>();
        
        public EntitySilo() : base(++s_identifier)
        {
        }

        public override string ToString()
        {
            return "EntitySilo" + Identifier;
        }

        public override Entity Clone()
        {
            return new EntitySilo();
        }

        public List<Entity> HoldedEntities
        {
            get => _holdedEntities;
            set => _holdedEntities = value;
        }
    }
}