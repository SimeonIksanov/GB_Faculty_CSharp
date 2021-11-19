using GameLib.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Observer
{
    public class Observer : IMyObserver
    {
        public void Notify(BaseCharacter character, string message)
        {
            Console.WriteLine($"{character.GetType().Name}: {message}");
        }
    }
}
