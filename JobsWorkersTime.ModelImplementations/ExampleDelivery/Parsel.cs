using JobsWorkersTime.ModelAbstract.Entities;

namespace JobsWorkersTime.ModelImplementations.ExampleDelivery
{
    public class Parsel : Job
    {
        // Наслідуючись від абстрактного класу, можна спростити конструктор, захардкодивши константи
        public Parsel(string title) : base("посилка", title)
        {
        }
    }
}
