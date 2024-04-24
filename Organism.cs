using System;
using SpaceEssentials;
using SimEssentials;
namespace SimEssentials
{ 
    public class Organism : SimObject
    {
        
        public static List<string> types;
        public static List<int> population;
        public static List<int> hieracy;
        public double viewDistance;
        public double speed; // Measued in units pr. update - Subject to change
        public double saturationValue;
        public SimObject target;
        public string type;
        public Vector direction;
        public bool gender; // Male = 1 Female = 0
        public bool matingReady;
        public int positionInHieracy;


        public Organism(Vector position, double size, double saturationValue, double viewDistance, double speed, string type) : base(position, size)
        {
            this.saturationValue = saturationValue;
            this.viewDistance = viewDistance;
            this.speed = speed;
            this.type = type;
            target = null;

            // make sure population list exits
            if (population == null)
            {
                population = new List<int>();
            }

            // Add 1 to population and assign position in hieracy
            int index = 0;
            foreach (string typ in types)
            {
                if (typ == type)
                {
                    population[index]++; // Add one to population if already exist
                    positionInHieracy = hieracy[index];
                    break;
                }
                index++;
            }
            


            gender = PRandom.boolean();
            matingReady = false;

            
        }

        public void Update()
        {
            UpdateMatingStatus();
            DefineTarget();
            Rotate();
            Move();
            LoseSaturation();
            if (matingReady){HandleMate();} 
            else{Eat();}
            
        }

        void LoseSaturation()
        {
            saturationValue -= (1f / 1000f);
            if (saturationValue <= 0f)
            {
                this.QueueDestroy();
            }
        }

        void HandleMate()
        {
            if (target is null) // If no mating partner, then stop mating
            {
                return;
            }
            if (this.CheckColission(target))
            {
                Organism.Reproduce(this, target as Organism);
            }
        }
        

        void UpdateMatingStatus()
        {
            if (saturationValue >= 10)
            {
                matingReady = true;
            } else
            {
                matingReady = false;
            }
        }
        void Eat()
        {
            if (target == null) { return; }
            // If colliding then eat
            if (this.CheckColission(target)) 
            {
                Food targetAsFood = target as Food;
                if (targetAsFood != null)
                {
                    saturationValue += targetAsFood.saturationValue;
                }
                Organism targetAsOrganism = target as Organism;
                if (targetAsOrganism != null)
                {
                    saturationValue += targetAsOrganism.saturationValue;
                }

                target.QueueDestroy();
                target = null;
            }
        }
        void DefineTarget()
        {
            if (matingReady)
            {
                List<Organism> closePotentialMatingPartners = FindAllNear<Organism>(SimObject.allSimObjects, viewDistance);

                if (closePotentialMatingPartners.Count() == 0)
                {
                    Console.WriteLine("No available mating partners");
                    return;
                }

                double currentDistance = double.MaxValue;
                foreach (Organism obj in closePotentialMatingPartners)
                {
                    if (obj.gender == gender){continue;} // If same gender, continue
                    if (obj.type != type){continue;}     // If not same type, continue
                    double newDistance = this.GetDistanceTo(obj);
                    if (newDistance < currentDistance)
                    {
                        target = obj;
                        currentDistance = newDistance;
                    }
                }
            }
            else
            {
                Food nearestFood = FindNearest<Food>(SimObject.allSimObjects, viewDistance) as Food;
                Organism nearestPrey = FindNearestPrey();

                if (nearestFood == null && nearestPrey == null)
                {
                    target = null;
                    return;
                } 

                if (nearestFood == null)
                {
                    target = nearestPrey;
                    return;
                }
                if (nearestPrey == null)
                {
                    target = nearestFood;
                    return;
                }

                bool preyHasShortestDistance = (this.GetDistanceTo(nearestPrey) < this.GetDistanceTo(nearestFood));
                if (preyHasShortestDistance)
                {
                    target = nearestPrey;
                }
                else
                {
                    target = nearestFood;
                }

            }
        }
        void Rotate()
        {
            if (target != null)
            {
                direction = (target.position - position).Normalize();
            } else
            {
                direction = new Vector(0, 1, 0);
            }

        }
        void Move()
        {
            position += direction * speed;
        }

        public Organism FindNearestPrey()
        {
            List<SimObject> nearObjects = this.FindWithin(viewDistance);
            double shortestDistance = double.MaxValue;
            Organism prey = null;
            foreach (SimObject obj in nearObjects)
            {
                if (obj is not Organism) { continue; }
                Organism organism = obj as Organism;
                if (organism.positionInHieracy >= positionInHieracy) { continue; }

                double distance = this.GetDistanceTo(organism);
                if (distance < shortestDistance)
                {
                    prey = organism;
                    shortestDistance = distance;
                }

            }
            return prey;
        }

        public SomeClass FindNearest<SomeClass>(List<SimObject> objs, double maxDist) where SomeClass : class
            // Will check for nearest object of class SomeClass (or subclass). This requires the SomeClass to be compatible with the .GetDistanceTo(obj) method
            // Proper use : ObjectRefference.FindNearest<InsertClassNameHere>(ListRefference)
        {
            SomeClass nearestObj = null;
            double shortestDistance = double.MaxValue;
            foreach (SimObject obj in objs)
            {
                if (obj is SomeClass && obj != this) // If obj is the same class as SomeClass (or any subclass) -> truthy
                {
                    double distance = this.GetDistanceTo(obj);
                    if (distance < shortestDistance && distance <= maxDist)
                    {
                        shortestDistance = distance;
                        nearestObj = obj as SomeClass;
                    }
                }
            }
            return nearestObj;
        }
        public List<SomeClass> FindAllNear<SomeClass>(List<SimObject> objs, double maxDist) where SomeClass : class
        {
            List<SomeClass> nearObjects = new List<SomeClass>();
            List<SimObject> withinRange = this.FindWithin(maxDist);
            foreach (SimObject obj in  withinRange)
            {
                if (obj is SomeClass)
                {
                    nearObjects.Add(obj as SomeClass);
                }
            }
            return nearObjects;
        }


        

        public static void Reproduce(Organism main, Organism other)
        {
            main.saturationValue *= (2f/3f);
            other.saturationValue *= (2f/3f);
            double size = GetAverage(main.size, other.size);
            double saturation = GetAverage(main.saturationValue, other.saturationValue);
            double viewDistance = GetAverage(main.viewDistance, other.viewDistance);
            double speed = GetAverage(main.speed, other.speed);
            
            for (int i = 0;i < 1; i++) // This loop is currently redundant, but may be usefull later, if we want fish to produce more than one new fish per reproduction
            {
                new ReproducedOrganism(main.position, size, saturation, viewDistance, speed, main.type);
            }
            
            double GetAverage(double x, double y)
            {
                return (x + y) / 2;
            }
        }
        
        public static void AddNewType(string typeName, int placeInHieracy)
        {
            if (Organism.types == null)
            {
                Organism.types = new List<string>();
                Organism.population = new List<int>();
                Organism.hieracy = new List<int>();
            }
            Organism.types.Add(typeName);
            Organism.population.Add(1);
            Organism.hieracy.Add(placeInHieracy);

        }

    }
}