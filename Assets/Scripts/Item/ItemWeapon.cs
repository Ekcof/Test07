using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemWeapon : ItemBase
{
    public ItemWeapon(string id, string name, int attack = 0, int defense = 0, int hp = 0) : base(id, name, attack, defense, hp)
    {
    }

    public override void SetRandomValues(int min, int max)
    {
        _attack = UnityEngine.Random.Range(min, max);
        _defense = 0;
        _hp = UnityEngine.Random.Range(min, min + 2);
    }
}
