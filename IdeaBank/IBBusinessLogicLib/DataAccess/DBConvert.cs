using DataBaseLib.Models;
using System;
using System.Collections.Generic;

namespace BusinessLogicLib
{
    class DBConvert
    {
        public List<Comment> TblToComment(List<CommentsTbl> dbComments)
        {
            List<Comment> comments = new();
            foreach (CommentsTbl i in dbComments)
            {
                Comment Comment = new();
                Comment.Id = i.Id;
                Comment.Initials = i.Initials;
                Comment.Message = i.Message;
                Comment.CreatedAt = i.CreatedAt;
                comments.Add(Comment);
            }
            return comments;
        }

        public List<ViewIdea> TblToViewIdea(List<IdeasTbl> dbIdeas)
        {
            List<ViewIdea> ideas = new();
            foreach (IdeasTbl i in dbIdeas)
            {
                ViewIdea idea = new();
                idea.Id = i.Id;
                idea.Initials = i.Initials;
                idea.ProjectName = i.ProjectName;
                idea.Description = i.Description;
                idea.BusinessUnit = (i.BusinessUnit != null) ? i.BusinessUnit.Name : "Ikke angivet";
                idea.Department = (i.Department != null) ? i.Department.Name : "Ikke angivet";
                idea.Priority = GetPriorityStr(i.Priority);
                idea.Status = GetStatusStr(i.Status);
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
        public IdeasTbl NewIdeaToTbl(NewIdea dbIdeas)
        {
            IdeasTbl newIdea = new();
            newIdea.ProjectName = dbIdeas.ProjectName;
            newIdea.Initials = dbIdeas.Initials;
            newIdea.Description = dbIdeas.Description;
            //newIdea.BusinessUnit.Id = dbIdeas.BusinessUnit;
           // newIdea.Department.Id = dbIdeas.Description;
            newIdea.Team = dbIdeas.Team;
            newIdea.PlanDescription = dbIdeas.Plan;
            newIdea.ExpectedResults = dbIdeas.ExpectedResults;
            newIdea.IsHidden = dbIdeas.IsHidden;
            newIdea.Status = 1;
            newIdea.CreatedAt = DateTime.Now;
            newIdea.UpdatedAt = DateTime.Now;
            return newIdea;
        }
        public string GetPriorityStr(int index)
        {
            switch (index)
            {
                case 1: return "Lav";
                case 2: return "Mellem";
                case 3: return "Høj";
                default: return "Ikke angivet";
            }
        }
        public string GetStatusStr(int index)
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
    }
}

