using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce_blackcat_api.Src.Dtos
{
    public class PageResultDto<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; } = new();
    }
}