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
        // basic simulation varibles 
		public static int spawnSphereRadius = 50; // controls the area where organism can spawn within
		public static double baseSaturation = 5; // the staarting amount of saturation organisms have
		public static double foodSize = 1; // the size of food
		public static double foodValue = 5; // the value of food

        // controll the 'time' in the simulation
		public static int simTime = 0;
		public static bool endSim = false;

        // controlls how often and how much food will be spawned 
		static int foodSpawnAmount = 0;
        static int foodSpawnRate = 0;

        //variables related to data recording
		public static DataRecorder dataRecorder = new DataRecorder();
		public static int recordingFrequency;
		static int recordingTime;



        public static void StartSim() // starts sim and ask user for starting conditions
		{
			int numberOfOrganismTypes; // holds the amount of different organism types

			dataRecorder.AskForFilePath(); // makes file for data recording, ask for path in the process

            while (true) //asks how often to record data points
            {
                Console.WriteLine("Hvor ofte skal simulationen opsamle datapunkter");
                string rF = Console.ReadLine(); // String version of recordingFrequency

                try //tries to parse to an int
                {
                    rF = rF.Trim(); //trims the sting 
                    recordingFrequency = Int32.Parse(rF); // parces the string to an int
                    if (recordingFrequency <= 0) // forces the exception to be cast if the frequency is zero or less
                    {
                        Console.WriteLine("Det må ikke være nul eller mindre");
                        Int32.Parse("wrong");
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Fejlede! prøv venligst igen"); // informs the user that the process has failed and they need to try angain
                }
            }
                
            while (true) // get the amount of time that simulation need to run for. reffrence the above coments for the logic as it the same in nature
            {
                Console.WriteLine("Hvor lang tid skal simulationen kører?");
                string rT = Console.ReadLine(); // String version of recordingTime

                try
                {
                    rT = rT.Trim();
                    recordingTime = Int32.Parse(rT);
                    if (recordingTime <= 0)
                    {
                        Console.WriteLine("Det må ikke være nul eller mindre");
                        Int32.Parse("wrong");
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Fejlede! prøv venligst igen");
                }
            }

            Console.WriteLine("Hvor mange forskellige typer af Organismer vil du have? (kan kun være hele positive tal):");
                
			while (true) // Ask for the amount of different organisms wanted, 
			{ 
				string nOOT = Console.ReadLine(); // String version of numberOfOrganismTypes, reffrence the first while loops comments for discriptions as it is of the same nature

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

			for (int i = 0; i < numberOfOrganismTypes; i++) // Ask for the wanted data for type of the organisms for each type of organism
			{
                string organismTypeName;
				int organismHieracyPosition;
                int numberOfWantedOrganism;
				double organismSize;
				double organismViewDistance;
				double organismSpeed;
				
				Console.WriteLine($"Organisme nummer {i+1}"); // tells which organism they are working on
                //reffrence the first while loops comments for discriptions as it is of the same nature

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

				Organism.AddNewType(organismTypeName, organismHieracyPosition); // makes the type

                while (true) //gets the amount of the organism need at the simulation start
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

                while (true) //gets the organisms size
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
					
                while (true) // gets the organisms viewing distances
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

                while (true) //get the organism speed
                {
                    Console.WriteLine("Hvor hurtig skal denne type af organismer være? (kan være et komme tal og anbefales, brug '.' som komma)");
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

				MakeXOrganisms(numberOfWantedOrganism, organismSize, organismViewDistance, organismSpeed, organismTypeName); // makes the wanted amount og the given organism
            }

			while (true) // ask for how often food will be spawned
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

			if (foodSpawnRate != 0) // if food will spawn, asks for the amoint of food that will be spawned each time
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


            Console.WriteLine("Starting simulation"); // informs that the simulation starts 
            RunSim(); // runs the simulation
		}

		public static void Update()
		{
            // checks if the simulation has run over the forced limit which is 100000000 frames
            ForceStopSim();

			foreach (SimObject simObj in SimObject.allSimObjects)
			{
				if (simObj is Organism)
				{
					Organism org = simObj as Organism;
					org.Update();
				}
			} //Runs through all Organisms and updates them

            SimObject.Destroy(); // destroys the organism that have died this frame
            ReproducedOrganism.InstantiateAll(); // creates all the organisms that have been born this frame

			if (foodSpawnRate != 0) // spawns food if enabled 
			{
				if (simTime % foodSpawnRate == 0)
				{
					SpawnXFood(foodSpawnAmount);
				}
			}

			dataRecorder.CheckRecord(); // chechs if it's time to record
            
            if (simTime >= recordingTime)
            {
                endSim = true;
            } // checks if the simulation need to end

		}

        public static void RunSim() // runs the simulation
		{
			while (true)
			{
				simTime++; // adds one to the current time
				Update(); // updates everything in the simulation
				if (endSim) // stops the simulation when the end has come
				{
                    Console.WriteLine("Ending Simulation");
					break;
				}
			}
		}

		public static void MakeXOrganisms (int numberOfOrganism, double organismSize, double organismViewDistance, double organismSpeed, string organismType) // the given amount of the wanted organism in a random location within the spawning sphere
		{
			for (int i = 0; i < numberOfOrganism; i++) 
			{
				Vector position = new Vector(random.Next(-spawnSphereRadius, spawnSphereRadius), random.Next(-spawnSphereRadius, spawnSphereRadius), random.Next(-spawnSphereRadius, spawnSphereRadius));
				new Organism(position, organismSize, baseSaturation, organismViewDistance, organismSpeed, organismType);
			}
		}

		public static void SpawnXFood(int numberOfFood) // makes the given amount food at random locations with in the spawning sphere 
		{
			for (int i = 0; i < numberOfFood; i++)
			{
				Vector position = new Vector(random.Next(-spawnSphereRadius, spawnSphereRadius), random.Next(-spawnSphereRadius, spawnSphereRadius), random.Next(-spawnSphereRadius, spawnSphereRadius));
				new Food(position, foodSize, foodValue);
			}
		}

		private static void ForceStopSim()
		{
			if (simTime > 100000000)
			{
				Console.WriteLine("Force stopping simulation, time limit exceded");
				endSim = true;
			}
		}

	}
}