using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private const int zoomLimit = 25;
    private GameManager gameManager;
    private Camera cam;

    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float speed = 10;
    private float bound;
    private float spacing;
    private bool pause;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UpdateCamera();

    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        if (!pause)
        {
            Vector3 movement = new(horizontalInput, verticalInput);

            if (movement.magnitude > 1)
                movement.Normalize();

            transform.position += speed * Time.deltaTime * movement;

            if (transform.position.x > bound)
                transform.position = new Vector3(bound, transform.position.y, transform.position.z);
            if (transform.position.x < 0)
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            if (transform.position.y > bound)
                transform.position = new Vector3(transform.position.x, bound, transform.position.z);
            if (transform.position.y < 0)
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

    }
    // Update is called once per frame
    public void UpdateCamera()
    {
        bound = gameManager.width;
        spacing = gameManager.spacing;
        transform.position = new Vector3((bound / 2) - 0.5f, (bound / 2) * spacing - 0.5f, -10);

        if (bound < zoomLimit)
        {
            cam.orthographicSize = bound * spacing / 2;
        }
        else cam.orthographicSize = zoomLimit / 2;
    }
    public void pauseCamera()
    {
        pause = !pause;
    }
}
