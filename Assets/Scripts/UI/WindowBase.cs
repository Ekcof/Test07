using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowBase : MonoBehaviour
{
    [SerializeField] private protected Button _blockPanel;
    private bool _isHidden = true;
    // Start is called before the first frame update

    private protected virtual void Awake()
    {
        if (_blockPanel != null)
        {
            _blockPanel.onClick.RemoveAllListeners();
            _blockPanel.onClick.AddListener(() => Hide(0));
        }
    }

    private protected virtual void Show()
    {
        DOTween.Kill(transform);
        transform.DOScale(1, 0.3f);
        if (_blockPanel != null)
            _blockPanel.gameObject.SetActive(true);
        _isHidden = false;
    }

    private protected virtual void Hide(float hideTime = 0.3f)
    {
        if (_blockPanel != null)
        {
            _blockPanel.gameObject.SetActive(false);
            _blockPanel.onClick.RemoveAllListeners();
        }
        DOTween.Kill(transform);
        transform.DOScale(0, hideTime).OnComplete(() =>
        {
            Debug.Log("What hapnn");
            if (_blockPanel != null)
                _blockPanel.onClick.AddListener(() => Hide(0));
            _isHidden = true;
        });
    }

    private protected virtual void OnDestroy()
    {
        if (_blockPanel != null)
            _blockPanel.onClick.RemoveAllListeners();
    }
}
