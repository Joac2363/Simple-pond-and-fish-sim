using System;
using SpaceEssentials;
using System.Collections.Generic;
using SimEssentials;
using DataRecording;

namespace SimEssentials
{
	public class SimManager
	{
		static Random random = new Random();
		public static int spawnSphereRadius = 50;
		public static double baseSaturation = 5;
		public static double foodSize = 1;
		public static double foodValue = 1;
		public static DataRecorder dataRecorder = new DataRecorder();

		public static void StartSim()
		{
			int numberOfOrganismTypes;

			dataRecorder.AskForFilePath();

			Console.WriteLine("Hvor mange forskellige typer af Organismer vil dy have? (kan kun være hele positive tal):");
			while (true)
			{ 
				string nOOT = Console.ReadLine(); // String version of numberOfOrganismTypes

				try
				{
					nOOT = nOOT.Trim();
					numberOfOrganismTypes = Int32.Parse(nOOT);
					if (numberOfOrganismTypes <= 0 ) 
					{
						Console.WriteLine("Der skal være midst en Organisme!");
						Int32.Parse("wrong");
					}
					break;
				}
				catch (FormatException)
				{
					Console.WriteLine("Fejlede! prøv venligst igen");
				}
			}

			for (int i = 0; i < numberOfOrganismTypes; i++)
			{
				int numberOfWantedOrganism;
				double organismSize;
				double organismViewDistance;
				double organismSpeed;
				string organismTypeName;

				Console.WriteLine("Organisme");
				while (true)
				{
					Console.WriteLine("Hvor mange af denne type af organismer skal der være i simulationens start?");
					string nOWO = Console.ReadLine(); // String version of numberOfWantedOrganism

					try
					{
						nOWO = nOWO.Trim();
						numberOfWantedOrganism = Int32.Parse(nOWO);
						if (numberOfWantedOrganism <= 0)
						{
							Console.WriteLine("Der skal være midst en af Organisme typen!");
							Int32.Parse("wrong");
						}
						break;
					}
					catch (FormatException)
					{
						Console.WriteLine("Fejlede! prøv venligst igen");
					}

				}

                while (true)
                {
                    Console.WriteLine("Hvor stor skal denne type af organismer være? (kan være et komme tal, brug '.' som komma)");
                    string oS = Console.ReadLine(); // String version of organismSize

                    try
                    {
                        oS = oS.Trim();
                        organismSize = Double.Parse(oS);
                        if (organismSize <= 0)
                        {
                            Console.WriteLine("Organismen skal være større end nul");
                            Double.Parse("wrong");
                        }
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Fejlede! prøv venligst igen");
                    }

                }
					
                while (true)
                {
                    Console.WriteLine("Hvor lang skal denne type af organismer kunne se? (kan være et komme tal, brug '.' som komma)");
                    string oVD = Console.ReadLine(); // String version of organismViewDistance

                    try
                    {
                        oVD = oVD.Trim();
                        organismViewDistance = Double.Parse(oVD);
                        if (organismViewDistance <= 0)
                        {
                            Console.WriteLine("Organismen skal være større end nul");
                            Double.Parse("wrong");
                        }
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Fejlede! prøv venligst igen");
                    }

                }

                while (true)
                {
                    Console.WriteLine("Hvor hurtig skal denne type af organismer være? (kan være et komme tal, brug '.' som komma)");
                    string oSp = Console.ReadLine(); // String version of organismSpeed

                    try
                    {
                        oSp = oSp.Trim();
                        organismViewDistance = Double.Parse(oSp);
                        if (organismViewDistance < 0)
                        {
                            Console.WriteLine("Organisms hastighed må ikke være mindre end nul");
                            Double.Parse("wrong");
                        }
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Fejlede! prøv venligst igen");
                    }

                }

            }
		}

		public static void MakeXOrganisms (int numberOfOrganism, double organismSize, double organismViewDistance, double organismSpeed, string organismType) 
		{
			for (int i = 0; i < numberOfOrganism; i++) 
			{
				Vector position = new Vector(random.Next(-spawnSphereRadius, spawnSphereRadius), random.Next(-spawnSphereRadius, spawnSphereRadius), random.Next(-spawnSphereRadius, spawnSphereRadius));
				new Organism(position, organismSize, baseSaturation, organismViewDistance, organismSpeed, organismType);
			}
		}

		public static void SpawnXFood(int numberOfFood)
		{
			for (int i = 0; i < numberOfFood; i++)
			{
				Vector position = new Vector(random.Next(-spawnSphereRadius, spawnSphereRadius), random.Next(-spawnSphereRadius, spawnSphereRadius), random.Next(-spawnSphereRadius, spawnSphereRadius));
				new Food(position, foodSize, foodValue);
			}
		}

	}
}