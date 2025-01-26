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

    private bool _notifiedOnce = false;
    
    private int TotalBubblefishesPopped => App.Instance.BubblefishManager.BubblefishPopped;

    public void Initialize()
    {
        App.Instance.EventsNotifier.BubblefishPopped += OnBubblefishPopped;
        
        SetUpFishIndicatorPositions();
        depthIndicator.Initialize();
    }

    private void OnBubblefishPopped(Bubblefish bubblefish)
    {
        bubblefishNumberText.text = TotalBubblefishesPopped.ToString();

        if (TotalBubblefishesPopped >= 10 && !_notifiedOnce)
        {
            _notifiedOnce = true;
            Debug.Log("PASSED FIRST THRESHOLD");
            App.Instance.EventsNotifier.NotifyFirstFishSizeRequirement();
        }
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