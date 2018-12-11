using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.ViewModel
{
    public class NewsSearchModel
    {
        public string search;
        public int index, pageSize;
        public int? fkChannel, PKUser;
        public bool isSubscribe;
    }
}
