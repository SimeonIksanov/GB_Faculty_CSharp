using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib.Characters;

namespace GameLib.Weapon
{
    public interface IOrcWeapon
    {
        public void Hit(BaseCharacter enemy);
    }
}
