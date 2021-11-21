using GameLib.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Weapon
{
    public class Scimitar : IOrcWeapon
    {
        private uint _damage = 15;
        public void Hit(BaseCharacter enemy)
        {
            enemy.GetDamaged(_damage);
        }
    }
}
