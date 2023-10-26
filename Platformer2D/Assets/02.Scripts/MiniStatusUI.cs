using Platformer.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniStatusUI : MonoBehaviour
{
    private Slider _hpBar;
    private IHp _character;

    void Awake()
    {
        _hpBar = GetComponentInChildren<Slider>();
        _character = transform.root.GetComponent<IHp>();
        _hpBar.maxValue = _character.hpMax;
        _hpBar.minValue = _character.hpMin;
        _hpBar.value = _character.hpValue;
        _character.onHpChanged += Refresh;
    }

    public void Refresh(float value)
    {
        _hpBar.value = value;

        if (value <= 0)
        {
            Invoke("Inactive", 1.0f);
        }
    }

    void Inactive()
    {
        gameObject.SetActive(false);
    }
}
