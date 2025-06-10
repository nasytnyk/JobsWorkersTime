using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobsWorkersTime.ModelImplementations.ExampleImage;
using JobsWorkersTime.ModelAbstract.Entities;
using JobsWorkersTime.LogicAbstract;
using Xunit;

public class LogicAbstractTests
{
    [Fact]
    public async Task ExampleImage_AverageSimulatedMinutes_IsWithinExpectedRange()
    {
        var jobs = Enumerable.Range(1, 1000).Select(i => new AdvertisingImage($"image{i}.jpg")).ToList();
        var workers = new List<GraphicDesigner>
        {
            new("Влад", TimeSpan.FromMinutes(2), ConsoleColor.Gray),
            new("Ілля", TimeSpan.FromMinutes(3), ConsoleColor.White),
            new("Катя", TimeSpan.FromMinutes(4), ConsoleColor.DarkGray),
        };

        await Execution.RunAsync(jobs, workers);

        double averageSimulatedMinutes = workers.Select(_ => _.TimePerJob.TotalMinutes * _.CompletedJobs.Count).Average();

        Assert.InRange(averageSimulatedMinutes, 900, 950); 
    }
}