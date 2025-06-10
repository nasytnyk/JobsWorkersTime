using JobsWorkersTime.ModelAbstract.Delegates;
using System.Threading.Channels;

namespace JobsWorkersTime.ModelAbstract.Entities
{
    public abstract class Worker // Абстрактний клас для працівника
    {
        public string Specialty { get; }    // Спеціальність
        public string Name { get; }         // Ім'я працівника
        public ConsoleColor Color { get; }  // Колір для виведення в консоль 
        public TimeSpan TimePerJob { get; }     // Фактичний час, який працівник витрачає на одне завдання
        private TimeSpan _simulatedTimePerJob;  // Прискорений час, кратно менший фактичного (для симуляції)
        public List<Job> CompletedJobs { get; } = new(); // Сюди додаються завдання, які він виконав

        public Worker(string specialty, string name, TimeSpan timePerJob, ConsoleColor color)
        {
            Specialty = specialty;
            Name = name;
            Color = color;
            TimePerJob = timePerJob;
            _simulatedTimePerJob = TimeSpan.FromMilliseconds(TimePerJob.TotalMilliseconds / Config.SimulationSpeed); // Швидкість з Config

        }

        public async Task DoWorkAsync( // Варто зазначити, що цей метод виконується у всіх працівників одночасно
            ChannelReader<Job> jobChannelReader, 
            CancellationToken token,
            ProgressReportHandler? reportProgress = null)
        {
            // "await foreach" (C# 8.0) працює з асинхронним ітератором, запобігаючи блокуванню потоку
            // Він діє доти, доки не викличеться метод Complete() на ChannelWriter і всі завдання не закінчаться
            await foreach (var job in jobChannelReader.ReadAllAsync(token))
            {
                await Task.Delay(_simulatedTimePerJob, token); // Ось тут власне витрачається час на виконання завдання

                CompletedJobs.Add(job); // Ми зберігаємо його у окремий список виконаних завдань

                // ВАЖЛИВО: це прогрес окремого працівника, який близький до загального - але не рівний йому
                double progress = (double) CompletedJobs.Count / (CompletedJobs.Count + jobChannelReader.Count);
                reportProgress?.Invoke(this, job, progress);
            }
        }
    }
}
