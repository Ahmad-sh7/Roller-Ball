using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    AudioScript audioScript;
    [SerializeField] Image[] ballImages;

    void Start()
    {
        audioScript = GameObject.Find("Audio Object").GetComponent<AudioScript>();
        SetSelectedImage(0);
        PlayerPrefs.SetInt("SelectedBall", 0);
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
        SetSelectedImage(0);
        PlayerPrefs.SetInt("SelectedBall", 0);
    }

    public void OnDogImageSecleted()
    {
        audioScript.HoverSFX();
        SetSelectedImage(1);
        PlayerPrefs.SetInt("SelectedBall", 1);
    }

    public void OnGirlImageSecleted()
    {
        audioScript.HoverSFX();
        SetSelectedImage(2);
        PlayerPrefs.SetInt("SelectedBall", 2);
    }

    private void SetSelectedImage(int index)
    {
        for (int i = 0; i < ballImages.Length; i++)
            ballImages[i].transform.localScale = new Vector3(1f, 1f, 1f);
        ballImages[index].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
}
