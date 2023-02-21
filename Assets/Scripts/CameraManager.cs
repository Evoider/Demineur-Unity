using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UpdateCamera();
    }

    // Update is called once per frame
    public void UpdateCamera()
    {
        transform.position = new Vector3((gameManager.width / 2) - 0.5f, (gameManager.height / 2) - 0.5f, -10);
        cam.orthographicSize = gameManager.height / 2;
    }
}
