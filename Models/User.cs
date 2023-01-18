using System;
using System.Collections.Generic;

#nullable disable

namespace KursovaNet5BD.Models
{
    public partial class User
    {
        public User()
        {
            UsersGames = new HashSet<UsersGame>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsDeleted { get; set; }
        public string IpAddress { get; set; }

        public virtual Item IdNavigation { get; set; }
        public virtual ICollection<UsersGame> UsersGames { get; set; }
        public override string ToString()
        {
            return $"User information:\nId: {Id}\n" +
                $"Username: {Username}\n" +
                $"First name: {FirstName}\n" +
                $"Last name: {LastName}\n" +
                $"Email: {Email}\n" +
                $"Registration date: {RegistrationDate}\n" +
                $"IpAddress: {IpAddress}\n";
        }
    }
}
