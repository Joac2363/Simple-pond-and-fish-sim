using System;
using SpaceEssentials;
using SimEssentials;
namespace SimEssentials
{
    public class Organism : SimObject
    {
        double viewDistance;
        double speed; // Measued in units pr. update - Subject to change
        double rotationSpeed; // Measured in degrees pr. update - Subject to change
        double saturationValue;
        Vector targetPos;

        public Vector direction;

        public Organism(Vector position, double size, double saturationValue, double viewDistance, double speed, double rotationSpeed, Vector direction) : base(position, size)
        {
            this.saturationValue = saturationValue;
            this.viewDistance = viewDistance;
            this.speed = speed;
            this.direction = direction;
            this.rotationSpeed = rotationSpeed;
            targetPos = null;
        }

        public void Update()
        {
            Rotate();
            Move();
        }
        public void Rotate()
        {
            //if (targetPos != null)
            if (true)
            {
                double xAngle = Math.Atan(direction.y / direction.z);
                double yAngle = Math.Atan(direction.x / direction.z);
                double zAngle = Math.Atan(direction.y / direction.x);

                //Vector targetDirection = (targetPos - position).Normalize();
                Vector targetDirection = new Vector(-1,0,0);

                double xAngleToMatch = Math.Atan(targetDirection.y / targetDirection.z);
                double yAngleToMatch = Math.Atan(targetDirection.x / targetDirection.z);
                double zAngleToMatch = Math.Atan(targetDirection.y / targetDirection.x);

                double xRotation = rotationSpeed;
                double yRotation = rotationSpeed;
                double zRotation = rotationSpeed;

                double xdiff =  xAngleToMatch - xAngle;
                if ( Math.Abs(xdiff) <= rotationSpeed )
                {
                    xRotation = xdiff;
                } else if (xdiff < 0)
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
                    direction.RotateX(xRotation);
                    direction = direction.Normalize();
                }

                if (yRotation != 0)
                { 
                    direction.RotateY(yRotation);
                    direction = direction.Normalize();
                }
                if (zRotation != 0)
                {
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