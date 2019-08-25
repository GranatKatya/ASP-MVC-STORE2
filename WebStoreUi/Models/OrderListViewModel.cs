using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStoreDomain.Entities;

namespace WebStoreUi.Models
{
    public class OrderListViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentState { get; set; }
    }
}