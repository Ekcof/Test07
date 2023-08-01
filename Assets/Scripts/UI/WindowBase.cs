using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowBase : MonoBehaviour
{
    [SerializeField] private protected Button _blockPanel;
    private protected bool _isHidden = true;

    private protected virtual void Awake()
    {
        if (_blockPanel != null)
        {
            _blockPanel.onClick.RemoveAllListeners();
        }
    }

    private protected virtual void Show()
    {
        if (_blockPanel != null)
        {
            _blockPanel.onClick.RemoveAllListeners();
            _blockPanel.onClick.AddListener(() => Hide(0));
        }
        DOTween.Kill(transform);
        transform.DOScale(1, 0.3f);
        if (_blockPanel != null)
            _blockPanel.gameObject.SetActive(true);
        _isHidden = false;
    }

    private protected virtual void Hide(float hideTime = 0.3f)
    {
        if (_isHidden)
            return;
        if (_blockPanel != null)
        {
            _blockPanel.gameObject.SetActive(false);
        }
        DOTween.Kill(transform);
        transform.DOScale(0, hideTime);
        _isHidden = true;
    }

    private protected virtual void OnDestroy()
    {
        if (_blockPanel != null)
            _blockPanel.onClick.RemoveAllListeners();
        DOTween.Kill(transform);
    }
}
