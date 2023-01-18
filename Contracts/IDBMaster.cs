using KursovaNet5BD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovaNet5BD.Contracts
{
    internal interface IDBMaster
    {
        Item CreateNewItem(string name, int typeId, decimal price, int minLvl, int strength, int defence, int mind = 0, int speed = 0, int luck = 0);
        List<Character> GetAllCharacters();
        List<Item> GetAllItems(int minStrenth = 0, int minDefence = 0, int minSpeed = 0);
        List<Item> GetAllItemsOfUser(int userId);
        Dictionary<int, string> GetAllItemTypes();
        Character GetCharacterById(int id);
        List<Character> GetCharactersInGame(int gameId);
        List<Character> GetCharactersOfUser(int userId);
        List<Item> GetForbiddenItemsForGame(int gameId);
        List<UsersGame> GetUserGamesAfterDate(DateTime joinTime);
        List<UsersGame> GetUserGamesAfterDate(int year, int month = 0, int day = 0);
        List<UsersGame> GetUserGamesWorthMoreThan(decimal minimumFee);
        List<User> GetUsersInGame(int gameId);
        List<Character> GetUsersInGames(int gameId);
        bool IsItemAllowedForThatGame(Item item, Game game);
    }
}
