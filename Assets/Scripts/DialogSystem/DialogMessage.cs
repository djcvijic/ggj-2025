using Audio;
using UnityEngine;

public class DialogMessage : MonoBehaviour
{
    [field: SerializeField] public DialogCondition Condition { get; private set; }
    [field: SerializeField] public int Index { get; private set; }
    [field: SerializeField] public AudioClipSettings PlayOnShow { get; private set; }

    public bool IsShown => gameObject.activeSelf;

    public void Show()
    {
        gameObject.SetActive(true);
        if (PlayOnShow != null)
        {
            App.Instance.AudioManager.PlayAudio(PlayOnShow);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}