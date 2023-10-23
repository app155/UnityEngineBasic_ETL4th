using Platformer.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageButton : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private Button button;

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => DepleteHp(5));
    }

    void DepleteHp(float amount)
    {
        player.DepleteHp(player, amount);
    }
}
