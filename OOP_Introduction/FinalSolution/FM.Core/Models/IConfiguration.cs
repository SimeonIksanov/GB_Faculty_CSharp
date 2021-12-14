using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Core.Models
{
    public interface IConfiguration
    {
        public string CurrentDirectory { get; set; }
        public int PageSize { get; set; }
    }
}
