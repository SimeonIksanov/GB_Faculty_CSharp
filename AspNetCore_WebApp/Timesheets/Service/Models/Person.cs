﻿using System;
namespace Service.Models
{
    public class Person
    {
        public int Id { get; set; } = -1;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public int Age { get; set; }
    }
}
