using SimulationCore.HCCMElements;
namespace RailCargo.HCCM.Entities
{
    public class Wagon : Entity
    {
        public Wagon(int identifier) : base(identifier)
        {
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

        public override Entity Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}