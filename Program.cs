using SpaceEssentials;
using SimEssentials;

class Program
{
    
    public static void Main()
    {
        List<SimObject> allPositions = new List<SimObject>();

        allPositions.Add(new Organism(new Vector(0,1,2)));
        
        foreach (SimObject obj in allPositions) 
        {
            if (obj is Organism)
            {
                //Console.WriteLine("Suc");
                Organism organism = (Organism)obj; // Cast SimObject to Organism
                organism.test();
            }
        }
        
        //Vector myVector = new Vector(13, 13, 13);
        //Vector other = new Vector(2, 2, 2);

        //Console.WriteLine(other.Normalize().x);

        //string myString = Console.ReadLine();
    }
}