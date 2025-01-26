using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBars : MonoBehaviour
{
    [SerializeField] private TMP_Text bubblefishNumberText;
    
    [SerializeField] private Image bubblefishProgressBarFill;
    [SerializeField] private List<ProgressBarFishIndicator> progressBarFishIndicators;
    [SerializeField] private RectTransform bubblefishProgressBarHolder;
    
    [SerializeField] private DepthIndicator depthIndicator;
    [SerializeField] private RectTransform depthHolder;
    
    private int TotalBubblefishesPopped => App.Instance.BubblefishManager.BubblefishPopped;

    public void Initialize()
    {
        App.Instance.EventsNotifier.BubblefishPopped += OnBubblefishPopped;
        
        SetUpFishIndicatorPositions();
    }

    private void OnBubblefishPopped(Bubblefish bubblefish)
    {
        bubblefishNumberText.text = TotalBubblefishesPopped.ToString();
    }

    private void Update()
    {
        UpdateSideBarPointer();
        UpdateTopBarProgress();
    }


    private void UpdateTopBarProgress()
    {
        bubblefishProgressBarFill.fillAmount = App.Instance.BubblefishManager.BubblefishPoppedPercentage / 100f;
    }

    private void UpdateSideBarPointer()
    {
        
    }

    private void SetUpFishIndicatorPositions()
    {
        
    }
}