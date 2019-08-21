using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebStoreUi.Models;

namespace WebStoreUi.HtmlHelpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks (this HtmlHelper html, PagingInfo info, Func<int, string> pageUri )// we create one obj to don't sent all parametrs    //we send all parametrs in oge obj
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= info.TotalPages ; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUri(i));
                tag.InnerHtml = i.ToString();

                if (i==info.CurrentPage)
                {
                    tag.AddCssClass("select btn btn-success  btn-lg");
                }
                else
                {
                    tag.AddCssClass("select btn btn-default");
                }
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}




//instead of it



//<div > 
//    @{
//        int totalPage = (int)Math.Ceiling((double)ViewBag.Count / ViewBag.PageSize);

//    }
//    @for(int i = 0; i<totalPage; i++)
//    {

//        string str = (i + 1 == ViewBag.CurrentPage) ? "select btn btn-success  btn-lg " : "btn btn-default";

//        @*if (totalPage == ViewBag.CurrentPage )
//        {
//            @Html.ActionLink($"{i + 1}", "List", "Product", new { page = i + 1 }, new { @class = "btn btn-success" });
//        }
//        else
//        {*@
//            @Html.ActionLink($"{i + 1}", "List", "Product", new { page = i + 1 }, new { @class = str });
//      //  }

//    }


//</div>