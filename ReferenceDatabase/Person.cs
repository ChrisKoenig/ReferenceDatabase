using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;

namespace ReferenceDatabase
{
    [Table(Name = "People")]
    public class Person
    {
        [Column(
            IsPrimaryKey = true,
            IsDbGenerated = true,
            CanBeNull = false,
            AutoSync = AutoSync.OnInsert,
            DbType = "INT NOT NULL IDENTITY")]
        public int PersonId { get; set; }

        [Column]
        public string FirstName { get; set; }

        [Column]
        public string LastName { get; set; }

        [Column]
        public string FavoriteColor { get; set; }

        [Column]
        public DateTime BirthDate { get; set; }
    }
}