using System;
using SpaceEssentials;
namespace SimEssentials
{
    public class Organism : SimObject
    {
        double speed;
        // double sensing distance
        public Organism(Vector position, double size, double speed) : base(position, size)
        {
            this.speed = speed;
        }
        public void test()
        {
            Console.WriteLine("Succes");
        }

        // Private void Move(Vector direction) 
    }
}