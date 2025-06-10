using JobsWorkersTime.ModelAbstract.Entities;

namespace JobsWorkersTime.ModelImplementations.ExampleDelivery
{
    public class Courier: Worker
    {
        // Наслідуючись від абстрактного класу, можна спростити конструктор, захардкодивши константи
        public Courier(string name, TimeSpan timePerParsel, ConsoleColor color) : base("Кур'єр", name, timePerParsel, color)
        {
        }
    }
}
