using System;
using TMPro;
using UnityEngine;

public class PlayerScoreHolder : MonoBehaviour
{
    [SerializeField] private TMP_Text bubblefishNumberText;
    
    private int _totalBubblefishesPopped = 0;

    public void Initialize()
    {
        App.Instance.EventsNotifier.SubscribeBubblefishPopped(OnBubblefishPopped);
    }

    private void OnBubblefishPopped(Bubblefish bubblefish)
    {
        _totalBubblefishesPopped++;
        bubblefishNumberText.text = _totalBubblefishesPopped.ToString();
    }
}