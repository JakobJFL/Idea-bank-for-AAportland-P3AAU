using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BusinessLogicLib
{
    public class InsertToDB
    {
        public void InsertIdea(string connectionString, NewIdea idea)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using Context db = new(optionsBuilder.Options);
            DBConvert convert = new DBConvert();
            db.IdeasTbl.Add(convert.NewIdeaToTbl(idea));
            db.SaveChanges();
        }

    }

}

