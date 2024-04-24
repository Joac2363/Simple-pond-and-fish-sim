using System;
using System.Collections.Generic;
using SimEssentials;
using SpaceEssentials;
using System.IO;

namespace DataRecording
{
    public class DataRecorder
    {
        string filePath = ""; //Filepath
        string fileName = "SimulationData.cvs";

        public string RecordDataPoint()
        {
            string returnString = "x, y, z";
            

            foreach (SimObject obj in SimObject.allSimObjects)
            {
                string x = obj.position.x.ToString();
                string y = obj.position.y.ToString();
                string z = obj.position.z.ToString();
                returnString += $"\n{x}, {y}, {z}";
            }

            return returnString;
        }

        public void AskForFilePath()
        {
            string givenPath = "";
            while (true)
            {
                Console.WriteLine("Giv din ønsket fil lokation (hele stien):");
                givenPath = Console.ReadLine();

                try
                {
                    givenPath = givenPath.Trim();
                    File.WriteAllText(Path.Combine(givenPath, fileName), "text");
                    break;
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Fejlede! lokation ikke fundet, prøv venligst igen");
                }
            }

            filePath = givenPath;
        }
    }

}