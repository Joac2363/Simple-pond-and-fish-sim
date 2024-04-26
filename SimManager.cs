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
		public static int simTime = 0;
		public static bool endSim = false;

		static int foodSpawnAmount = 0;
        static int foodSpawnRate = 0;

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
                string organismTypeName;
				int organismHieracyPosition;
                int numberOfWantedOrganism;
				double organismSize;
				double organismViewDistance;
				double organismSpeed;
				
				Console.WriteLine($"Organisme nummer {i+1}");

                while (true) // gets type name
                {
                    Console.WriteLine("Hvad skal denne type af organismer hedde?");
                    string oTM = Console.ReadLine(); // String version of organismTypeName

                    oTM = oTM.Trim();

                    Console.WriteLine($"Skal denne type af organisme hedde: '{oTM}'? Skriv 'ja' hvis navnet er rigitigt");
                    string answer = Console.ReadLine();
                    if (answer == "ja")
                    {
                        organismTypeName = oTM;
                        break;
                    }
                }

                while (true) //gets types place in hieracy
                {
                    Console.WriteLine("Hvor højt skal organismen være i fødekæden, hvor 0 er i bunden?");
                    string oHP = Console.ReadLine(); // String version of organismHieracyPosition

                    try
                    {
                        oHP = oHP.Trim();
                        organismHieracyPosition = Int32.Parse(oHP);
                        if (organismHieracyPosition < 0)
                        {
                            Console.WriteLine("Den kan ikke have en negativt position i fødekæden!");
                            Int32.Parse("wrong");
                        }
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Fejlede! prøv venligst igen");
                    }

                }

				Organism.AddNewType(organismTypeName, organismHieracyPosition);

                while (true) //gets 
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
                        organismSpeed = Double.Parse(oSp);
                        if (organismSpeed < 0)
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

				MakeXOrganisms(numberOfWantedOrganism, organismSize, organismViewDistance, organismSpeed, organismTypeName);
            }

			while (true)
			{
                Console.WriteLine("Hvor ofte skal der fremkomme mad? (kan kun være hele positive tal, skriv '0', hvis mad ikke skal frem komme):");

                string sFR = Console.ReadLine(); // String version of foodSpawnRate

                try
                {
                    sFR = sFR.Trim();
                    foodSpawnRate = Int32.Parse(sFR);
                    if (foodSpawnRate < 0)
                    {
                        Console.WriteLine("Tallet kan ikke være negativt!");
                        Int32.Parse("wrong");
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Fejlede! prøv venligst igen");
                }
            }

			if (foodSpawnRate != 0)
			{
				while (true)
				{
					Console.WriteLine("Hvor meget mad skal der fremkomme? (kan kun være hele positive tal.):");

					string sFA = Console.ReadLine(); // String version of foodSpawnAmount

					try
					{
						sFA = sFA.Trim();
						foodSpawnAmount = Int32.Parse(sFA);
						if (foodSpawnAmount <= 0)
						{
							Console.WriteLine("skal være 1 eller mere!");
							Int32.Parse("wrong");
						}
						break;
					}
					catch (FormatException)
					{
						Console.WriteLine("Fejlede! prøv venligst igen");
					}
				}
			}

            RunSim();
		}

		public static void Update()
		{
			//temp
			ForceStopSim();

			foreach (SimObject simObj in SimObject.allSimObjects)
			{
				if (simObj is Organism)
				{
					Organism org = simObj as Organism;
					org.Update();
				}
			} //Runs through all Organisms and updates them

			if (foodSpawnRate != 0)
			{
				if (simTime % foodSpawnRate == 0)
				{
					SpawnXFood(foodSpawnAmount);
				}
			}

		}

        public static void RunSim()
		{
			while (true)
			{
				simTime++;
				Update();
				if (endSim)
				{
					break;
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

		private static void ForceStopSim()
		{
			if (simTime > 1000000)
			{
				Console.WriteLine("Force stopping simulation, time limit exceded");
				endSim = true;
			}
		}

	}
}