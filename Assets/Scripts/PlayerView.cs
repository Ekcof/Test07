using DG.Tweening;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _attackText;
    [SerializeField] private TextMeshProUGUI _defenseText;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private AnimatedCoin[] _animatedCoins;
    [SerializeField] private SkeletonAnimation _animatedCharacter;
    [SerializeField] private string _animHitKey;
    [SerializeField] private string _animLoopKey;

    private int _completedAnimations;
    private Action _onCompleteCoinAnimations;
    private Action _onAnimationEndAction;

    // Start is called before the first frame update
    private void Awake()
    {
        _playButton.onClick.RemoveAllListeners();
        _playButton.onClick.AddListener(OnButtonPressed);
        EventsBus.Subscribe<OnItemObtained>(OnItemObtained);

        _animatedCharacter.AnimationState.SetAnimation(0, _animLoopKey, true);
    }

    private void OnDestroy()
    {
        EventsBus.Unsubscribe<OnItemObtained>(OnItemObtained);
        _playButton.onClick.RemoveAllListeners();
        _animatedCharacter.state.Complete -= OnAnimationEndEvent;
        _onCompleteCoinAnimations = null;
    }

    private void SetCharacterAnimation(string key, bool isLoop, Action onDone)
    {
        if (_animatedCharacter != null)
        {
            _onAnimationEndAction = onDone;
            _animatedCharacter.state.Complete += OnAnimationEndEvent;
            _animatedCharacter.AnimationState.SetAnimation(0, key, isLoop);
        }
        else
            onDone?.Invoke();
    }

    private void OnAnimationEndEvent(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == _animHitKey)
        {
            _onAnimationEndAction?.Invoke();
            _animatedCharacter.AnimationState.SetAnimation(0, _animLoopKey, true);
            _animatedCharacter.state.Complete -= OnAnimationEndEvent;
        }
    }

    private void OnButtonPressed()
    {
        _playButton.interactable = false;
        SetCharacterAnimation("attack", false, () =>
        EventsBus.Publish(new OnPlayerButtonPressed { }));
    }

    public void ShowSellingEffect(Action onDone)
    {
        foreach (var coin in _animatedCoins)
            coin.StartAnimation(-300, Screen.width / 2, _moneyText.transform.position, OnCoinAnimationComplete);
        _onCompleteCoinAnimations = onDone;
    }

    public void RefreshParameters(int attack, int defense, int hp)
    {
        _attackText.text = $"ATK: {attack}";
        _defenseText.text = $"DEF: {defense}";
        _hpText.text = $"HP: {hp}";
    }

    public void ShowMoneyText(int money)
    {
        _moneyText.text = money.ToString();
        _playButton.interactable = true;
    }

    private void OnItemObtained(OnItemObtained data)
    {
        _playButton.interactable = true;
    }

    private void OnCoinAnimationComplete()
    {
        _completedAnimations++;
        if (_completedAnimations == _animatedCoins.Length)
        {
            _onCompleteCoinAnimations?.Invoke();
            _onCompleteCoinAnimations = null;
            _completedAnimations = 0;
        }
    }
}
