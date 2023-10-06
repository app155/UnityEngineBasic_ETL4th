using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // abstract 키워드: 
    // 추상 용도로 사용하는 것이므로 반드시 상속자가 해당 내용을 직접 구현해주어야 한다고 명시.

    internal abstract class Character : IAttacker, IHp
    {
        public string NickName { get; set; }

        public float Exp
        {
            get
            {
                return _exp;
            }
            private set
            {
                if (value < 0)
                    value = 0;

                _exp = value;
            }
        }

        public float AttackPower
        {
            get
            {
                return _attackPower;
            }
        }

        public float HpValue => throw new NotImplementedException();

        public float HpMax => throw new NotImplementedException();

        public float HpMin => throw new NotImplementedException();

        private float _exp;
        private float _attackPower;

        public float GetExp()
        {
            return _exp;
        }

        public void SetExp(float value)
        {
            if (value < 0)
                value = 0;

            _exp = value;
        }

        public abstract void Breath();

        public virtual void Jump()
        {
            Console.WriteLine("Jump");
        }

        public void Attack(IHp target)
        {
            target.DepleteHp(AttackPower);
        }

        public void RecoverHp(float value)
        {
            throw new NotImplementedException();
        }

        public void DepleteHp(float value)
        {
            throw new NotImplementedException();
        }
    }
}
