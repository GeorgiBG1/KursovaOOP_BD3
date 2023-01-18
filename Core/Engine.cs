
using KursovaOOP_BD3.Services;
using KursovaNet5BD.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KursovaNet5BD.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading;
using System.Runtime;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace KursovaOOP_BD3.Core
{
    public class Engine
    {
        public void Run()
        {
            Console.Title = "Diablo app";
            var options = Menu().Select(d=>d.Key).ToList();
            string input = Console.ReadLine()!;
            Console.WriteLine();
            input.ToLower();
            while (true)
            {
                input.ToLower();
                if (input == options[0])
                {
                    TestGetCharacterById();
                }
                else if (input == options[1])
                {
                    TestGetAllCharacters();
                }
                else if (input == options[2])
                {
                    TestGetCharacterInGame();
                }
                else if (input == options[3])
                {
                    TestGetUsersInGame();
                }
                else if (input == options[4])
                {
                    TestGetUsersInGames();
                }
                else if (input == options[5])
                {
                    TestGetUserGamesAfterDate();
                }
                else if (input == options[6])
                {
                    TestGetUserGamesAfterDate2();
                }
                else if (input == options[7])
                {
                    TestGetUserGamesWorthMoreThan();
                }
                else if (input == options[8])
                {
                    TestGetAllItems();
                }
                else if (input == options[9])
                {
                    TestGetForbiddenItemsForGame();
                }
                else if (input == options[10])
                {
                    TestGetCharactersOfUser();
                }
                else if (input == options[11])
                {
                    TestGetAllItemsOfUser();
                }
                else if (input == options[12])
                {
                    TestGetAllItemTypes();
                }
                else if (input == options[13])
                {
                    TestCreateNewItem();
                }
                else if (input == options[14])
                {
                    Console.WriteLine("o) public bool IsItemAllowedForThatGame(Item item, Game game)");
                    Console.WriteLine("Auto check\nHow many items you want to check?");
                    int count = int.Parse(Console.ReadLine()!);
                    int counter = 0;
                    while (counter < count)
                    {
                        TestIsItemAllowedForThatGame();
                        counter++;
                    }
                    DrawSeparator();
                }
                else
                { Console.WriteLine("Bye"); break; }
                Menu();
                input = Console.ReadLine()!;
                Console.WriteLine();
            }
        }
        public void TestGetCharacterById()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("a) public Character GetCharacterById(int id)\nEnter character id:");
            int input = int.Parse(Console.ReadLine()!);
            Character characterById = dB.GetCharacterById(input);
            if (characterById != null)
            {
                Console.WriteLine(characterById);
            }
            else
            {
                Console.WriteLine("There is no character on this id!");
            }
            DrawSeparator();
        }
        public void TestGetAllCharacters()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("b) public List<Character> GetAllCharacters()");
            List<Character> result = dB.GetAllCharacters();
            Console.WriteLine(string.Join("\n", result));
            DrawSeparator();
        }
        public void TestGetCharacterInGame()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("c) public List<Character> GetCharactersInGame(int gameId)\nEnter game id:");
            int input = int.Parse(Console.ReadLine()!);
            List<Character> characters = dB.GetCharactersInGame(input);
            if (characters.Count != 0)
            {
                Console.WriteLine(string.Join("\n", characters));
            }
            else
            {
                Console.WriteLine("0 characters in this game!");
            }
            DrawSeparator();
        }
        public void TestGetUsersInGame()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("d) public List<User> GetUsersInGame(int gameId)\nEnter game id:");
            int input = int.Parse(Console.ReadLine()!);
            List<User> users = dB.GetUsersInGame(input);
            if (users.Count != 0)
            {
                Console.WriteLine(string.Join("\n", users));
            }
            else
            {
                Console.WriteLine("0 users in this game!");
            }
            DrawSeparator();
        }
        public void TestGetUsersInGames()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("e) public List<Character> GetUsersInGame(int gameId)\nEnter game id:");
            int input = int.Parse(Console.ReadLine()!);
            List<Character> users = dB.GetUsersInGames(input);
            if (users.Count != 0)
            {
                Console.WriteLine(string.Join("\n", users));
            }
            else
            {
                Console.WriteLine("0 characters in this game!");
            }
            DrawSeparator();
        }
        public void TestGetUserGamesAfterDate()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("f) public List<UserGames> GetUserGamesAfterDate(DateTime joinTime)");
            DateTime joinTime = new DateTime(2013, 01, 01);
            List<UsersGame> ugames = dB.GetUserGamesAfterDate(joinTime);
            //   var result = userGames.GroupBy(x => x.User).ToList();
            Dictionary<User, List<Game>> usersGames = new Dictionary<User, List<Game>>();
            foreach (var ug in ugames)
            {
                if (!usersGames.ContainsKey(ug.User))
                {
                    usersGames[ug.User] = new List<Game>();
                }
                usersGames[ug.User].Add(ug.Game);
            }
            int counter = 0;
            foreach (var kvp in usersGames)
            {
                Console.WriteLine("User number:" + ++counter + "\n" + kvp.Key);
                int gameCounter = 0;
                foreach (var game in kvp.Value)
                {
                    Console.WriteLine("Game number: " + (++gameCounter) + "\n" + game);
                }
            }
            DrawSeparator();
        }
        public void TestGetUserGamesAfterDate2()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("g) public List<UserGames> GetUserGamesAfterDate(int year, int month=0, int day=0)");
            DateTime joinTime = new DateTime(2013, 01, 01);
            int year = joinTime.Year;
            int month = joinTime.Month;
            int day = joinTime.Day;
            List<UsersGame> userGames = dB.GetUserGamesAfterDate(year, month, day);//or only year
            Dictionary<User, List<Game>> usersGames = new Dictionary<User, List<Game>>();

            foreach (var ug in userGames)
            {
                if (!usersGames.ContainsKey(ug.User))
                {
                    usersGames[ug.User] = new List<Game>();
                }
                usersGames[ug.User].Add(ug.Game);
            }

            int counter = 0;
            foreach (var kvp in usersGames)
            {
                Console.WriteLine("User number:" + ++counter + "\n" + kvp.Key);
                int gameCounter = 0;
                foreach (var game in kvp.Value)
                {
                    Console.WriteLine("Game number: " + (++gameCounter) + "\n" + game);
                }
            }
            DrawSeparator();
        }
        public void TestGetUserGamesWorthMoreThan()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("h) public List<UserGames> GetUserGamesWorthMoreThan(decimal minimumFee)");
            decimal minimumFee = 6010.54m; //5000, 6010.54
            List<UsersGame> userGames = dB.GetUserGamesWorthMoreThan(minimumFee);
            Console.WriteLine(string.Join("\n", userGames));
            DrawSeparator();
        }
        public void TestGetAllItems()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("i) public List<Item> GetAllItems(int minStrenth=0,int minDefence=0,int minSpeed=0)\n" +
                "Do you want to set custom values for:\nMinimum strength\nMinimum defence\nMinimum speed\nYes/No");
            string input = Console.ReadLine()!;
            if (input == "yes" || input == "Yes")
            {
                int minStrength = int.Parse(Console.ReadLine())!;
                int minDefence = int.Parse(Console.ReadLine())!;
                int minSpeed = int.Parse(Console.ReadLine())!;
                List<Item> customItems = dB.GetAllItems(minStrength, minDefence, minSpeed);
                Console.WriteLine(string.Join("\n", customItems));
            }
            else
            {
            List<Item> items = dB.GetAllItems();
            Console.WriteLine(string.Join("\n", items));
            }
            DrawSeparator();
        }
        public void TestGetForbiddenItemsForGame()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("j) public List<Item> GetForbiddenItemsForGame(int gameId)\nEnter game id:");
            int gameId = int.Parse(Console.ReadLine()!);
            List<Item> items = dB.GetForbiddenItemsForGame(gameId);
            if (items.Count != 0)
            {
                Console.WriteLine("Forbidden items:\n" + string.Join("\n", items));
            }
            DrawSeparator();
        }
        public void TestGetCharactersOfUser()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("k) public List<Character> GetCharactersOfUser(int UserId)\nEnter user id:");
            int userId = int.Parse(Console.ReadLine()!);
            List<Character> characters = dB.GetCharactersOfUser(userId);
            Console.WriteLine(string.Join("\n", characters));
            DrawSeparator();
        }
        public void TestGetAllItemsOfUser()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("l) public List<Item> GetAllItemsOfUser(int userId)\nEnter user id:");
            int userId = int.Parse(Console.ReadLine()!);
            List<Item> items = dB.GetAllItemsOfUser(userId);
            Console.WriteLine(string.Join("\n", items));
            DrawSeparator();
        }
        public void TestGetAllItemTypes()
        {
            IDBMaster dB = new DBMaster();
            Dictionary<int, string> dictionary = dB.GetAllItemTypes();
            Console.WriteLine("m) public Dictionary<int,string> GetAllItemTypes()\nAll item types:");
            dictionary = dictionary
                    .OrderBy(d => d.Value)
                    .ToDictionary(d => d.Key, d => d.Value);
            int counter = 1;
            foreach (var itemType in dictionary)
            {
                Console.WriteLine($"{counter}. {itemType.Value}");
                counter++;
            }
            DrawSeparator();
        }
        public void TestCreateNewItem()
        {
            IDBMaster dB = new DBMaster();
            Console.WriteLine("n) public Item CreateNewItem(string name, int typeId, decimal price, int minLvl,\n" +
                "int strength, int defence, int mind=0,int speed=0, int luck=0)\n");
            string name = string.Empty; // "Engulfing lightning";
            decimal price = 0; // 534.12m;
            int minLvl = 0; // 17;
            int typeId = 0; // 4;
            int strength = 0; // 15;
            int defence = 0; // 6;
            int mind = 0; // 3;
            int speed = 0; // 25;
            int luck = 0; // 8;
            DBMaster specialDB = new DBMaster();
            Random rnd = new Random();
            #region Random Type
            var allItemTypes = dB.GetAllItemTypes();
            typeId = rnd.Next(allItemTypes.Count);
            #endregion
            Console.WriteLine($"Your first item is: {allItemTypes.Where(it => it.Key == typeId).FirstOrDefault().Value}"
            + "\nEnter name:");
            name = Console.ReadLine()!;
            Console.WriteLine("Enter price:");
            price = decimal.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter minimum level:");
            minLvl = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter strength:");
            strength = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter defence:");
            defence = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Do you want to customize mind, speed and luck? Yes/No");
            string customizeOption = Console.ReadLine()!;
            bool custom = false;
            if (customizeOption == "yes" || customizeOption == "Yes")
            {
                Console.WriteLine("Enter mind:");
                mind = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Enter speed:");
                speed = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Enter luck:");
                luck = int.Parse(Console.ReadLine()!);
                custom = true;
            }
            Console.WriteLine("Do you want to save this item? Yes/No");
            string saveOption = Console.ReadLine()!;
            if (saveOption == "yes" || saveOption == "Yes")
            {
                Console.Clear();
                if (custom)
                {
                dB.CreateNewItem(name, typeId, price, minLvl, strength, defence, mind, speed, luck);
                }
                else { dB.CreateNewItem(name, typeId, price, minLvl, strength, defence); }
                var myNewItem = specialDB.GetAllItems().LastOrDefault();
                Console.WriteLine($"Last item\n{myNewItem}\nItem statistics:\n{myNewItem.Statistic}");
            }
            DrawSeparator();
        }
        public void TestIsItemAllowedForThatGame()
        {
            IDBMaster dB = new DBMaster();
            DBMaster specialDB = new DBMaster();
            Random rnd = new Random();
            #region Random game
            List<Game> allGames = specialDB.GetAllGames();
            int randomGameId = rnd.Next(1,allGames.Count);
            Game randomGame = allGames.FirstOrDefault(g => g.Id == randomGameId);
            #endregion
            #region Random item
            List<Item> allItems = specialDB.GetAllItems();
            int randomItemId = rnd.Next(1,allItems.Count);
            Item randomItem = allItems.LastOrDefault(i => i.Id == randomItemId);
            #endregion
            Item item = new Item() { Id = 7 }; //allowed with id=7 or id=9
            Game game = new Game() { GameTypeId = 2 }; // allowed with gameTypeId = 2 and not allowed with id=1
            bool result = dB.IsItemAllowedForThatGame(randomItem, randomGame);
            if (result)
            { 
                Console.WriteLine($"Item: ({randomItem.Name}) in game ({randomGame.Name})\n" +
                $"This item is allowed for that game!"); 
            }
            else
            {
                Console.WriteLine($"Item: ({randomItem.Name}) in game ({randomGame.Name})\n" + 
                $"This item is not allowed for that game!"); 
            }
            Console.WriteLine();
        }
        private static void DrawSeparator()
        {
            Console.WriteLine(new String('~', 30));
        }
        private static Dictionary<string, string> Menu()
        {
            Dictionary<string, string> pairs = new()
            {
                { "a", "a) public Character GetCharacterById(int id)" },
                { "b", "b) public List<Character> GetAllCharacters()" },
                { "c", "c) public List<Character> GetCharactersInGame(int gameId)" },
                { "d", "d) public List<User> GetUsersInGame(int gameId)" },
                { "e", "e) public List<Character> GetUsersInGame(int gameId)" },
                { "f", "f) public List<UserGames> GetUserGamesAfterDate(DateTime joinTime)" },
                { "g", "g) public List<UserGames> GetUserGamesAfterDate(int year, int month=0, int day=0)" },
                { "h", "h) public List<UserGames> GetUserGamesWorthMoreThan(decimal minimumFee)" },
                { "i", "i) public List<Item> GetAllItems(int minStrenth=0,int minDefence=0,int minSpeed=0)" },
                { "j", "j) public List<Item> GetForbiddenItemsForGame(int gameId)" },
                { "k", "k) public List<Character> GetCharactersOfUser(int UserId)" },
                { "l", "l) public List<Item> GetAllItemsOfUser(int userId)" },
                { "m", "m) public Dictionary<int,string> GetAllItemTypes()" },
                { "n", "n) public Item CreateNewItem(string name, int typeId, decimal price, int minLvl,\nint strength, int defence, int mind=0,int speed=0, int luck=0)" },
                { "o", "o) public bool IsItemAllowedForThatGame(Item item, Game game)" },
            };
            Console.WriteLine($"{pairs.Count} methods:\nEnter:");
            foreach (var item in pairs)
            {
                Console.WriteLine($"{item.Key} -> {item.Value}");
            }
            return pairs;
        }
    }
}

