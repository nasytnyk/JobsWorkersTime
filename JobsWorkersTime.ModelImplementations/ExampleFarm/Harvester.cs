using JobsWorkersTime.ModelAbstract.Entities;

namespace JobsWorkersTime.ModelImplementations.ExampleFarm
{
    public class Harvester: Worker
    {
        // Наслідуючись від абстрактного класу, можна спростити конструктор, захардкодивши константи
        public Harvester(string name, TimeSpan timePerImage, ConsoleColor color) : base("Комбайн", name, timePerImage, color)
        {
        }
    }
}
