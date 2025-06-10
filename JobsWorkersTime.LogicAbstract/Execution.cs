using JobsWorkersTime.ModelAbstract.Delegates;
using JobsWorkersTime.ModelAbstract.Entities;
using System.Threading.Channels;

namespace JobsWorkersTime.LogicAbstract
{
    public static class Execution
    {
        public static async Task RunAsync(IEnumerable<Job> jobs, IEnumerable<Worker> workers, ProgressReportHandler? reportProgress = null) 
        {
            var jobChannel = Channel.CreateUnbounded<Job>(); // Розподілена потокобезпечна черга, у яку додаватимуться завдання (пул завдань)

            var cts = new CancellationTokenSource(); // ВАЖЛИВО: джерело токену скасування є спільним для всіх працівників

            // Тут починається логіка, по якій у методі DoWorkAsync працівники періодично забиратимуть собі завдання з черги
            var processingTasks = 
                workers.Select( 
                    _ => _.DoWorkAsync(
                        jobChannel.Reader, // Читач черги, звідки працівник отримуватиме завдання
                        cts.Token,
                        reportProgress
                        )
                    ).ToList();

            foreach (var job in jobs)
            {
                await jobChannel.Writer.WriteAsync(job); // Записуємо завдання у чергу
            }
            jobChannel.Writer.Complete(); // Закриваємо чергу

            await Task.WhenAll(processingTasks); // Очікуємо, поки всі працівники завершать всі завдання
        }
    }
}
