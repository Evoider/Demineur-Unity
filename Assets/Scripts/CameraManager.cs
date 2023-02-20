using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameManager gameManager;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    public void Update()
    {
        transform.position = new Vector3((gameManager.width / 2) - 0.5f, (gameManager.height / 2) - 0.5f, -10);
        camera.orthographicSize = gameManager.height / 2;
    }
}
