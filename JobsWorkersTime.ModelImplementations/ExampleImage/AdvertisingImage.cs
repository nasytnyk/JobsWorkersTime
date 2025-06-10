using JobsWorkersTime.ModelAbstract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JobsWorkersTime.ModelImplementations.ExampleImage
{
    public class AdvertisingImage : Job
    {
        // Наслідуючись від абстрактного класу, можна спростити конструктор, захардкодивши константи
        public AdvertisingImage(string title) : base("зображення", title)
        {
        }
    }
}
