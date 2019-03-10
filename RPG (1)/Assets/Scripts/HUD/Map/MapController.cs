using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public float maxSize = 20, minSize = 7;

    public GameObject cameraGameObject;

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        camera = cameraGameObject.GetComponent<Camera>();

    }

    public void ShowMap()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (camera.orthographicSize >= minSize)
            {
                camera.orthographicSize--;
            }
           
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (camera.orthographicSize <= maxSize)
            {
                camera.orthographicSize++;
            }
        }
    }


}
