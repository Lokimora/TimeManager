using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Auth.Model
{
    public class User : PrimaryKeyModel
    {
        
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        /// <summary>
        /// Подтвердил ли пользователь свою учетную запись
        /// </summary>
        public bool IsConfirm { get; set; } 

        public string ConfirmationHash { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Registration { get; set; }

    }
}
