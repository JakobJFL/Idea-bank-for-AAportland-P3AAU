using DataBaseLib.Models;
using System;
using System.Collections.Generic;
using BusinessLogicLib.Models;


namespace BusinessLogicLib
{
    public static class DBConvert // internal???
    {
        public static List<Comment> TblToComment(List<CommentsTbl> dbComments)
        {
            List<Comment> comments = new();
            foreach (CommentsTbl i in dbComments)
            {
                Comment comment = new();
                comment.Id = i.Id;
                comment.Initials = i.Initials;
                comment.Message = StrNewLineToBr(i.Message);
                comment.CreatedAt = i.CreatedAt;
                comments.Add(comment);
            }
            return comments;
        }
        public static CommentsTbl CommentToTbl(Comment comment)
        {
            CommentsTbl newComment = new();
            newComment.Id = comment.Id;
            newComment.Initials = comment.Initials;
            newComment.Message = comment.Message;
            newComment.CreatedAt = comment.CreatedAt;
            newComment.IdeaId = comment.IdeaId;
            return newComment;
        }

        public static List<ViewIdea> TblToViewIdea(List<IdeasTbl> dbIdeas)
        {
            List<ViewIdea> ideas = new();
            foreach (IdeasTbl i in dbIdeas)
            {
                ViewIdea idea = new();
                idea.Id = i.Id;
                idea.Initials = i.Initials;
                idea.ProjectName = i.ProjectName;
                idea.Description = StrNewLineToBr(i.Description);
                idea.BusinessUnit = i.BusinessUnit.Id;
                idea.BusinessUnitStr = i.BusinessUnit.Name;
                idea.Department = i.Department.Id;
                idea.DepartmentStr = i.Department.Name;
                idea.Priority = i.Priority;
                idea.PriorityStr = GetPriorityStr(i.Priority);
                idea.Status = i.Status;
                idea.StatusStr = GetStatusStr(i.Status);
                idea.Plan = StrNewLineToBr(i.PlanDescription);
                idea.Risk = StrNewLineToBr(i.Risk);
                idea.Team = i.Team;
                idea.IsHidden = i.IsHidden;
                idea.ExpectedResults = StrNewLineToBr(i.ExpectedResults);
                idea.CreatedAt = i.CreatedAt;
                idea.UpdatedAt = i.UpdatedAt;
                ideas.Add(idea);
            }
            return ideas;
        }
        public static IdeasTbl EditIdeaToTbl(EditIdea editIdea)
        {
            IdeasTbl idea = new();
            idea.Id = editIdea.Id;
            idea.Initials = editIdea.Initials;
            idea.ProjectName = editIdea.ProjectName;
            idea.Description = editIdea.Description;
            idea.PlanDescription = editIdea.Plan;
            idea.Risk = editIdea.Risk;
            idea.CreatedAt = editIdea.CreatedAt;
            idea.Status = editIdea.Status;
            idea.Priority = editIdea.Priority;
            idea.Team = editIdea.Team;
            idea.ExpectedResults = editIdea.ExpectedResults;
            return idea;
        }
        public static IdeasTbl NewIdeaToTbl(NewIdea idea)
        {
            IdeasTbl newIdea = new();
            newIdea.ProjectName = idea.ProjectName;
            newIdea.Initials = idea.Initials;
            newIdea.Description = idea.Description;
            newIdea.Team = idea.Team;
            newIdea.PlanDescription = idea.Plan;
            newIdea.ExpectedResults = idea.ExpectedResults;
            newIdea.IsHidden = idea.IsHidden;
            newIdea.Status = 1;
            newIdea.CreatedAt = DateTime.Now;
            newIdea.UpdatedAt = DateTime.Now;
            return newIdea;
        }
        private static string GetPriorityStr(int index)
        {
            switch (index)
            {
                case 1: return "Lav";
                case 2: return "Mellem";
                case 3: return "Høj";
                default: return "Ikke angivet";
            }
        }
        private static string GetStatusStr(int index)
        {
            switch (index)
            {
                case 1: return "Oprettet";
                case 2: return "Godkendt";
                case 3: return "Arkiveret";
                case 4: return "Afsluttet";
                default: throw new ArgumentException("index for getStatusStr was not within range");
            }
        }

        private static string StrNewLineToBr(string str)
        {
            if (str != null)
                return str.Replace("\r\n", "<br />").Replace("\n", "<br />");
            return null;
        }
        public static string StrBrToNewLine(string str)
        {
            if (str != null)
                return str.Replace("<br />", "\n");
            return null;
        }
    }
}

