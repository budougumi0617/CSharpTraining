using System.Threading;
using System.Threading.Tasks;

namespace SandBox
{
    class TaskSample
    {
        CancellationTokenSource source { get; }

        public TaskSample(CancellationTokenSource source)
        {
            this.source = source;
        }

        /// <summary>
        /// Echo count.
        /// </summary>
        /// <remarks>
        /// Task never stop until cancelToken is called.
        /// </remarks>
        public async Task CountAsync()
        {
            // Get cancel token.
            var ct = source.Token;

            // Execute other thread.
            await Task.Run(() =>
            {

                // Were we already canceled?
                ct.ThrowIfCancellationRequested();
                int count = 0;
                var looper = new System.Timers.Timer();
                looper.AutoReset = true;
                looper.Interval = 1000; // Span in milliseconds 
                looper.Elapsed += (o, e) =>
                {
                    count++;
                    // Poll on this property if you have to do
                    // other cleanup before throwing.
                    if (ct.IsCancellationRequested)
                    {
                        // Clean up here, then...
                        ct.ThrowIfCancellationRequested();
                    }
                    System.Console.WriteLine(count);
                };
                looper.Start();
            }, source.Token); // Pass same token to StartNew.
        }
    }
}
