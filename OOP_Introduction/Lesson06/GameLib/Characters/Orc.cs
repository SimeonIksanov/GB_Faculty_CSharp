using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib.Observer;
using GameLib.Weapon;

namespace GameLib.Characters
{
    public class Orc : Warrior
    {
        private IOrcWeapon? _weapon;
        public Orc(IMyObserver observer) : base(120, 10, observer)
        {
        }

        public void TakeWeapon(IOrcWeapon weapon)
        {
            _weapon = weapon;
        }
        public override void Hit(BaseCharacter enemy)
        {

            if (_weapon != null && IsAlive)
            {
                _weapon.Hit(enemy);
            }
            else
            {
                base.Hit(enemy);
            }
        }

        public override void GetDamaged(uint damage)
        {
            base.GetDamaged(damage);
            if (IsAlive)
            {
                _observer.Notify(this, "arrr..");
            }
        }
    }
}
