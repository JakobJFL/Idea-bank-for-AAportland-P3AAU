﻿using DataBaseLib.DataAccess;
using DataBaseLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BusinessLogicLib
{
    static class DBConvert
    {
        public static List<Comment> TblToComment(List<CommentsTbl> dbComments)
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
            comments.Reverse();
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
                idea.Description = i.Description;
                idea.BusinessUnit = i.BusinessUnit.Name;
                idea.Department = i.Department.Name;
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
        public static IdeasTbl NewIdeaToTbl(NewIdea dbIdeas)
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
        public static string GetPriorityStr(int index)
        {
            switch (index)
            {
                case 1: return "Lav";
                case 2: return "Mellem";
                case 3: return "Høj";
                default: return "Ikke angivet";
            }
        }
        public static string GetStatusStr(int index)
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
