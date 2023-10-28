using Platformer.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAssets : MonoBehaviour
{
    public static PoolAssets instance;

    public PoolOfDamagePopUp this[string name] => _poolOfDamagePopUps[name];
    private Dictionary<string, PoolOfDamagePopUp> _poolOfDamagePopUps;
    [SerializeField] private List<PoolOfDamagePopUp> _poolOfDamagePopUpList;


    private void Awake()
    {
        instance = this;

        _poolOfDamagePopUps = new Dictionary<string, PoolOfDamagePopUp>();

        foreach (var damagePopUp in _poolOfDamagePopUpList)
        {
            _poolOfDamagePopUps.Add(damagePopUp.name, damagePopUp);
        }
    }
}
