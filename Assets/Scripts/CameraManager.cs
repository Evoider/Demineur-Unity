using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;

    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float speed = 10;
    private float bound;
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


        if(!pause) {
            Vector3 movement = new(horizontalInput, verticalInput);

            if (movement.magnitude > 1)
                movement.Normalize();

            transform.position += speed * Time.deltaTime * movement;

            if (transform.position.x > bound - 2)
                transform.position = new Vector3(bound - 2, transform.position.y, transform.position.z);
            if (transform.position.x < 2)
                transform.position = new Vector3(2, transform.position.y, transform.position.z);
            if (transform.position.y > bound - 2)
                transform.position = new Vector3(transform.position.x, bound - 2, transform.position.z);
            if (transform.position.y < 2)
                transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        }

    }
    // Update is called once per frame
    public void UpdateCamera()
    {
        bound = gameManager.width;
        transform.position = new Vector3((bound / 2) - 0.5f, (bound / 2) - 0.5f, -10);
        cam.orthographicSize = bound / 2;
    }
    public void pauseCamera()
    {
        pause = !pause;
    }
}
