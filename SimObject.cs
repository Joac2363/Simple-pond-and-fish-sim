using System;
using SpaceEssentials;
namespace SimEssentials
{
    public class SimObject
    {
        public static List<SimObject> allSimObjects;
        public Vector position;
        public double size; // Is always treated as a radius. Yes everything is a sphere :)

        public SimObject(Vector position, double size)
        {
            if (allSimObjects == null) 
            {
                allSimObjects = new List<SimObject>();
            }
            SimObject.allSimObjects.Add(this);
            this.position = position;
            this.size = size;
        }

        // Private bool CheckColiding() returns bool
        // Private bool CheckColission(SimObject other) returns bool
        // Private FindColissions() reutrns list of SimObject


    }
    
}