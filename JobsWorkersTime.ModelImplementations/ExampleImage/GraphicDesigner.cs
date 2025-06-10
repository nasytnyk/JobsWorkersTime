using JobsWorkersTime.ModelAbstract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsWorkersTime.ModelImplementations.ExampleImage
{
    public class GraphicDesigner : Worker
    {
        // Наслідуючись від абстрактного класу, можна спростити конструктор, захардкодивши константи
        public GraphicDesigner(string name, TimeSpan timePerImage, ConsoleColor color) : base("Дизайнер", name, timePerImage, color)
        {
        }
    }
}
