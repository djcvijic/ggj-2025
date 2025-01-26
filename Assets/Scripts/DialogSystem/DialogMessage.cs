using UnityEngine;

public class DialogMessage : MonoBehaviour
{
    [field: SerializeField] public DialogCondition Condition { get; private set; }
    [field: SerializeField] public int Index { get; private set; }

    public bool IsShown => gameObject.activeSelf;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}