using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Color hoverColor = new Color(0.8f, 0.675f, 0.45f), originalColor;
    Image image;
    AudioScript audioScript;

    private void Awake()
    {
        audioScript = GameObject.Find("Audio Object").GetComponent<AudioScript>();
        image = GetComponent<Image>();
        originalColor = image.color;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioScript.HoverSFX();
        image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = originalColor;
    }
}
