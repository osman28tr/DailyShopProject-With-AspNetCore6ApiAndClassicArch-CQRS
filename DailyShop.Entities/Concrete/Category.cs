﻿using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class Category:Entity
    {
        public string? Name { get; set; }
        public Category()
        {
            
        }
        public Category(int id):base(id)
        {
            Id = id;
        }
        public ICollection<Product>? Products { get; set; }
    }
}
