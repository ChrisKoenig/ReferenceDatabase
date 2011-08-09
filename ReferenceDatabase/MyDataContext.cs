using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace ReferenceDatabase
{
    public class MyDataContext : DataContext
    {
        /// <summary>
        /// Initializes a new instance of the MyDataContext class.
        /// </summary>
        public MyDataContext(string connectionString)
            : base(connectionString)
        {
        }

        public Table<Person> People;
    }
}