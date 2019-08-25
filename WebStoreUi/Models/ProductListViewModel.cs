using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreDomain.Entities;

namespace WebStoreUi.Models
{
    public class ProductListViewModel
    {   
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public SelectList ProductNames { get; set; }
    }
}