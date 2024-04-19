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
        public double rotationSpeed; // Measured in degrees pr. update - Subject to change
        public double saturationValue;
        public SimObject target;
        public string type;
        public Vector direction;


        public Organism(Vector position, double size, double saturationValue, double viewDistance, double speed, double rotationSpeed, Vector direction, string type) : base(position, size)
        {
            this.saturationValue = saturationValue;
            this.viewDistance = viewDistance;
            this.speed = speed;
            this.direction = direction.Normalize();
            this.rotationSpeed = rotationSpeed;
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
        }

        public void Update()
        {
            DefineTarget();
            Rotate();
            Move();
            Eat(); 
            
        }
        void Eat()
        {
            if (target != null) 
            {
                //Console.WriteLine(this.GetDistanceTo(target));
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
            SimObject nearestFood = FindNearest<Food>(SimObject.allSimObjects, viewDistance);
            if (nearestFood != null)
            {
                target = nearestFood;
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

    }
}