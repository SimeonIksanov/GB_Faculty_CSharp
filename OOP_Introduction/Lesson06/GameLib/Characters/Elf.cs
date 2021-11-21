using GameLib.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Characters
{
    public class Elf : BaseCharacter
    {
        private uint _healPoints = 5;
        public Elf(IMyObserver observer) : base(200, 9, observer)
        {
        }

        public void Heal(BaseCharacter friend)
        {
            friend.GetHealed(_healPoints);
        }
    }
}
