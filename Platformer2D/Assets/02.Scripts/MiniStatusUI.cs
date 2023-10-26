using Platformer.Controllers;
using Platformer.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Platformer.Controllers.CharacterController;

public class MiniStatusUI : MonoBehaviour
{
    private Slider _hpBar;
    private IHp _character;
    private CharacterController _controller;

    void Awake()
    {
        _hpBar = GetComponentInChildren<Slider>();
        _character = transform.root.GetComponent<IHp>();
        _controller = transform.root.GetComponent<EnemyController>();
        _hpBar.maxValue = _character.hpMax;
        _hpBar.minValue = _character.hpMin;
        _hpBar.value = _character.hpValue;
        _character.onHpChanged += Refresh;
        _controller.onDirectionChanged += AdjustDirection;
    }

    public void AdjustDirection()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
