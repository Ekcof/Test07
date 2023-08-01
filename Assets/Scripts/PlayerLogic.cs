using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private int _money;
    [SerializeField] private int _level;
    [SerializeField] private ItemHelmet _helmet;
    [SerializeField] private ItemWeapon _weapon;
    [SerializeField] private ItemShield _shield;
    [SerializeField] private ItemHolder _holder;
    [SerializeField] private SlotBase _helmetSlot;
    [SerializeField] private SlotBase _weaponSlot;
    [SerializeField] private SlotBase _shieldSlot;

    private int _attack;
    private int _defense;
    private int _hp;

    public int Money => _money;
    public int Level => _level;
    public ItemHelmet Helmet => _helmet;
    public ItemWeapon Weapon => _weapon;
    public ItemShield Shield => _shield;

    private void Awake()
    {
        EventsBus.Subscribe<OnItemObtained>(OnItemObtained);
        EventsBus.Subscribe<OnDropItem>(OnDropItem);
        EventsBus.Subscribe<OnPlayerButtonPressed>(OnPlayerButtonPressed);
    }

    private void OnDestroy()
    {
        EventsBus.Unsubscribe<OnDropItem>(OnDropItem);
        EventsBus.Unsubscribe<OnItemObtained>(OnItemObtained);
        EventsBus.Unsubscribe<OnPlayerButtonPressed>(OnPlayerButtonPressed);
    }

    private void RecalculateParameters()
    {
        List<ItemBase> items = new()
        {
            _helmet,
            _weapon,
            _shield
        };
        items.RemoveAll(item => item == null);

        if (items.Count > 0)
        {
            _attack = items.Sum(item => item.Attack);
            _defense = items.Sum(item => item.Defense);
            _hp = items.Sum(item => item.HP);
        }
        else
        {
            _attack = 0;
            _defense = 0;
            _hp = 0;
        }

        _playerView.RefreshParameters(_attack, _defense, _hp);
    }

    private void OnItemObtained(OnItemObtained data)
    {
        ItemBase itemToDrop = null;
        if (data.ObtainedItem is ItemHelmet helmet)
        {
            _helmetSlot.SetItem(helmet);
            itemToDrop = _helmet;
            _helmet = helmet;
        }
        else if (data.ObtainedItem is ItemWeapon weapon)
        {
            _weaponSlot.SetItem(weapon);
            itemToDrop = _weapon;
            _weapon = weapon;
        }
        else if (data.ObtainedItem is ItemShield shield)
        {
            _shieldSlot.SetItem(shield);
            itemToDrop = _shield;
            _shield = shield;
        }

        if (itemToDrop != null && !string.IsNullOrEmpty(itemToDrop.Id))
            _playerView.ShowSellingEffect(() => SellItem(itemToDrop));

        RecalculateParameters();
    }

    private void OnPlayerButtonPressed(OnPlayerButtonPressed data)
    {
        var item = _holder.GetRandomItemWithSubclassCheck();

        if (item != null && !string.IsNullOrEmpty(item.Id))
            EventsBus.Publish(new OnItemTaken { Item = item });
    }

    private void SellItem(ItemBase item)
    {
        var gain = item.GetBaseCost();
        _money += gain;
        _playerView.ShowMoneyText(_money);
    }

    private void OnDropItem(OnDropItem data)
    {
        _playerView.ShowSellingEffect(() => SellItem(data.Item));
    }
}
