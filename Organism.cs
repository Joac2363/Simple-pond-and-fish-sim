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
        public Vector targetPos;
        public string type;
        public Vector direction;


        public Organism(Vector position, double size, double saturationValue, double viewDistance, double speed, double rotationSpeed, Vector direction, string type) : base(position, size)
        {
            this.saturationValue = saturationValue;
            this.viewDistance = viewDistance;
            this.speed = speed;
            this.direction = direction;
            this.rotationSpeed = rotationSpeed;
            this.type = type;
            targetPos = null;

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
            Rotate();
            Move();
        }
        public void Rotate()
        {
            if (targetPos != null)
            {
                x0 = new Vector(0, 1, 0);
                y0 = new Vector(1, 0, 0);
                z0 = new Vector(1, 0, 0);

                double xAngle = new Vector(0, direction.y, direction.z).GetAngle(x0) * (direction.z / Math.Abs(direction.z));
                double yAngle = new Vector(direction.x, 0, direction.z).GetAngle(y0) * (direction.z / Math.Abs(direction.z));
                double zAngle = new Vector(direction.x, direction.y, 0).GetAngle(z0) * (direction.y / Math.Abs(direction.y)); 


                Vector targetDirection = (targetPos - position).Normalize();
                double xAngleToMatch = Math.Atan(targetDirection.y / targetDirection.z);
                double yAngleToMatch = Math.Atan(targetDirection.x / targetDirection.z);
                double zAngleToMatch = Math.Atan(targetDirection.y / targetDirection.x);

                double xRotation = rotationSpeed;
                double yRotation = rotationSpeed;
                double zRotation = rotationSpeed;

                double xdiff = xAngleToMatch - xAngle;
                if (Math.Abs(xdiff) <= rotationSpeed)
                {
                    xRotation = xdiff;
                }
                else if (xdiff < 0)
                {
                    xRotation = -rotationSpeed;
                }
                else
                {
                    xRotation = 0;
                }

                double ydiff = yAngleToMatch - yAngle;
                if (Math.Abs(ydiff) <= rotationSpeed)
                {
                    yRotation = ydiff;
                }
                else if (ydiff < 0)
                {
                    yRotation = -rotationSpeed;
                }
                else
                {
                    yRotation = 0;
                }


                double zdiff = zAngleToMatch - zAngle;
                if (Math.Abs(zdiff) <= rotationSpeed)
                {
                    zRotation = zdiff;
                }
                else if (zdiff < 0)
                {
                    zRotation = -rotationSpeed;
                }
                else
                {
                    zRotation = 0;
                }


                if (xRotation != 0)
                {
                    Console.WriteLine("x");
                    direction.RotateX(xRotation);
                    direction = direction.Normalize();
                }

                if (yRotation != 0)
                {
                    Console.WriteLine("y");
                    direction.RotateY(yRotation);
                    direction = direction.Normalize();
                }
                if (zRotation != 0)
                {
                    Console.WriteLine("z");
                    direction.RotateZ(zRotation);
                    direction = direction.Normalize();
                }
            }
        }
        void Move()
        {
            position += direction * speed;
        }


        public SomeClass FindNearest<SomeClass>(List<SimObject> objs) where SomeClass : class
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
                    if (distance < shortestDistance)
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