using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    internal class Wizard : Character
    {
        public override void Breath()
        {
            throw new NotImplementedException();
        }

        public override void Jump()
        {
            base.Jump();
        }

        public void FireBall()
        {
            Console.WriteLine("Fireball");
        }
    }
}
