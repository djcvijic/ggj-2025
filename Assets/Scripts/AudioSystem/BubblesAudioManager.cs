using System.Collections;
using Audio;
using UnityEngine;

public class BubblesAudioManager : AudioManager
{
    [Header("Music")]
    [SerializeField] private AudioClipSettings musicStart;
    [SerializeField] private AudioClipSettings musicLoop;

    [Header("Sounds")]
    [SerializeField] private AudioClipSettings dash;
    [SerializeField] private AudioClipSettings bubblePop;
    [SerializeField] private AudioClipSettings unlockNewDepth;
    [SerializeField] private AudioClipSettings enemyKill;
    [SerializeField] private AudioClipSettings bossKill;

    private bool _musicPlaying;

    public void StartMusic()
    {
        StopAudio(musicLoop);

        if (!_musicPlaying)
        {
            _musicPlaying = true;
            PlayAudio(musicStart);
            StartCoroutine(StartMusicLoop());
        }
    }

    private IEnumerator StartMusicLoop()
    {
        yield return new WaitForSeconds(musicStart.Variants[0].length);

        StopAudio(musicStart);
        PlayAudio(musicLoop);
    }

    public void Dash() => PlayAudio(dash);

    public void BubblePop() => PlayAudio(bubblePop);

    public void UnlockNewDepth() => PlayAudio(unlockNewDepth);

    public void EnemyKill() => PlayAudio(enemyKill);

    public void BossKill() => PlayAudio(bossKill);
}