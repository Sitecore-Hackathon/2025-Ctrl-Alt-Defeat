using Sitecore.Data.Items;
using Sitecore.Publishing.Pipelines.Publish;
using ReadLogsAndSolve.Helpers;
using Sitecore.Publishing.Pipelines.PublishItem;
using System.Threading.Tasks;
using System.Threading;

namespace ReadLogsAndSolve.Pipelines
{
    public class CustomPublisher : PublishItemProcessor
    {
        public override void Process(PublishItemContext context)
        {
            if (context == null)
                return;


            Task.Run(() => ResponseToFile.AnalyzeLogErrorsAndSaveResponseAsync()).Wait();

        }
    }
}