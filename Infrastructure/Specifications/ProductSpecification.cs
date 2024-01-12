using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    public class ProductSpecification
    {

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public string? Sort { get; set; }
        private const int MAXPAGESIZE = 50;
        public int PageIndex { get; set; } = 1;

        private int _PageSize = 6;

        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE : value;
        }
        private string _search;

        public string Search
        {
            get => _search;
            set => _search = value.Trim().ToLower();
        }



    }
}
