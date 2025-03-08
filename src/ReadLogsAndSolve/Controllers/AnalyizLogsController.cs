using ReadLogsAndSolve.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ReadLogsAndSolve.Controllers
{
    public class AnalyizLogsController : Controller
    {
        
        public async Task<String> testApi()
        {
            await ResponseToFile.AnalyzeLogErrorsAndSaveResponseAsync();
            return "AI analysis completed successfully.";
        }
    }
}
