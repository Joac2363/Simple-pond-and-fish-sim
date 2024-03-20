using SpaceEssentials;
using SimEssentials;

class Program
{
    
    public static void Main()
    {
              

        foreach (SimObject obj in SimObject.allSimObjects) 
        {
            if (obj is Organism)
            {
                Organism organism = (Organism)obj; // Cast SimObject to Organism
                organism.test();
            }
        }
        
        //string myString = Console.ReadLine();
    }
}