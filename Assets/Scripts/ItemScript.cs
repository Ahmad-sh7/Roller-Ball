using UnityEngine;
using TMPro;

public class ItemScript : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystemPrefab;
    BallScript ballScript;

    void Start()
    {
        ballScript = GameObject.Find("Ball").GetComponent<BallScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballScript.UpdateScore();
            ParticleSystem particleSystem = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            particleSystem.Play();
            gameObject.SetActive(false);
            Invoke("DestroyItem", 1f);
        }
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
    }
}
