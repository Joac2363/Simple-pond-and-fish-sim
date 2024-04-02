using System;
using SpaceEssentials;
namespace SimEssentials
{
    public class Organism : SimObject
    {
        double viewDistance;
        double speed;
        public Vector direction;

        public Organism(Vector position, double size, double viewDistance, double speed, Vector direction) : base(position, size)
        {
            this.viewDistance = viewDistance;
            this.speed = speed;
            this.direction = direction;
        }
        
        public void Move()
        {
            position.print();
            position += direction * speed;
            position.print();
        }
        public void RotateX(double angle)
        {
            double x = direction.x;
            double y = Math.Cos(angle) * direction.y + -Math.Sin(angle) * direction.y;
            double z = Math.Sin(angle) * direction.z + Math.Cos(angle) * direction.z;
            SetDirection(x,y,z);
        }
        public void RotateY(double angle)
        {
            double x = Math.Cos(angle) * direction.x + Math.Sin(angle) * direction.x;
            double y = direction.y;
            double z = -Math.Sin(angle) * direction.z + Math.Cos(angle) * direction.z;
            SetDirection(x, y, z);
        }
        public void RotateZ(double angle) 
        {
            double x = Math.Cos(angle) * direction.x + -Math.Sin(angle) * direction.x;
            double y = Math.Sin(angle) * direction.y + Math.Cos(angle) * direction.y;
            double z = direction.z;
            SetDirection(x, y, z);
        }
        private void SetDirection(double x, double y, double z)
        {
            direction.x = x;
            direction.y = y;
            direction.z = z;
            direction.Normalize();
        }

    }
}