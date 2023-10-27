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


            // is 키워드
            // 객체가 어떤 타입으로 참조할 수 있는지 확인하고 bool 결과를 반환하는 키워드
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
