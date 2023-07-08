using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField] AudioClip hoverSFX;
    [SerializeField] AudioClip clickSFX;
    [SerializeField] AudioClip collectItemSFX;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip goalSFX;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }

    public void HoverSFX()
    {
        audioSource.PlayOneShot(hoverSFX, 0.5f);
    }

    public void ClickSFX()
    {
        audioSource.PlayOneShot(clickSFX, 0.5f);
    }

    public void CollectItemSFX()
    {
        audioSource.PlayOneShot(collectItemSFX, 0.5f);
    }

    public void JumpSFX()
    {
        audioSource.PlayOneShot(jumpSFX, 0.5f);
    }

    public void GoalSFX()
    {
        audioSource.PlayOneShot(goalSFX, 0.5f);
    }
}
