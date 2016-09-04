using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCUI.ViewModels.File;

namespace MVCUI.ViewModels
{
    public class PageInfo
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов

        public int TotalPages // всего страниц
            => (int) Math.Ceiling((decimal) TotalItems/PageSize);
    }

    public class IndexViewModel
    {
        public IEnumerable<FileViewModel> Files { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}