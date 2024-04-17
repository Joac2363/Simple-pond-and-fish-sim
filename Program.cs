using SpaceEssentials;
using SimEssentials;

class Program
{
    
    public static void Main()
    {
        //Vector vOne = new Vector(1, 2, 3);
        //Vector vTwo = new Vector(-1, -2, -3);
        //new SimObject(vOne, 7);
        //new SimObject(vTwo, 5);
        //foreach (SimObject obj in SimObject.allSimObjects)
        //{
        //    Console.WriteLine("new object");
        //    foreach (SimObject simobject in obj.FindColissions())
        //    {
        //        Console.WriteLine(simobject.position.x);
        //    }
        //}

        //string myString = Console.ReadLine();

        //Vector a = new Vector(1, 1, 0);
        //Vector b = new Vector(1, 0, 0);
        //Console.WriteLine(a.GetAngle(b));

        Organism a = new Organism(new Vector(0, 0, 0), 1, 0, 4, 0.1, 0.1, new Vector(0.1, 0.1, 1), "hunter");
        foreach (SimObject obj in SimObject.allSimObjects)
        {
            Organism org = obj as Organism;
            org.targetPos = new Vector(1, 1, -10);
        }
        for (int i = 0; i < 100; i++)
        {
            foreach (SimObject obj in SimObject.allSimObjects)
            {
                Organism org = obj as Organism;
                org.Update();
                Console.WriteLine($"Direction: {org.direction}, TagetDirection{(org.targetPos-org.position).Normalize()}, Position: {org.position}, iteration: {i}");
            }
        }






        string myString = Console.ReadLine();

    }
}