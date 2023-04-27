using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningORM.Models;

namespace LearningORM
{
    internal class ProductViewDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int Price { get; set; }
    }
}
