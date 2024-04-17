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
        new Food(new Vector(25, 10, 25), 1, 1);
        new Food(new Vector(25, 25, 5), 1, 1);
        Organism a = new Organism(new Vector(0, 0, 0), 1, 0, 500, 1, 0.1, new Vector(1, 0, 0), "hunter");
        //foreach (SimObject obj in SimObject.allSimObjects)
        //{
        //    Organism org = obj as Organism;
        //    org.targetPos = new Vector(1, 1, -10);
        //}
        for (int i = 0; i < 100; i++)
        {
            foreach (SimObject obj in SimObject.allSimObjects)
            {
                Organism org = obj as Organism;
                if (org != null)
                {
                org.Update();       
                //Console.WriteLine($", Position: {org.position}, iteration: {i}");
                }
            }
            SimObject.Destroy();
        }





        string myString = Console.ReadLine();

    }
}