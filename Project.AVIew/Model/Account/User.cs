﻿using System;

namespace Project.AVIew.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
