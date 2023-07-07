using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField] AudioClip hoverSFX;
    [SerializeField] AudioClip clickSFX;
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
}
