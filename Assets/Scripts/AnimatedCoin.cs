using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCoin : MonoBehaviour
{
    private Vector2 _originalPos;
    private RectTransform _rect;

    private void Start()
    {
        _rect = (RectTransform)transform;
        _originalPos = transform.position;
    }

    public void StartAnimation(float lowerBorder, float horizontalLimit, Vector2 destination, Action onDone)
    {
        DOTween.Kill(_rect);
        float randomX = (UnityEngine.Random.Range(_originalPos.x - horizontalLimit, _originalPos.x + horizontalLimit) - horizontalLimit);
        float randomTime = UnityEngine.Random.Range(0.5f, 0.7f);
        _rect.DOScale(1f, 0.3f);
        _rect.DOAnchorPosX(randomX, randomTime);
        _rect.DOAnchorPosY(lowerBorder, randomTime).SetEase(Ease.InQuad).

            OnComplete(() =>
            {
                _rect.DOPunchAnchorPos(Vector3.up * UnityEngine.Random.Range(20, 60), UnityEngine.Random.Range(0.3f, 0.6f), UnityEngine.Random.Range(3, 5)).OnComplete(() =>
                  {
                      _rect.DOMove(destination, 0.5f).OnComplete(() =>
                      {
                          _rect.DOScale(0, 0);
                          _rect.DOMove(_originalPos, 0).OnComplete(() =>
                          {
                              DOTween.Kill(_rect);
                              onDone?.Invoke();
                          });
                      });
                  });
            }
            );
    }
}
