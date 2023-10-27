using Platformer.Controllers;
using Platformer.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Platformer.Controllers.CharacterController;

namespace Platformer.UI
{
    public class MiniStatusUI : MonoBehaviour
    {
        [SerializeField] private Slider _hpBar;

        private void Start()
        {
            IHp hp = transform.root.GetComponent<IHp>();

            _hpBar.maxValue = hp.hpMax;
            _hpBar.minValue = hp.hpMin;
            _hpBar.value = hp.hpValue;
            hp.onHpChanged += (value) => _hpBar.value = value;


            // is Ű����
            // ��ü�� � Ÿ������ ������ �� �ִ��� Ȯ���ϰ� bool ����� ��ȯ�ϴ� Ű����
            if (hp is CharacterController)
            {
                Vector3 originScale = transform.localScale;
                ((CharacterController)hp).onDirectionChanged += (value) =>
                {
                    transform.localScale = value < 0 ?
                        new Vector3(-originScale.x, originScale.y, originScale.z) :
                        new Vector3(+originScale.x, originScale.y, originScale.z);
                };
            }
        }

    }
}
