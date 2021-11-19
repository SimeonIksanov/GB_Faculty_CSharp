using GameLib.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Characters
{
    public class Human : Warrior
    {
        public Human(IMyObserver observer) : base(100, 8, observer)
        {
        }

        public override void GetDamaged(uint damage)
        {
            base.GetDamaged(damage);
            if (IsAlive)
            {
                _observer.Notify(this, "ouch..");
            }
        }
    }
}
