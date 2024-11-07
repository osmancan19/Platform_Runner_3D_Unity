using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip background;
    [SerializeField] private AudioClip collectCoin;
    [SerializeField] private AudioClip fail;
    [SerializeField] private AudioClip finishLine;
    [SerializeField] private AudioClip countDown;

    private AudioSource audioSource;

    void Awake()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayBackgroundSound()
    {
        audioSource.clip = background;
        audioSource.loop = true; // Arka plan sesi sürekli çalsın
        audioSource.Play();
    }

    public void PlayCollectCoinSound()
    {
        audioSource.PlayOneShot(collectCoin);
    }

    public void PlayFailSound()
    {
        audioSource.PlayOneShot(fail);
    }

    public void PlayFinishLineSound()
    {
        audioSource.PlayOneShot(finishLine);
    }
    public void PlayCountDownSound()
    {
        audioSource.PlayOneShot(countDown);
    }
}
