﻿using System;
using System.Collections.Generic;

#nullable disable

namespace KursovaNet5BD.Models
{
    public partial class UserGameItem
    {
        public int ItemId { get; set; }
        public int UserGameId { get; set; }

        public virtual Item Item { get; set; }
        public virtual UsersGame UserGame { get; set; }
    }
}
