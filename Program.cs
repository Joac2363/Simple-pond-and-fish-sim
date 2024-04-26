using SpaceEssentials;
using SimEssentials;

class Program
{
    
    public static void Main()
    {
        SimManager.StartSim();
        
        //SimManager.MakeXOrganisms(100, 2, 15, 0.6, "Fishy");

        //foreach (SimObject simObj in SimObject.allSimObjects)
        //{
        //    if (simObj is Organism)
        //    {
        //        Organism org = simObj as Organism;
        //        Console.WriteLine(org.position);
        //    }
        //}

        string myString = Console.ReadLine();
    }
}