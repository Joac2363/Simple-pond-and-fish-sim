using SpaceEssentials;

class Program
{
    static void Main()
    {
        Vector myVector = new Vector(13, 13, 13);
        Vector other = new Vector(2, 2, 2);
        
        Console.WriteLine(other.Normalize().x);

        string myString = Console.ReadLine();
    }
}