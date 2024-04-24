using System;
using SpaceEssentials;
using SimEssentials;
namespace SimEssentials
{ // reproduce, die/food consumption, predator/prey
    public class ReproducedOrganism
    {
        public static List<ReproducedOrganism> reproducedOrganisms;

        public Vector position;
        public double size;
        public double saturationValue;
        public double viewDistance;
        public double speed; 
        public string type;


        public ReproducedOrganism(Vector position, double size, double saturationValue, double viewDistance, double speed, string type)
        {
            this.position = position;
            this.size = size;
            this.saturationValue = saturationValue;
            this.viewDistance = viewDistance;
            this.speed = speed;
            this.type = type;
            if (ReproducedOrganism.reproducedOrganisms == null )
            {
                ReproducedOrganism.reproducedOrganisms = new List<ReproducedOrganism>();
            }
            ReproducedOrganism.reproducedOrganisms.Add( this );
        }

        public static void InstantiateAll()
        {
            if (ReproducedOrganism.reproducedOrganisms == null) { return; }

            foreach (ReproducedOrganism obj in ReproducedOrganism.reproducedOrganisms)
            {
                new Organism(obj.position, obj.size, obj.saturationValue, obj.viewDistance, obj.speed, obj.type);
            }
            ReproducedOrganism.reproducedOrganisms.Clear();
        }
    }
}