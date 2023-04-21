using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Helper
{
  public  class CacheHelper
    {
        IMemoryCache Cache;

        public CacheHelper(IMemoryCache Cache)
        {
            this.Cache = Cache;
        }

    }
}
