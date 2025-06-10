using JobsWorkersTime.ModelAbstract.Entities;

namespace JobsWorkersTime.ModelImplementations.ExampleFarm
{
    public class Potato: Job
    {
        // Наслідуючись від абстрактного класу, можна спростити конструктор, захардкодивши константи
        public Potato(string title) : base("картоплина", title)
        {
        }
    }
}
