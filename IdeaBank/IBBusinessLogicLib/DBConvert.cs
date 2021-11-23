using DataBaseLib.Models;
using System;
using System.Collections.Generic;
using BusinessLogicLib.Models;


namespace BusinessLogicLib
{
    public static class DBConvert // internal???
    {
        /// <summary>
        /// Inserts break line tag in comment.message
        /// </summary>
        /// <param name="dbComments">List of comments</param>
        /// <returns>List of converted comments related to an idea</returns>
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
        /// <summary>
        /// Inserts an idea to the database.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>The new comment</returns>
        public static CommentsTbl CommentToTbl(Comment comment)
        {
            CommentsTbl newComment = new();
            newComment.Id = comment.Id;
            newComment.Initials = comment.Initials;
            newComment.Message = comment.Message;
            newComment.CreatedAt = comment.CreatedAt;
            return newComment;
        }

        /// <summary>
        /// Insert break line, bu&dep name, status and priority.
        /// </summary>
        /// <param name="dbIdeas">All ideas in database</param>
        /// <returns>List of ideas</returns>
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

                idea.AuthorBusinessUnit = i.AuthorBusinessUnitId;
                idea.AuthorBusinessUnitStr = i.AuthorBusinessUnit.Name;
                idea.AuthorDepartment = i.AuthorDepartmentId;
                idea.AuthorDepartmentStr = i.AuthorDepartment.Name;

                idea.IdeaBusinessUnit = i.IdeaBusinessUnitId;
                idea.IdeaBusinessUnitStr = i.IdeaBusinessUnit.Name;
                idea.IdeaDepartment = i.IdeaDepartmentId;
                idea.IdeaDepartmentStr = i.IdeaDepartment.Name;

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
        /// <summary>
        /// Converts the edited idea to IdeasTbl so it can be inserted in database.
        /// </summary>
        /// <param name="editIdea"></param>
        /// <returns>The edited idea of type IdeasTbl</returns>
        public static IdeasTbl EditIdeaToTbl(EditIdea editIdea)
        {
            IdeasTbl idea = new();
            idea.Id = editIdea.Id;
            idea.Initials = editIdea.Initials;
            idea.ProjectName = editIdea.ProjectName;
            idea.Description = editIdea.Description;
            idea.Description = editIdea.Description;
            idea.IdeaBusinessUnitId = editIdea.IdeaBusinessUnit;
            idea.IdeaDepartmentId = editIdea.IdeaDepartment;
            idea.AuthorBusinessUnitId = editIdea.AuthorBusinessUnit;
            idea.AuthorDepartmentId = editIdea.AuthorDepartment;
            idea.PlanDescription = editIdea.Plan;
            idea.Risk = editIdea.Risk;
            idea.CreatedAt = editIdea.CreatedAt;
            idea.Status = editIdea.Status;
            idea.Priority = editIdea.Priority;
            idea.IsHidden = editIdea.IsHidden;
            idea.Team = editIdea.Team;
            idea.ExpectedResults = editIdea.ExpectedResults;
            idea.UpdatedAt = DateTime.Now;
            return idea;
        }

        /// <summary>
        /// Converts the new idea to IdeasTbl so it can be inserted in database.
        /// </summary>
        /// <param name="idea"></param>
        /// <returns>The new idea of type IdeasTbl</returns>
        public static IdeasTbl NewIdeaToTbl(NewIdea idea)
        {
            IdeasTbl newIdea = new();
            newIdea.ProjectName = idea.ProjectName;
            newIdea.Initials = idea.Initials;
            newIdea.Description = idea.Description;
            newIdea.IdeaBusinessUnitId = idea.IdeaBusinessUnit;
            newIdea.IdeaDepartmentId = idea.IdeaDepartment;
            newIdea.AuthorBusinessUnitId = idea.AuthorBusinessUnit;
            newIdea.AuthorDepartmentId = idea.AuthorDepartment;
            newIdea.Team = idea.Team;
            newIdea.PlanDescription = idea.Plan;
            newIdea.ExpectedResults = idea.ExpectedResults;
            newIdea.IsHidden = idea.IsHidden;
            newIdea.Status = 1;
            newIdea.CreatedAt = DateTime.Now;
            newIdea.UpdatedAt = DateTime.Now;
            return newIdea;
        }
        public static GuideTextTbl SettingsToGuideTbl(Settings setting)
        {
            GuideTextTbl newGuideText = new();
            newGuideText.HomepageGuide = setting.HomepageGuide;
            newGuideText.Purpose = setting.Purpose;
            newGuideText.SubmitGuide = setting.SubmitGuide;
            return newGuideText;
        }

        public static Settings GuideTblToSettings(GuideTextTbl guideText)
        {
            Settings newSettings = new();
            newSettings.HomepageGuide = StrNewLineToBr(guideText.HomepageGuide);
            newSettings.Purpose = StrNewLineToBr(guideText.Purpose);
            newSettings.SubmitGuide = StrNewLineToBr(guideText.SubmitGuide);
            return newSettings;
        }

        /// <summary>
        /// Converts priority int to string.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Name of a priority.</returns>
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

        /// <summary>
        /// Converts status int to string.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Name of status.</returns>
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

        public static string StrNewLineToBr(string str)
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

