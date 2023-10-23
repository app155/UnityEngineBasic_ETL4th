using Platformer.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Platformer.Controllers.CharacterController;

public class DamageButton : MonoBehaviour
{
    [SerializeField] private CharacterController player;
    [SerializeField] private float damage;
    private Button button;

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => player.DepleteHp(player, damage));
    }
}
