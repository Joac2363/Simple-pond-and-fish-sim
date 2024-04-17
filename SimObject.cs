using System;
using SpaceEssentials;
using System.Collections.Generic;

namespace SimEssentials
{
    public class SimObject
    {
        public static List<SimObject> allSimObjects;
        public static List<SimObject> objectsToBeRemoved;
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
        
        public double GetDistanceTo(SimObject other) 
        {
            return (position - other.position).Len();
        }

        public bool CheckColission(SimObject other)
        {
            return GetDistanceTo(other) <= size + other.size;
        }

        public bool CheckColiding()
        {
            bool isColiding = false;
            foreach (SimObject obj in allSimObjects) 
            { 
                if (CheckColission(obj))
                {
                    isColiding = true; break;
                }
            }
            return isColiding;
        }

        public List<SimObject> FindColissions() 
        {
            List<SimObject> list = new List<SimObject>();
            foreach (SimObject obj in allSimObjects)
            {
                if (obj != this)
                {
                    if (CheckColission(obj))
                    {
                        list.Add(obj);
                    }
                }
                
            }
            return list;
        }

        public static void Destroy() // Will remove object completely from simulation
        {
            if (objectsToBeRemoved == null)
            {
                objectsToBeRemoved = new List<SimObject>();
            }
            foreach (SimObject obj in SimObject.objectsToBeRemoved)
            {
                if (obj != null)
                {
                    SimObject.allSimObjects.Remove(obj);
                }
            }
            SimObject.objectsToBeRemoved.Clear();

        }
        public void QueueDestroy()
        {
            if (objectsToBeRemoved == null)
            {
                objectsToBeRemoved = new List<SimObject>();
            }
            SimObject.objectsToBeRemoved.Add(this);
        }

    }
    
}