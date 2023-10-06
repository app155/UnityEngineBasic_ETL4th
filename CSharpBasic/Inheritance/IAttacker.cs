using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // 인터페이스는 창구 역할을 해주는 기능들을 추상화하는 용도의 사용자 정의 자료형.
    // 기능의 추상화이므로 각 기능의 구현은 없고 어떤 기능이 있어야 하는지에 대해 선언만 한다.

    internal interface IAttacker
    {
        float AttackPower { get; }

        void Attack(IHp target);
    }
}
