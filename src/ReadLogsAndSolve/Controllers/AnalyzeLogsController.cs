using ReadLogsAndSolve.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ReadLogsAndSolve.Controllers
{
    public class AnalyzeLogsController : Controller
    {
        
        public async Task<String> SolveError()
        {
            await ResponseToFile.AnalyzeLogErrorsAndSaveResponseAsync();
            return "AI analysis completed successfully.";
        }
    }
}
