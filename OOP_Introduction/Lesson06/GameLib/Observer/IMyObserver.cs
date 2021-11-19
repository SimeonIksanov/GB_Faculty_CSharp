using GameLib.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Observer
{
    public interface IMyObserver
    {
        public void Notify(BaseCharacter character, string message);
    }
}
