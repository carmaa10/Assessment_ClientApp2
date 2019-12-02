using System;
using System.Collections.Generic;
using System.IO;

namespace Assessment_ClientApp2
{

    // **************************************************
    //
    // Assessment: Client App 2.0
    // Author: Carma Aten
    // Dated: 11/27/2019
    // Level (Novice, Apprentice, or Master): 
    //
    // **************************************************    

    class Program
    {
        /// <summary>
        /// Main method - app starts here
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //
            // initialize monster list from method
            //
            //List<Monster> monsters = InitializeMonsterList();
            List<Monster> monsters = ReadMonstersFromFile(@"Data\Data.txt");

            //
            // read monsters from data file
            //
            //List<Monster> monsters = ReadFromDataFile();

            //
            // application flow
            //
            DisplayWelcomeScreen();
            DisplayMenuScreen(monsters);
            DisplayClosingScreen();
        }

        #region UTILITY METHODS

        /// <summary>
        /// initialize a list of monsters
        /// </summary>
        /// <returns>list of monsters</returns>
        static List<Monster> InitializeMonsterList()
        {
            //
            // create a list of monsters
            // note: list and object initializers used
            //
            List<Monster> monsters = new List<Monster>()
            {

                new Monster()
                {
                    Name = "Sid",
                    Age = 145,
                    Attitude = Monster.EmotionalState.happy,
                    InTribe = Monster.Tribe.geeks,
                    Active = true
                },

                new Monster()
                {
                    Name = "Lucy",
                    Age = 125,
                    Attitude = Monster.EmotionalState.bored,
                    InTribe = Monster.Tribe.nerds,
                    Active = false
                },

                new Monster()
                {
                    Name = "Bill",
                    Age = 934,
                    Attitude = Monster.EmotionalState.sad,
                    InTribe = Monster.Tribe.weirdos,
                    Active = true
                },

                new Monster()
                {
                    Name = "Angela",
                    Age = 5887,
                    Attitude = Monster.EmotionalState.angry,
                    InTribe = Monster.Tribe.nerds,
                    Active = false
                },

                new Monster()
                {
                    Name = "Desi",
                    Age = 3,
                    Attitude = Monster.EmotionalState.bored,
                    InTribe = Monster.Tribe.weirdos,
                    Active = true
                }

            };

            return monsters;
        }

        static List<Monster> ReadMonstersFromFile(string path)
        {
            List<Monster> monsters = new List<Monster>();
            string[] monstersString = File.ReadAllLines(path);
            Monster newMonster = new Monster();

            foreach (string line in monstersString)
            {
                string[] newMonsterString = line.Split(',');

                // Assign values to a monster
                newMonster.Name = newMonsterString[0];

                int.TryParse(newMonsterString[1], out int age);
                newMonster.Age = age;

                Enum.TryParse(newMonsterString[2], out Monster.EmotionalState attitude);
                newMonster.Attitude = attitude;

                Enum.TryParse(newMonsterString[3], out Monster.Tribe tribe);
                newMonster.InTribe = tribe;

                bool.TryParse(newMonsterString[4], out bool active);
                newMonster.Active = active;

                monsters.Add(newMonster);
            }

            return monsters;
        }

        #endregion

        #region SCREEN DISPLAY METHODS

        /// <summary>
        /// SCREEN: display and process menu options
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayMenuScreen(List<Monster> monsters)
        {
            bool quitApplication = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) List All Monsters");
                Console.WriteLine("\tb) View Monster Detail");
                Console.WriteLine("\tc) Add Monster");
                Console.WriteLine("\td) Delete Monster");
                Console.WriteLine("\te) Update Monster");
                Console.WriteLine("\tf) Write To Data File");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayAllMonsters(monsters);
                        break;

                    case "b":
                        DisplayViewMonsterDetail(monsters);
                        break;

                    case "c":
                        DisplayAddMonster(monsters);
                        break;

                    case "d":
                        DisplayDeleteMonster(monsters);
                        break;

                    case "e":
                        DisplayUpdateMonster(monsters);
                        break;

