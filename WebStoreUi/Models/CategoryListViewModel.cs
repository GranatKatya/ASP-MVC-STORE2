using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStoreDomain.Entities;

namespace WebStoreUi.Models
{
    public class CategoryListViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}