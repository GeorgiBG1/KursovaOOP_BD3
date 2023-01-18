using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KursovaNet5BD.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using KursovaNet5BD.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;

namespace KursovaOOP_BD3.Services
{
    internal class DBMaster : IDBMaster
    {
        private DiabloContext _dbContext;
        public DBMaster()
        {
            _dbContext = new DiabloContext();
        }
        public Character GetCharacterById(int id) //a)
        {
            return _dbContext.Characters.Include(s => s.Statistic).FirstOrDefault(c => c.Id == id)!;
        }
        public List<Character> GetAllCharacters() //b)
        {
            List<Character> statistics = _dbContext.Characters
                .Include(s => s.Statistic)
                .OrderBy(s => s.Statistic!.Strength)
                .ThenBy(s => s.Statistic!.Defence)
                .ToList();
            return statistics;
        }
        public List<Character> GetCharactersInGame(int gameId) //c)
        {
            //var result = _dbContext.Games.FirstOrDefault(c => c.Id == gameId)!.UsersGames.Select(ug => ug.Character).ToList();

            var result = _dbContext.UsersGames
                .Where(c => c.GameId == gameId)
                .Include(ug => ug.Character)
                .ThenInclude(c => c.Statistic)
                .Select(ug => ug.Character)
                .ToList();
            return result;
        }
        public List<User> GetUsersInGame(int gameId) //d)
        {
            var result = _dbContext.UsersGames //1st way
                .Where(c => c.GameId == gameId)
                .Select(ug => ug.User)
                .ToList();
            var result2 = _dbContext.Users //2nd way
                .SelectMany(u => u.UsersGames)
                .Where(ug => ug.GameId == gameId)
                .Select(ug => ug.User)
                .ToList();
            return result;
        }
        public List<Character> GetUsersInGames(int gameId) //e)
        {
            var result = _dbContext.UsersGames
                .Where(ug => ug.GameId == gameId)
                .Include(ug => ug.Character)
                .ThenInclude(c => c.Statistic)
                .Select(ug => ug.Character)
                .ToList();
            return result;
        }
        public List<UsersGame> GetUserGamesAfterDate(DateTime joinTime) //f
        {
            var result = _dbContext.UsersGames
                .Include(ug => ug.Game!)
                .Include(ug => ug.User!)
                .Where(ug => ug.JoinedOn >= joinTime)
                .ToList();
            return result;
        }
        public List<UsersGame> GetUserGamesAfterDate(int year, int month = 0, int day = 0) //g)
        {
            var result = _dbContext.UsersGames
                .Include(ug => ug.Game)
                .Include(ug => ug.User!)
                .Where(ug => ug.JoinedOn.Year >= year)
                .Where(ug => ug.JoinedOn.Month >= month)
                .Where(ug => ug.JoinedOn.Day >= day)
                .ToList();
            return result;
        }
        public List<UsersGame> GetUserGamesWorthMoreThan(decimal minimumFee) //h)
        {
            var result = _dbContext //1st way
                .UsersGames
                .Include(ug => ug.Game)
                .Include(u => u.User!)
                .Where(ug => ug.Cash > minimumFee)
                .ToList();
            var result2 = _dbContext.Users //2nd way
                .SelectMany(u => u.UsersGames)
                .Include(ug => ug.User!)
                .Include(ug => ug.Game)
                .Where(ug => ug.Cash > minimumFee)
                .ToList();
            return result;
        }
        public List<Item> GetAllItems(int minStrenth = 0, int minDefence = 0, int minSpeed = 0) //i)
        {
            var result = _dbContext.Items
                .Include(iT => iT.ItemType)
                .Where(i => i.Statistic.Strength! > minStrenth)
                .Where(i => i.Statistic.Defence! > minDefence)
                .Where(i => i.Statistic.Speed! > minSpeed)
                .ToList();
            return result;
        }
        public List<Item> GetForbiddenItemsForGame(int gameId) //j
        {
            //var result = _dbContext.Games
            //    .Include(g => g.GameType)
            //    .ThenInclude(gt => gt.Items)
            //    .FirstOrDefault(x => x.Id == gameId)
            //    .GameType.Items.ToList();


            var result = _dbContext.Items.Where(i => i.GameTypeForbiddenItems.Where(x => x.GameTypeId == gameId).Any()).ToList();
            if (result.Count == 0)
            {
                Console.WriteLine("There aren't any forbidden items for this game!");
            }
            return result;
        }
        public List<Character> GetCharactersOfUser(int userId) //k
        {
            var characters1 = _dbContext.Users //1st way
                .SelectMany(u => u.UsersGames)
                .Where(x => x.UserId == userId)
                .Include(c => c.Character)
                .ThenInclude(c => c.Statistic)
                .Select(c => c.Character)
                .ToList();
            var characters2 = _dbContext.UsersGames //2nd way
                .Where(ug => ug.UserId == userId)
                .Include(c => c.Character)
                .ThenInclude(c => c.Statistic)
                .Select(ug => ug.Character)
                .ToList();
            if (characters2.Count == 0)
            {
                Console.WriteLine("0 characters!");
            }
            return characters2;
        }
        public List<Item> GetAllItemsOfUser(int userId) //l)
        {
            var usersGames = _dbContext.Users
                .Where(u => u.Id == userId)
                .SelectMany(c => c.UsersGames)
                .Include(ug => ug.UserGameItems)
                .ThenInclude(ug => ug.Item)
                .ToList();
            var items = usersGames
                .SelectMany(ug => ug.UserGameItems)
                .Select(ugi => ugi.Item)
                .ToList();
            return items;
        }
        public Dictionary<int, string> GetAllItemTypes() //m)
        {
            var dictionary = new Dictionary<int, string>();
            foreach (var item in _dbContext.ItemTypes)
            {
                dictionary.Add(item.Id, item.Name);
            }
            dictionary = dictionary
                .GroupBy(pair => pair.Value)
                .Select(group => group.First())
                .ToDictionary(pair => pair.Key, pair => pair.Value);
            return dictionary;
        }
        public Item CreateNewItem(string name, int typeId, decimal price, int minLvl, int strength, int defence, int mind = 0, int speed = 0, int luck = 0) //n)
        {
            using (var dbContext = _dbContext)
            {
                ; Item item = new Item()
                {
                    Name = name,
                    Price = price,
                    MinLevel = minLvl,
                    ItemTypeId = typeId,
                    Statistic = new Statistic()
                    {
                        Strength = strength,
                        Defence = defence,
                        Mind = mind,
                        Speed = speed,
                        Luck = luck
                    }
                };
                dbContext.Items.Add(item);
                dbContext.SaveChanges();
            }
            return new Item();
        }
        public bool IsItemAllowedForThatGame(Item item, Game game) //o)
        {
            bool result = false;
            try
            {
                result = _dbContext.GameTypeForbiddenItems.Include(x => x.GameType).Where(x => x.GameTypeId == game.GameTypeId).Where(i => i.Item.Id == item.Id)//.Select(x => $"{x.ItemId}. {x.Item.Name}").ToList();
               .Any();
            }
            catch (Exception)
            { Console.WriteLine("An error occurred. Try again!"); return false; }
            if (result)
            {
                return false;
            }
            return true;
        }
        public List<Game> GetAllGames()
        {
            List<Game> games = _dbContext.Games.ToList();
            return games;
        }
        public List<Item> GetAllItems()
        {
            List<Item> items = _dbContext.Items.Include(i => i.Statistic).ToList();
            return items;
        }
    }
}
