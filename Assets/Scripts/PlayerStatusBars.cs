using System;
using TMPro;
using UnityEngine;

public class PlayerStatusBars : MonoBehaviour
{
    [SerializeField] private TMP_Text bubblefishNumberText;

    private int TotalBubblefishesPopped => App.Instance.BubblefishManager.BubblefishPopped;

    public void Initialize()
    {
        App.Instance.EventsNotifier.BubblefishPopped += OnBubblefishPopped;
    }

    private void OnBubblefishPopped(Bubblefish bubblefish)
    {
        bubblefishNumberText.text = TotalBubblefishesPopped.ToString();
    }
}