using System;

namespace BusinessLogicLib
{
    public class DbNoConnectionException : Exception
    {
        public DbNoConnectionException() : base("Could not connect to database. Maybe it does not exists.") { }
    }
}

