using GameLib.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Characters
{
    public abstract class BaseCharacter
    {
        protected uint _health;

        protected uint _damage;

        protected IMyObserver _observer;

        private uint _maxHelth;
        public BaseCharacter(uint health, uint damage, IMyObserver observer)
        {
            _health = health;
            _maxHelth = health;
            _damage = damage;
            _observer = observer;
        }

        //public uint Health { get { return _health;} }

        //public uint Damage { get { return _damage;} }

        public bool IsAlive => _health > 0;


        public virtual void Hit(BaseCharacter enemy)
        {
            if (IsAlive)
            {
                enemy.GetDamaged(_damage);
            }
        }

        public virtual void GetDamaged(uint damage)
        {
            var prevHP = _health;
            _health = _health >= damage ? _health - damage : 0;

            if (prevHP > 0 && _health == 0)
            {
                _observer.Notify(this, "I am fucking dying");
            }
        }

        public virtual void GetHealed(uint hp)
        {
            _health = Math.Max(_maxHelth, _health + hp);
        }
    }
}
