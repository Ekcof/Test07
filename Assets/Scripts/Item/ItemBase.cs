using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemBase
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;

    [SerializeField] private protected int _attack;
    [SerializeField] private protected int _defense;
    [SerializeField] private protected int _hp;

    private int baseCost;
    private SlotBase _currentSlot;

    public string Id => _id;
    public string Name => _name;
    public int Attack => _attack;
    public int Defense => _defense;
    public int HP => _hp;

    public ItemBase(string id, string name, int attack = 0, int defense = 0, int hp = 0)
    {
        _id = id;
        _name = name;
        _attack = attack;
        _defense = defense;
        _hp = hp;
    }

    public int GetBaseCost(float coef = 1)
    {
        float baseCost = (_attack + _defense + _hp) * coef;

        return (int)baseCost;
    }

    public virtual void SetRandomValues(int min, int max)
    {
    }
}