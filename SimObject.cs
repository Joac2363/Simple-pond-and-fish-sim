using System;
using SpaceEssentials;
namespace SimEssentials
{
    public class SimObject
    {
        public Vector position;
        public double size; // Is always treated as a radius. Yes everything is a sphere :)

        public SimObject(Vector position, double size)
        {
            this.position = position;
            this.size = size;
        }

        // Private bool CheckColiding() returns bool
        // Private bool CheckColission(SimObject other) returns bool
        // Private FindColissions() reutrns list of SimObject


    }
    public class Organism : SimObject
    {
        public Organism(Vector position) : base(position)
        {

        }
        public void test()
        {
            Console.WriteLine("Succes");
        }

        // Private void Move(Vector direction) 
    }
}