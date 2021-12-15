using System.IO;
using System.Threading.Tasks;
using BusinessLogicLib.Interfaces;

namespace BusinessLogicLib.Service
{
    public interface ICsvService
    {
        IIdeasDataAccess IdeasDataAccess { get; }

        Task<MemoryStream> CreateCsvFileAsync();
    }
}