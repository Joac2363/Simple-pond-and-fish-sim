using System;
using System.Collections.Generic;
using SimEssentials;
using SpaceEssentials;
using System.IO;

namespace DataRecording
{
    public class DataRecorder
    {
        // varibles for creating the datafile
        string filePath = ""; //Filepath
        string fileName = "SimulationData.csv";
        bool firstLine = true;

        public string RecordDataPoint() // method that creates a sting to be added/wrinten to the data file
        {
            //string returnString = "";
            string returnString = "\n"; // makes sure that the data is on a new line

            foreach (int orgPop in Organism.population) // records the population of each type of organism
            {
                returnString += $"{orgPop};";
            }

            returnString += $"{SimManager.simTime}"; // adds the current time in the simulation
            

            return returnString;
        }

        public void AskForFilePath() // method that asks user for the file path for the file.
        {
            string givenPath = "";
            while (true) // asks for file path
            {
                Console.WriteLine("Giv din ønsket fil lokation (hele stien):");
                givenPath = Console.ReadLine();

                try // checks if viable file path is given 
                {
                    givenPath = givenPath.Trim();
                    File.WriteAllText(Path.Combine(givenPath, fileName), ""); //creates an empty file to write to
                    break;
                }
                catch (DirectoryNotFoundException) // catches the exception and informs the user
                {
                    Console.WriteLine("Fejlede! lokation ikke fundet, prøv venligst igen");
                }
            }

            filePath = givenPath;
        }

        public void AddDataPoint() // method that adds datapoint to data file
        {
            using (StreamWriter outputFile = File.AppendText(Path.Combine(filePath, fileName)))
            {
                if (firstLine) // adds headers if its the first line in the file
                {
                    string header = "";
                    foreach (string type in Organism.types) // gets the names of the organisms
                    {
                        header += $"Organism {type} population; ";
                    }
                    outputFile.Write(header + "time");
                    firstLine = false;
                }
                outputFile.Write(RecordDataPoint()); //writes datapoint to the file
            }
        }

        public void CheckRecord() // method that checks if its time to record a data point
        {
            if (SimManager.simTime % SimManager.recordingFrequency == 0)
            {
                Console.WriteLine("Recording Datapoint"); // informs the user that a datapoint is being recorded
                AddDataPoint();
            }
        }
    }

}