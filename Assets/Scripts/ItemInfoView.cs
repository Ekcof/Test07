using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ItemInfoView : WindowBase
{
    [SerializeField] private Image _logo;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _attackText;
    [SerializeField] private TextMeshProUGUI _defenseText;
    [SerializeField] private TextMeshProUGUI _hpText;

    private protected override void Awake()
    {
        EventsBus.Subscribe<OnItemSlotSelected>(OnItemSlotSelected);
        EventsBus.Subscribe<OnItemTaken>(OnItemTaken);
        base.Awake();
    }

    private protected override void OnDestroy()
    {
        EventsBus.Unsubscribe<OnItemSlotSelected>(OnItemSlotSelected);
        EventsBus.Unsubscribe<OnItemTaken>(OnItemTaken);
        DOTween.Kill(transform);
        base.OnDestroy();
    }

    private void OnItemSlotSelected(OnItemSlotSelected data)
    {
        if (data.Slot.CurrentItem == null)
        {
            Hide(0);
            return;
        }

        _logo.sprite = SpriteHandler.GetSpriteByName(data.Slot.CurrentItem.Id);
        _title.text = data.Slot.CurrentItem.Name;
        _attackText.text = data.Slot.CurrentItem.Attack.ToString();
        _defenseText.text = data.Slot.CurrentItem.Defense.ToString();
        _hpText.text = data.Slot.CurrentItem.HP.ToString();

        Show();
    }

    private void OnItemTaken(OnItemTaken data)
    {
        if (!_isHidden)
            Hide(0);
    }
}