                    case "f":
                        DisplayWriteToDataFile(monsters);
                        break;
                        
                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitApplication);
        }

        /// <summary>
        /// SCREEN: list all monsters
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayAllMonsters(List<Monster> monsters)
        {
            DisplayScreenHeader("All Monsters");

            foreach (Monster monster in monsters)
            {
                MonsterInfo(monster);
                Console.WriteLine();
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: monster detail
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayViewMonsterDetail(List<Monster> monsters)
        {
            DisplayScreenHeader("Monster Detail");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string monsterName = Console.ReadLine();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // display monster detail
            //
            Console.WriteLine();
            Console.WriteLine("\t*********************");
            MonsterInfo(selectedMonster);
            Console.WriteLine("\t*********************");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: add a monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayAddMonster(List<Monster> monsters)
        {
            Monster newMonster = new Monster();
            bool couldParse = false;

            DisplayScreenHeader("Add Monster");

            //
            // add monster object property values
            //
            Console.Write("\tName: ");
            newMonster.Name = Console.ReadLine();

            do
            {
                Console.Write("\tAge: ");
                couldParse = int.TryParse(Console.ReadLine(), out int age);
                newMonster.Age = age;
            } while (!couldParse);

            couldParse = false;
            do
            {
                Console.Write("\tAttitude: ");
                couldParse = Enum.TryParse(Console.ReadLine(), out Monster.EmotionalState attitude);
                newMonster.Attitude = attitude;
            } while (!couldParse);

            couldParse = false;
            do
            {
                Console.WriteLine("\tTribe: ");
                couldParse = Enum.TryParse(Console.ReadLine(), out Monster.Tribe tribe);
                newMonster.InTribe = tribe;
            } while (!couldParse);

            couldParse = false;
            do
            {
                Console.WriteLine("\tStatus of Activity: ");
                couldParse = bool.TryParse(Console.ReadLine(), out bool active);
                newMonster.Active = active;
            } while (!couldParse);




            //
            // echo new monster properties
            //
            Console.WriteLine();
            Console.WriteLine("\tNew Monster's Properties");
            Console.WriteLine("\t-------------");
            MonsterInfo(newMonster);
            Console.WriteLine();
            Console.WriteLine("\t-------------");

            DisplayContinuePrompt();

            monsters.Add(newMonster);
        }

        /// <summary>
        /// SCREEN: delete monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayDeleteMonster(List<Monster> monsters)
        {
            DisplayScreenHeader("Delete Monster");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string monsterName = Console.ReadLine();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // delete monster
            //
            if (selectedMonster != null)
            {
                monsters.Remove(selectedMonster);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedMonster.Name} deleted");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{monsterName} not found");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: update monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        /// 
        static void DisplayUpdateMonster(List<Monster> monsters)
        {
            bool validResponse = false;
            Monster selectedMonster = null;

            do
            {
                DisplayScreenHeader("Update Monster");

                //
                // display all monster names
                //
                Console.WriteLine("\tMonster Names");
                Console.WriteLine("\t-------------");
                foreach (Monster monster in monsters)
                {
                    Console.WriteLine("\t" + monster.Name);
                }

                //
                // get user monster choice
                //
                Console.WriteLine();
                Console.Write("\tEnter name:");
                string monsterName = Console.ReadLine();

                //
                // get monster object
                //

                foreach (Monster monster in monsters)
                {
                    if (monster.Name == monsterName)
                    {
                        selectedMonster = monster;
                        validResponse = true;
                        break;
                    }
                }

                //
                // feedback for wrong name choice
                //
                if (!validResponse)
                {
                    Console.WriteLine("\tPlease select a correct name.");
                    DisplayContinuePrompt();
                }

                //
                // update monster
                //

            } while (!validResponse);


            //
            // update monster properties
            //
            string userResponse;
            Console.WriteLine();
            Console.WriteLine("\tReady to update. Press the Enter to keep the current info.");
            Console.WriteLine();
            Console.Write($"\tCurrent Name: {selectedMonster.Name} New Name: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                selectedMonster.Name = userResponse;
            }

            Console.Write($"\tCurrent Age: {selectedMonster.Age} New Age: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                bool couldParse = false;
                int age;
                do
                {
                    couldParse = int.TryParse(userResponse, out age);
                } while (!couldParse);
                selectedMonster.Age = age;
            }

            Console.Write($"\tCurrent Attitude: {selectedMonster.Attitude} New Attitude: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                bool couldParse = false;
                Monster.EmotionalState attitude;

                do
                {
                    couldParse = Enum.TryParse(userResponse, out attitude);
                } while (!couldParse);
                selectedMonster.Attitude = attitude;
            }

            Console.Write($"\tCurrent Tribe: {selectedMonster.InTribe} New Tribe: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                bool couldParse = false;
                Monster.Tribe tribe;

                do
                {
                    couldParse = Enum.TryParse(userResponse, out tribe);
                } while (!couldParse);
                
                selectedMonster.InTribe = tribe;
            }

            Console.Write($"\tCurrent Status of Activity: {selectedMonster.Active} New Status of Activity: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                bool couldParse;
                bool active;

                do
                {
                    couldParse = bool.TryParse(userResponse, out active);
                } while (!couldParse);
                selectedMonster.Active = active;
            }

            //
            // echo updated monster properties
            //
            Console.WriteLine();
            Console.WriteLine("\tNew Monster's Properties");
            Console.WriteLine("\t-------------");
            MonsterInfo(selectedMonster);
            Console.WriteLine();
            Console.WriteLine("\t-------------");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: write list of monsters to data file
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayWriteToDataFile(List<Monster> monsters)
        {
            DisplayScreenHeader("Write to Data File");

            //
            // prompt the user to continue
            //
            Console.WriteLine("\tThe application is ready to write to the data file.");
            Console.Write("\tEnter 'y' to continue or 'n' to cancel.");
            if (Console.ReadLine().ToLower() == "y")
            {
                DisplayContinuePrompt();
                WriteToDataFile(monsters);
                //
                // TODO process I/O exceptions
                //

                Console.WriteLine();
                Console.WriteLine("\tList written to data the file.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("\tList not written to the data file.");
            }

            DisplayContinuePrompt();
        }

        #endregion

        #region FILE I/O METHODS

        /// <summary>
        /// write monster list to data file
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void WriteToDataFile(List<Monster> monsters)
        {
            string[] monstersString = new string[monsters.Count];

            //
            // create array of monster strings
            //
            for (int index = 0; index < monsters.Count; index++)
            {
                string monsterString =
                    monsters[index].Name + "," +
                    monsters[index].Age + "," +
                    monsters[index].Attitude + "," + 
                    monsters[index].InTribe + "," +
                    monsters[index].Active;

                monstersString[index] = monsterString;
            }

            File.WriteAllLines("Data\\Data.txt", monstersString);
        }

        /// <summary>
        /// read monsters from data file and return a list of monsters
        /// </summary>
        /// <returns>list of monsters</returns>        
        static List<Monster> ReadFromDataFile()
        {
            List<Monster> monsters = new List<Monster>();

            //
            // read all lines in the file
            //
            string[] monstersString = File.ReadAllLines("Data\\Data.txt");

            //
            // create monster objects and add to the list
            //
            foreach (string monsterString in monstersString)
            {
                //
                // get individual properties
                //
                string[] monsterProperties = monsterString.Split(',');

                //
                // create monster
                //
                Monster newMonster = new Monster();

                //
                // update monster property values
                //
                newMonster.Name = monsterProperties[0];

                int.TryParse(monsterProperties[1], out int age);
                newMonster.Age = age;

                Enum.TryParse(monsterProperties[2], out Monster.EmotionalState attitude);
                newMonster.Attitude = attitude;

                //
                // add new monster to list
                //
                monsters.Add(newMonster);
            }

            return monsters;
        }

        #endregion

        #region CONSOLE HELPER METHODS

        /// <summary>
        /// display all monster properties
        /// </summary>
        /// <param name="monster">monster object</param>
        static void MonsterInfo(Monster monster)
        {
            Console.WriteLine("\t********************************************************************************");
            Console.WriteLine("\tName".PadRight(15) + 
                "Age".PadRight(5) + 
                "Attitude".PadRight(10) + 
                "Tribe".PadRight(10) + 
                "Status Of Activity".PadRight(20) + 
                "Greeting".PadRight(30));
            Console.WriteLine("\t********************************************************************************");
            Console.WriteLine($"\t{monster.Name}".PadRight(15) + 
                $"{monster.Age}".PadRight(5) + 
                $"{monster.Attitude}".PadRight(10) + 
                $"{monster.InTribe}".PadRight(10) + 
                $"{monster.Active}".PadRight(20) +
                $"{monster.Greeting()}".PadRight(30));

            //Console.WriteLine($"\tName: {monster.Name}");
            //Console.WriteLine($"\tAge: {monster.Age}");
            //Console.WriteLine($"\tAttitude: {monster.Attitude}");
            //Console.WriteLine($"\tTribe: {monster.InTribe}");
            //Console.WriteLine($"\tStatus of Activity: {monster.Active}");
            //Console.WriteLine("\t" + monster.Greeting());
        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThe Monster Tracker");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using the Monster Tracker!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.Write("\tPress any key to continue.");
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
