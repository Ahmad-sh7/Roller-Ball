using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    AudioScript audioScript;
    [SerializeField] Image[] ballImages;
    Image selectedImage;
    Color unselectedColor = new Color(0f, 0f, 0f), originalColor;

    void Start()
    {
        audioScript = GameObject.Find("Audio Object").GetComponent<AudioScript>();
        selectedImage = ballImages[0];
        originalColor = selectedImage.color;
        SetSelectedImage(0);
    }

    public void FirstLevelOnClick()
    {
        audioScript.ClickSFX();
        Invoke("LoadFirstLevelScene", 1f);
    }

    public void SecondLevelOnClick()
    {
        audioScript.ClickSFX();
        Invoke("LoadSecondLevelScene", 1f);
    }

    private void LoadFirstLevelScene()
    {
        SceneManager.LoadScene("FirstMapSecne", LoadSceneMode.Single);
    }

    private void LoadSecondLevelScene()
    {
        SceneManager.LoadScene("SecondMapSecne", LoadSceneMode.Single);
    }

    public void OnRubImageSecleted()
    {
        audioScript.HoverSFX();
        selectedImage = ballImages[0];
        SetSelectedImage(0);
    }

    public void OnDogImageSecleted()
    {
        audioScript.HoverSFX();
        selectedImage = ballImages[1];
        SetSelectedImage(1);
    }

    public void OnGirlImageSecleted()
    {
        audioScript.HoverSFX();
        selectedImage = ballImages[2];
        SetSelectedImage(2);
    }

    private void SetSelectedImage(int index)
    {   /*
        for (int i = 0; i < ballImages.Length; i++)
            ballImages[i].color = unselectedColor;
        ballImages[index].color = originalColor;
        */
    }
}
