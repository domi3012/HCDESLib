using System.Collections.Generic;
using SimulationCore.HCCMElements;

namespace RailCargo.HCCM.Entities
{
    public class DepartureArea : Entity, IDynamicHoldingEntity
    {
        private static int s_identifier;
        private List<Entity> _holdedEntities = new List<Entity>();

        public DepartureArea() : base(++s_identifier)
        {
        }

        public override string ToString()
        {
            return "DepartureArea" + Identifier;
        }

        public override Entity Clone()
        {
            return new DepartureArea();
        }

        public List<Entity> HoldedEntities
        {
            get => _holdedEntities;
            set => _holdedEntities = value;
        }
    }
}