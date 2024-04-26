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
        string fileName = "SimulationData.csv";
        bool firstLine = true;

        public string RecordDataPoint()
        {
            //string returnString = "";
            string returnString = "\n";

            foreach (int orgPop in Organism.population)
            {
                returnString += $"{orgPop};";
            }

            returnString += $"{SimManager.simTime}";
            

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
                    File.WriteAllText(Path.Combine(givenPath, fileName), "");
                    break;
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Fejlede! lokation ikke fundet, prøv venligst igen");
                }
            }

            filePath = givenPath;
        }

        public void AddDataPoint()
        {
            using (StreamWriter outputFile = File.AppendText(Path.Combine(filePath, fileName)))
            {
                if (firstLine)
                {
                    string header = "";
                    foreach (string type in Organism.types)
                    {
                        header += $"Organism {type} population; ";
                    }
                    outputFile.Write(header + "time");
                    firstLine = false;
                }
                outputFile.Write(RecordDataPoint());
            }
        }

        public void CheckRecord()
        {
            if (SimManager.simTime % SimManager.recordingFrequency == 0)
            {
                AddDataPoint();
            }
        }
    }

}