using FM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Core.Views
{
    public interface IView
    {
        void Display(IViewData viewData);
    }
}
