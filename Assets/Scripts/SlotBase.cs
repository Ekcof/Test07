using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotBase : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    private ItemBase _currentItem;
    private Sprite _defaultSprite;
    private Color _defaultColor;

    public ItemBase CurrentItem => _currentItem;

    private void Awake()
    {
        _defaultSprite = _image.sprite;
        _defaultColor = _image.color;
        _button.onClick.AddListener(OnPress);
    }

    public void ClearItem()
    {
        _currentItem = null;
    }

    public void SetItem(ItemBase newItem)
    {
        _currentItem = newItem;
        _image.sprite = SpriteHandler.GetSpriteByName(newItem.Id);
        _image.color = Color.white;
        DOTween.Kill(transform);
        transform.DOPunchScale(new Vector2(0.1f, 0.1f), 0.3f);
    }

    private void OnPress()
    {
        EventsBus.Publish(new OnItemSlotSelected { Slot = this });
        DOTween.Kill(transform);
        transform.DOPunchScale(new Vector2(0.2f, 0.2f), 0.3f);
    }

    private void ClearItemView()
    {
        if (_currentItem == null)
        {
            _image.sprite = _defaultSprite;
            _image.color = _defaultColor;
        }
    }
}
