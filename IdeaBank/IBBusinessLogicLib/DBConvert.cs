using DataBaseLib.Models;
using System;
using System.Collections.Generic;
using BusinessLogicLib.Models;
using BusinessLogicLib.Service;

namespace BusinessLogicLib
{
    public static class DBConvert
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
        public static List<ViewIdea> TblListToViewIdea(List<IdeasTbl> dbIdeas)
        {
            List<ViewIdea> ideas = new();
            foreach (IdeasTbl dbIdea in dbIdeas)
            {
                ideas.Add(TblToViewIdea(dbIdea));
            }
            return ideas;
        }
        public static ViewIdea TblToViewIdea(IdeasTbl dbIdea)
        {
            ViewIdea idea = new();
            idea.Id = dbIdea.Id;
            idea.Initials = dbIdea.Initials;
            idea.ProjectName = dbIdea.ProjectName;
            idea.Description = StrNewLineToBr(dbIdea.Description);

            idea.AuthorBusinessUnit = dbIdea.AuthorBusinessUnitId;
            idea.AuthorBusinessUnitStr = dbIdea.AuthorBusinessUnit.Name;
            idea.AuthorDepartment = dbIdea.AuthorDepartmentId;
            idea.AuthorDepartmentStr = dbIdea.AuthorDepartment.Name;

            idea.IdeaBusinessUnit = dbIdea.IdeaBusinessUnitId;
            idea.IdeaBusinessUnitStr = dbIdea.IdeaBusinessUnit.Name;
            idea.IdeaDepartment = dbIdea.IdeaDepartmentId;
            idea.IdeaDepartmentStr = dbIdea.IdeaDepartment.Name;

            idea.Priority = dbIdea.Priority;
            idea.PriorityStr = Config.PriorityStrs[dbIdea.Priority - 1];
            idea.Status = dbIdea.Status;
            idea.StatusStr = Config.StatusStrs[dbIdea.Status - 1];
            idea.Plan = StrNewLineToBr(dbIdea.PlanDescription);
            idea.Risk = StrNewLineToBr(dbIdea.Risk);
            idea.Team = dbIdea.Team;
            idea.IsHidden = dbIdea.IsHidden;
            idea.ExpectedResults = StrNewLineToBr(dbIdea.ExpectedResults);
            idea.CreatedAt = dbIdea.CreatedAt;
            idea.UpdatedAt = dbIdea.UpdatedAt;
            return idea;
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
        public static IdeasTbl IdeaToTbl(Idea idea)
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
            newIdea.Priority = 1;
            newIdea.CreatedAt = DateTime.Now;
            newIdea.UpdatedAt = DateTime.Now;
            return newIdea;
        }
        /// <summary>
        /// Converts the newGuideText to GuideTextTbl so it can be inserted in database
        /// </summary>
        /// <param name="setting"></param>
        /// <returns>The new guide text of type GuideTextTbl</returns>
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

