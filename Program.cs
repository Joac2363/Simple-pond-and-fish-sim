using SpaceEssentials;
using SimEssentials;

class Program
{
    
    public static void Main()
    {
        Vector vOne = new Vector(1, 2, 3);
        Vector vTwo = new Vector(-1, -2, -3);
        new SimObject(vOne, 7);
        new SimObject(vTwo, 5);
        foreach (SimObject obj in SimObject.allSimObjects)
        {
            Console.WriteLine("new object");
            foreach (SimObject simobject in obj.FindColissions())
            {
                Console.WriteLine(simobject.position.x);
            }
        }

        string myString = Console.ReadLine();
    }
}