namespace JobsWorkersTime.ModelAbstract.Entities
{
    public abstract class Job // Робота як база ієрархії є примітивом
    {
        public string Category { get; }
        public string Title { get; }
        public Job(string category, string title)
        {
            Category = category;
            Title = title;
        }
    }
}
