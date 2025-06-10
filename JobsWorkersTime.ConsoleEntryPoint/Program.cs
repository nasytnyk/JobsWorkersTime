using JobsWorkersTime.ModelImplementations.ExampleImage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using JobsWorkersTime.ModelAbstract;
using JobsWorkersTime.ModelAbstract.Entities;
using JobsWorkersTime.ModelImplementations.ExampleDelivery;
using JobsWorkersTime.ModelImplementations.ExampleFarm;


using JobsWorkersTime.LogicAbstract;

public class Program
{
    private static void ExampleImage(out IEnumerable<Job> jobs, out IEnumerable<Worker> workers)
    {
        jobs = Enumerable.Range(1, 1000).Select(i => new AdvertisingImage($"image{i}.jpg")).ToList();

        workers = new List<GraphicDesigner>
        {
            new("Влад", TimeSpan.FromMinutes(2), ConsoleColor.Gray),
            new("Ілля", TimeSpan.FromMinutes(3), ConsoleColor.White),
            new("Катя", TimeSpan.FromMinutes(4), ConsoleColor.DarkGray),
        };
    }

    private static void ExampleDelivery(out IEnumerable<Job> jobs, out IEnumerable<Worker> workers)
    {
        jobs = Enumerable.Range(1, 1234).Select(i => new Parsel($"посилка #{i}")).ToList();

        workers = new List<Courier>
        {
            new("Василь", TimeSpan.FromMinutes(5), ConsoleColor.Red),
            new("Петро", TimeSpan.FromMinutes(6), ConsoleColor.White),
            new("Іван", TimeSpan.FromMinutes(7), ConsoleColor.Magenta),
            new("Степан", TimeSpan.FromMinutes(8), ConsoleColor.Yellow),
            new("Тарас", TimeSpan.FromMinutes(9), ConsoleColor.Cyan),
            new("Валерій", TimeSpan.FromMinutes(10), ConsoleColor.Green),
        };
    }

    private static void ExampleFarm(out IEnumerable<Job> jobs, out IEnumerable<Worker> workers)
    {
        jobs = Enumerable.Range(1, 567).Select(i => new Potato($"картоплина #{i}")).ToList();

        workers = new List<Harvester>
        {
            new("Grimme", TimeSpan.FromMinutes(1.5), ConsoleColor.Gray),
            new("Ropa", TimeSpan.FromMinutes(2), ConsoleColor.White),
            new("Dewulf", TimeSpan.FromMinutes(2.5), ConsoleColor.Magenta),
            new("AVR", TimeSpan.FromMinutes(3), ConsoleColor.DarkGray),
            new("Standen", TimeSpan.FromMinutes(3.5), ConsoleColor.DarkYellow),
            new("Simon", TimeSpan.FromMinutes(4), ConsoleColor.DarkMagenta),
        };
    }

    public static async Task Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        IEnumerable<Job> jobs;
        IEnumerable<Worker> workers;

        //ExampleImage(out jobs, out workers);
        //ExampleDelivery(out jobs, out workers);
        ExampleFarm(out jobs, out workers);

        var stopwatch = Stopwatch.StartNew();

        await Execution.RunAsync(jobs, workers, (worker, job, progress) =>
        {
            Console.ForegroundColor = worker.Color;
            Console.WriteLine($"<{worker.Specialty}> з іменем [{worker.Name}] завершив <{job.Category}> з назвою [{job.Title}]. Прогрес: {progress:P2}");
        });

        stopwatch.Stop();

        Report(stopwatch, workers);
    }

    private static void Report(Stopwatch stopwatch, IEnumerable<Worker> workers)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine($"Швидкість симуляції: x{Config.SimulationSpeed}");
        Console.WriteLine($"Реальний час виконання: {stopwatch.Elapsed.TotalSeconds:N2} секунд\n");
        foreach (var worker in workers)
        {
            double simulatedMinutes = worker.TimePerJob.TotalMinutes * worker.CompletedJobs.Count;
            Console.ForegroundColor = worker.Color;
            Console.WriteLine($"{worker.Name} завершив {worker.CompletedJobs.Count} завдань");
            Console.WriteLine($"- Симульований час: {worker.TimePerJob.TotalMinutes} x {worker.CompletedJobs.Count} = {simulatedMinutes:N2} хвилин");
        }
        Console.WriteLine("-----------------------------------------------");
    }
}
