using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject plane;
    [SerializeField]
    GameObject planePrefab;
    RocketLauncherController rocketLauncher;

    RaycastHit2D hit;

    private void Start()
    {
        rocketLauncher = GameObject.FindGameObjectWithTag("RocketLauncher").GetComponent<RocketLauncherController>();
        CreatePlane();
    }

    void Update()
    {
        if(plane == null)
        {
            CreatePlane();
        }
        Touch();
    }

    void Touch()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "Rocket" || hit.collider.tag == "RocketLauncher")
                        {
                            Debug.Log("Touch");
                            rocketLauncher.CanLaunchRocket = true;
                        }
                    }
                    else
                    {
                        Debug.Log("Touch on Rocket Launcher");
                    }
                    break;
            }
        }
    }

    void CreatePlane()
    {
        plane = Instantiate(planePrefab);
        plane.name = "Plane";
    }
}
