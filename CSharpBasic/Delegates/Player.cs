using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    internal class Player
    {
        public float hp
        {
            get
            {
                return _hp;
            }

            set
            {
                if (_hp == value)
                    return;

                _hp = value;
                onHpChanged(value);
            }
        }

        private float _hp;

        public delegate void OnHpChangedHandler(float value);
        public event OnHpChangedHandler onHpChanged;
    }
}
