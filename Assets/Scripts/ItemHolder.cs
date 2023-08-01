using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.U2D;

/// <summary>
/// Stores the item's data
/// </summary>
[CreateAssetMenu(fileName = "Item Holder", menuName = "Assets/Item Holder")]

public class ItemHolder : ScriptableObject
{
    [Header(" оличество возможных повторенний класса")]
    [SerializeField] private int _sameItemLimit;
    [Space(20)]
    [Header("Ћимитные значени€ рандомных показателей")]
    [SerializeField] private int _minLimit = 1;
    [SerializeField] private int _maxLimit = 15;
    [Space(20)]
    [Header("’ранилище всех возможных итемов")]
    [SerializeField] private ItemHelmet[] _helmets;
    [SerializeField] private ItemWeapon[] _weapons;
    [SerializeField] private ItemShield[] _shields;


    private Type _previousType;

    public ItemHelmet[] Helmets => _helmets;
    public ItemWeapon[] Weapons => _weapons;
    public ItemShield[] Shields => _shields;
    private int _prevQueque;

    public ItemBase GetRandomItemWithSubclassCheck()
    {
        var allItems = new List<ItemBase>();
        allItems.AddRange(_helmets);
        allItems.AddRange(_weapons);
        allItems.AddRange(_shields);

        while (true)
        {
            int randomIndex = UnityEngine.Random.Range(0, allItems.Count);
            ItemBase itemFromSO = allItems[randomIndex];

            if (_prevQueque >= 3 && itemFromSO.GetType() == _previousType)
            {
                continue;
            }

            if (_prevQueque >= 3)
            {
                _prevQueque = 0;
            }

            _previousType = itemFromSO.GetType();
            ItemBase randomItem = itemFromSO is ItemHelmet ? new ItemHelmet(itemFromSO.Id, itemFromSO.Name) :
                itemFromSO is ItemWeapon ? new ItemWeapon(itemFromSO.Id, itemFromSO.Name) :
                itemFromSO is ItemShield ? new ItemShield(itemFromSO.Id, itemFromSO.Name) : null;

            if (randomItem != null)
                randomItem.SetRandomValues(_minLimit, _maxLimit);
            return randomItem;
        }
    }
}

