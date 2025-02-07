using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    private int _currentIndex;
    private List<DialogMessage> _allMessages;
    private bool _newDepthUnlocked;

    private DialogMessage CurrentMessage => _allMessages.FirstOrDefault(x => x.Index == _currentIndex);

    public bool IsMessageShown => CurrentMessage != null && CurrentMessage.IsShown;

    private void Awake()
    {
        App.Instance.EventsNotifier.PassedFishSizeRequirement += NewDepthUnlocked;
    }

    private void OnDestroy()
    {
        App.Instance.EventsNotifier.PassedFishSizeRequirement -= NewDepthUnlocked;
    }

    private void NewDepthUnlocked()
    {
        _newDepthUnlocked = true;
    }

    public void Initialize()
    {
        _allMessages = FindObjectsByType<DialogMessage>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .OrderBy(x => x.Index)
            .ToList();
        _currentIndex = _allMessages.Min(x => x.Index);
    }
    
    private void Update()
    {
        if (IsMessageShown)
        {
            if (Input.GetMouseButtonDown(0))
                HideCurrentMessageAndAdvance();
            else
                return;
        }

        if (CurrentMessage != null)
        {
            TryShowCurrentMessage();
        }
    }

    
    private void HideCurrentMessageAndAdvance()
    {
        CurrentMessage.Hide();
        if (_allMessages.Any(x => x.Index > _currentIndex))
        {
            _currentIndex = _allMessages.First(x => x.Index > _currentIndex).Index;
        }
        else
        {
            Invoke(nameof(TriggerGameOver), 2f);
        }
    }

    private void TriggerGameOver()
    {
        App.Instance.GameWin.TriggerWinGame();
    }

    private bool IsConditionMet(DialogCondition condition)
    {
        return condition switch
        {
            DialogCondition.None => true,
            DialogCondition.FishPopped => App.Instance.BubblefishManager.BubblefishPopped > 0,
            DialogCondition.BossDefeated => !App.Instance.EnemyManager.EnemyAlive(x => x.IsBoss),
            DialogCondition.NewDepthUnlocked => _newDepthUnlocked,
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private void TryShowCurrentMessage()
    {
        if (IsConditionMet(CurrentMessage.Condition))
        {
            CurrentMessage.Show();
        }
    }
}