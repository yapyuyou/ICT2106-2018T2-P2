﻿namespace SmartHome.Models
{
    public abstract class Account : MongoDbObject,IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsLogin { get; set; }
    }
}