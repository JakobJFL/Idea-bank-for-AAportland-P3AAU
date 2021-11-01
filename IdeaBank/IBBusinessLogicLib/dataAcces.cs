using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLib
{
    public class DataAcces
    {
        public async Task<List<Idea>> LoadAllIdeas(string connectionString)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using Context db = new Context(optionsBuilder.Options);
            List<IdeasTbl> dbIdeas = await db.IdeasTbl.ToListAsync();
            return ConvertTblToIdea(dbIdeas);
        }

        public List<Idea> ConvertTblToIdea(List<IdeasTbl> dbIdeas)
        {
            List<Idea> ideas = new();
            foreach(IdeasTbl i in dbIdeas)
            {
                Idea idea = new();
                idea.Id = i.Id;
                idea.Initials = i.Initials;
                idea.ProjectName = i.ProjectName;
                idea.Description = i.Description;
                idea.BusinessUnit = i.BusinessUnit.Name;
                idea.Department = Convert.ToString(i.BusinessUnitRefId);
                idea.Priority = getPriorityStr(i.Priority);
                idea.Status = getStatusStr(i.Status);
                idea.Plan = i.PlanDescription;
                idea.Risk = i.Risk;
                idea.Team = i.Team;
                idea.ExpectedResults = i.ExpectedResults;
                idea.CreatedAt = i.CreatedAt;
                idea.UpdatedAt = i.UpdatedAt;
                ideas.Add(idea);
            }
            return ideas;
        }
        public string getPriorityStr(int index)
        {
            switch(index)
            {
                case 1: return "Lav";
                case 2: return "Mellem";
                case 3: return "Høj";
                default: return "Ikke angivet";
            }
        }
        public string getStatusStr(int index)
        {
            switch (index)
            {
                case 1: return "Oprettet";
                case 2: return "Godkendt";
                case 3: return "Afvist";
                case 4: return "Afsluttet";
                default: throw new ArgumentException("index for getStatusStr was not within range");
            }
        }
        /*
        public async Task<DataBaseLib.Models.Idea> LoadIdeaByID(string connectionString, string id)
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString);
            using var db = new Context(optionsBuilder.Options);
            string sql = "SELECT * FROM Ideas WHERE Id='" + id + "'";
            return await db.Ideas.FromSqlRaw(sql).SingleAsync();
        }
        */
    }
}

