using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    Renderer objectRenderer;
    Light lightComponent;
    [SerializeField] Material greenMaterial;
    [SerializeField] List<GameObject> objects;
    bool activated = false;
    AudioScript audioScript;

    void Start()
    {
        lightComponent = GetComponent<Light>();
        lightComponent.enabled = false;
        objectRenderer = GetComponent<Renderer>();
        audioScript = GameObject.Find("Audio Object").GetComponent<AudioScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !activated)
        {
            activated = true;
            audioScript.CubeSFX();
            objectRenderer.material = greenMaterial;
            lightComponent.enabled = true;
            Vector3 currentPosition = transform.position;
            currentPosition.y = currentPosition.y - 0.09f;
            transform.position = currentPosition;
            reactivateObjects();
        }
    }

    private void reactivateObjects()
    {
        foreach (GameObject obj in objects) {
            obj.gameObject.SetActive(true);
        }
    }
}
