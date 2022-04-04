using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Dtos
{
    public class ProductTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanBeInsured { get; set; }


    }
}
