using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompareView : WindowBase
{
    [SerializeField] private Button _dropButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private PlayerLogic _playerData;

    [SerializeField] private Image _logo;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _attackText;
    [SerializeField] private TextMeshProUGUI _defenseText;
    [SerializeField] private TextMeshProUGUI _hpText;

    [SerializeField] private Image _newLogo;
    [SerializeField] private TextMeshProUGUI _newTitle;
    [SerializeField] private TextMeshProUGUI _newAttackText;
    [SerializeField] private TextMeshProUGUI _newDefenseText;
    [SerializeField] private TextMeshProUGUI _newHpText;

    private bool _isShowing;

    private protected override void Awake()
    {
        EventsBus.Subscribe<OnItemTaken>(OnItemTaken);
        base.Awake();
    }

    private protected override void OnDestroy()
    {
        _dropButton.onClick.RemoveAllListeners();
        _equipButton.onClick.RemoveAllListeners();
        EventsBus.Unsubscribe<OnItemTaken>(OnItemTaken);
        base.OnDestroy();
    }

    private protected override void Hide(float hideTime = 0.3F)
    {
        _dropButton.onClick.RemoveAllListeners();
        _equipButton.onClick.RemoveAllListeners();
        base.Hide(hideTime);
    }

    private void OnItemTaken(OnItemTaken data)
    {
        var newItem = data.Item;
        if (newItem == null)
            return;

        if (((_playerData.Helmet == null || string.IsNullOrEmpty(_playerData.Helmet.Id)) && newItem is ItemHelmet)
            ||
            ((_playerData.Weapon == null || string.IsNullOrEmpty(_playerData.Weapon.Id)) && newItem is ItemWeapon)
            ||
            ((_playerData.Shield == null || string.IsNullOrEmpty(_playerData.Shield.Id)) && newItem is ItemShield)
            )
        {
            EventsBus.Publish(new OnItemObtained { ObtainedItem = data.Item });
            return;
        }

        DOTween.Kill(transform);
        Show();
        _isShowing = true;

        var playerItem = newItem is ItemHelmet ? (ItemBase)_playerData.Helmet : (newItem is ItemWeapon ? _playerData.Weapon : _playerData.Shield);


        DisplayItemInfo(playerItem, newItem, _logo, _title, _attackText, _defenseText, _hpText);
        DisplayItemInfo(newItem, playerItem, _newLogo, _newTitle, _newAttackText, _newDefenseText, _newHpText);

        _equipButton.onClick.AddListener(() => EquipItem(newItem));
        _dropButton.onClick.AddListener(() => DropItem(newItem));
        _blockPanel.onClick.AddListener(() => DropItem(newItem));
    }

    private void DisplayItemInfo(ItemBase item1, ItemBase item2, Image logo, TextMeshProUGUI title, TextMeshProUGUI attackText, TextMeshProUGUI defenseText, TextMeshProUGUI hpText)
    {
        if (item1 == null)
            return;
        logo.sprite = SpriteHandler.GetSpriteByName(item1.Id);
        title.text = item1.Name;
        attackText.text = $"{item1.Attack}{ShowCompareValues(item1.Attack, item2.Attack)}";
        defenseText.text = $"{item1.Defense}{ShowCompareValues(item1.Defense, item2.Defense)}";
        hpText.text = $"{item1.HP}{ShowCompareValues(item1.HP, item2.HP)}";
    }

    private string ShowCompareValues(int value1, int value2)
    {
        if (value1 > value2)
            return "<sprite=0>";
        else if (value1 < value2)
            return "<sprite=1>";
        else
            return "";
    }

    private void EquipItem(ItemBase item)
    {
        _isShowing = false;
        EventsBus.Publish(new OnItemObtained { ObtainedItem = item });
        Hide(0);
    }

    private void DropItem(ItemBase item)
    {
        _isShowing = false;
        EventsBus.Publish(new OnDropItem { Item = item });
        Hide(0);
    }
}
