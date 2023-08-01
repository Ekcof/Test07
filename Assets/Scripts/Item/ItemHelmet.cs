using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemHelmet : ItemBase
{
    public ItemHelmet(string id, string name, int attack = 0, int defense = 0, int hp = 0) : base(id, name, attack, defense, hp)
    {
    }

    public override void SetRandomValues(int min, int max)
    {
        _attack = 0;
        _defense = UnityEngine.Random.Range(min, min + 2);
        _hp = UnityEngine.Random.Range(min, max);
    }
}
