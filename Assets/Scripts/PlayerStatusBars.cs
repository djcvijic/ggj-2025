using System;
using System.Collections.Generic;
using System.Linq;
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

    private int _previousBubblefishPopped;
    
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

        if (App.Instance.GameSettings.EnemySpawns
            .Select(x => x.RequiredBubblefishToDamage)
            .Any(x => TotalBubblefishesPopped >= x && x > _previousBubblefishPopped))
        {
            Debug.Log("PASSED THRESHOLD");
            App.Instance.EventsNotifier.NotifyPassedFishSizeRequirement();
        }

        _previousBubblefishPopped = TotalBubblefishesPopped;
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