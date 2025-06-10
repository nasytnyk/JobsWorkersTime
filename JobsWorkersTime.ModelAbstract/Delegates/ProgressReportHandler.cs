using JobsWorkersTime.ModelAbstract.Entities;

namespace JobsWorkersTime.ModelAbstract.Delegates
{
    // Цей делегат викликається, коли працівник виконує 1 завдання
    // Він передає:
    // - Самого працівника
    // - Щойно виконане завдання
    // - Загальний прогрес працівника
    public delegate void ProgressReportHandler(Worker worker, Job job, double progress);
}
