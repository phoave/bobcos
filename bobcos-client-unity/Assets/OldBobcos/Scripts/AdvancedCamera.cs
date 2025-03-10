using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCamera : MonoBehaviour
{
    public static AdvancedCamera instance;
   public GameObject Player;
    Camera camera;
    public GameObject Background;

    private void Start()
    {
        camera = GetComponentInChildren<Camera>();
        instance = this;
    }
    void FixedUpdate()
    {
     camera.orthographicSize = (Input.GetAxis("Mouse ScrollWheel") * -5) + camera.orthographicSize;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 1, 13);
        Background.transform.localScale = new Vector3(camera.orthographicSize +2.5f, camera.orthographicSize + 2.5f);
      




        if (Player != null)
        {



            transform.position = Vector3.Lerp(transform.position, Player.transform.position, 10 * Time.deltaTime);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 1.76f * camera.orthographicSize, 86.64024f), transform.position.y, -1.691383f);
        }
    }

    public void ZoomOut()
    {

        camera.orthographicSize = 1 + camera.orthographicSize;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 1, 13);
        Background.transform.localScale = new Vector3(camera.orthographicSize + 2.5f, camera.orthographicSize + 2.5f);

    }

    public void ZoomIn()
    {
        camera.orthographicSize = -1 + camera.orthographicSize;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 1, 13);
        Background.transform.localScale = new Vector3(camera.orthographicSize + 2.5f, camera.orthographicSize + 2.5f);
    }
}
