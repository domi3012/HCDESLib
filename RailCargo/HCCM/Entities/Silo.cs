using System.Collections.Generic;
using SimulationCore.HCCMElements;
namespace RailCargo.HCCM.Entities
{
    public class Silo : Entity, IDynamicHoldingEntity
    {
        private static int s_identifier;
        private List<Entity> _holdedEntities = new List<Entity>();
        
        public Silo() : base(++s_identifier)
        {
        }

        public override string ToString()
        {
            return "Silo" + Identifier;
        }

        public override Entity Clone()
        {
            return new Silo();
        }

        public List<Entity> HoldedEntities
        {
            get => _holdedEntities;
            set => _holdedEntities = value;
        }
    }
}