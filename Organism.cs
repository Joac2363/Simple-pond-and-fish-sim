using System;
using SpaceEssentials;
using SimEssentials;
namespace SimEssentials
{
    public class Organism : SimObject
    {
        public static List<string> types;
        public static List<int> population;
        public double viewDistance;
        public double speed; // Measued in units pr. update - Subject to change
        public double saturationValue;
        public SimObject target;
        public string type;
        public Vector direction;
        public bool gender; // Male = 1 Female = 0
        public bool matingReady;


        public Organism(Vector position, double size, double saturationValue, double viewDistance, double speed, string type) : base(position, size)
        {
            this.saturationValue = saturationValue;
            this.viewDistance = viewDistance;
            this.speed = speed;
            this.type = type;
            target = null;

            // make sure population and types list exit
            if (types == null)
            {
                types = new List<string>();
            }
            if (population == null)
            {
                population = new List<int>();
            }

            // Check if type is new type
            bool shouldCreateNew = true;
            int index = 0;
            foreach (string typ in types)
            {
                if (typ == type)
                {
                    population[index]++; // Add one to population if already exist
                    shouldCreateNew = false;
                    break;
                }
                index++;
            }

            if (shouldCreateNew) // Creation of new type
            {
                types.Add(type);
                population.Add(1);
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
            if (matingReady){Mate();} 
            else{Eat();}
            
        }
        void Mate()
        {
            return;
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
            if (target != null)
            {
                // If colliding then eat
                if (this.CheckColission(target)) 
                {
                    Food food = target as Food;
                    if (food != null)
                    {
                        saturationValue += food.saturationValue;
                        target.QueueDestroy();
                        target = null;

                    }
                }
            }
        }
        void DefineTarget()
        {
            if (matingReady)
            {
                List<Organism> closePotentialMatingPartners = FindAllNear<Organism>(SimObject.allSimObjects, viewDistance);

                if (closePotentialMatingPartners.Count() == 0)
                {
                    Console.WriteLine("esc");
                    return;
                }

                double currentDistance = double.MaxValue;
                foreach (Organism obj in closePotentialMatingPartners)
                {
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
                SimObject nearestFood = FindNearest<Food>(SimObject.allSimObjects, viewDistance);
                if (nearestFood != null)
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
                direction = new Vector(0, 0, 0);
            }

        }
        void Move()
        {
            position += direction * speed;
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

        //public static bool operator ==(Organism a, Organism b)
        //{
        //    if (a is null || b is null)
        //    {
        //        return false;
        //    }
        //    return a.x == b.x && a.y == b.y && a.z == b.z;
        //}

        //public static bool operator !=(Organism a, Organism b)
        //{
        //    Console.WriteLine($"{a is null} , {b is null}");
        //    if (a is null || b is null)
        //    {
        //        return false;
        //    }
        //    return a.x != b.x || a.y != b.y || a.z != b.z;
        //}

    }
}