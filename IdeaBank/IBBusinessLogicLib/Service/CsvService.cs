using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLib.Interfaces;
using BusinessLogicLib.Models;
using DataBaseLib;

namespace BusinessLogicLib.Service
{
    public class CsvService : ICsvService
    {
        private readonly int _maxIdeasInCSVFile = 5000;
        private readonly int _maxCharsPerIdea = 4000;
        public CsvService(IIdeasDataAccess ideasDataAccess)
        {
            IdeasDataAccess = ideasDataAccess;
        }
        public IIdeasDataAccess IdeasDataAccess { get; }

        public async Task<MemoryStream> CreateCsvFileAsync()
        {
            FilterSortIdea filterIdea = new();
            filterIdea.CurrentPage = 1;
            filterIdea.IdeasShownCount = _maxIdeasInCSVFile;

            List<ViewIdea> ideaList = await IdeasDataAccess.GetWFilter(filterIdea);

            StringBuilder sb = new(ideaList.Count * _maxCharsPerIdea);

            sb.Append("Projekt navn;" +
                        "Initialer;" +
                        "Forfatterens forretningsenhed;" +
                        "Forfatterens afdeling;" +
                        "Vedrørende forretningsenhed;" +
                        "Vedrørende afdeling;" +
                        "Prioritet;Status;" +
                        "Idébeskrivelse;" +
                        "Plan;" +
                        "Forventet resultat;" +
                        "Risiko;" +
                        "Team;" +
                        "Er skjult;" +
                        "Oprettet;" +
                        "Opdateret\n");

            foreach (ViewIdea idea in ideaList)
            {
                sb.Append(idea.ProjectName).Append(';').
                Append(idea.Initials).Append(';').
                Append(idea.AuthorBusinessUnitStr).Append(';').
                Append(idea.AuthorDepartmentStr).Append(';').
                Append(idea.IdeaBusinessUnitStr).Append(';').
                Append(idea.IdeaDepartmentStr).Append(';').
                Append(idea.PriorityStr).Append(';').
                Append(idea.StatusStr).Append(';').
                Append(idea.Description).Append(';').
                Append(idea.Plan).Append(';').
                Append(idea.ExpectedResults).Append(';').
                Append(idea.Risk).Append(';').
                Append(idea.Team).Append(';').
                Append(idea.IsHidden).Append(';').
                Append(idea.CreatedAt).Append(';').
                Append(idea.UpdatedAt).Append('\n');
            }

            using MemoryStream stream = new MemoryStream();
            UnicodeEncoding uniEncoding = new UnicodeEncoding();
            await stream.WriteAsync(uniEncoding.GetBytes(sb.ToString()));
            return stream;
        }
    }
}
