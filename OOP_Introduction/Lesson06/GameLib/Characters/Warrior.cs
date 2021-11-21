using GameLib.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Characters
{
    public abstract class Warrior : BaseCharacter
    {
        protected uint _armor = 5;

        protected Warrior(uint health, uint damage, IMyObserver observer) : base(health, damage, observer)
        {
        }

        public override void GetDamaged(uint damage)
        {
            damage = damage > _armor ? damage - _armor : 1;
            base.GetDamaged(damage);
        }

    }
}
