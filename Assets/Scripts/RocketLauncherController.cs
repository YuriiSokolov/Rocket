using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherController : MonoBehaviour
{
    [SerializeField]
    GameObject rocketLauncherMovePart;
    [SerializeField]
    GameObject rocketPrefab;
    float distanceToCurrentRocket;
    GameObject rocket;

    GameObject target;
    float targetSpeed;

    [SerializeField]
    GameObject crossHair;
    bool canLaunchRocket = false;

    public bool CanLaunchRocket
    {
        set
        {
            canLaunchRocket = value;
        }
    }


    private void Start()
    {
        AddRocket();
        CreateCrossHair();

        StartCoroutine(CalculateAim());
    }

    private void Update()
    {
        if (rocket != null)
        {
            distanceToCurrentRocket = Vector2.Distance(transform.position, rocket.transform.position);
        }
        

        if (RocketExist() == false && distanceToCurrentRocket > 2 || rocket == null)
        {
            AddRocket();
        }

        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Target");
        }

        if(target != null)
        {
            targetSpeed = target.GetComponent<PlaneController>().speed;
        }
    }

    private void FixedUpdate()
    {
        if (canLaunchRocket == true && RocketExist() == true)
        {
            canLaunchRocket = false;
            LaunchRocket();
        }
    }

    void AddRocket()
    {
        rocket = Instantiate(rocketPrefab);
        rocket.name = "Rocket";
        rocket.transform.parent = rocketLauncherMovePart.transform;
        rocket.transform.localPosition = Vector3.zero;
        rocket.transform.localRotation = Quaternion.Euler(Vector3.zero);
    } 

    void LaunchRocket()
    {
        if (rocket != null)
        {
            rocket.transform.parent = null;
            rocket.GetComponent<RocketController>().launched = true;
        }
    }

    bool RocketExist()
    {
        if(rocketLauncherMovePart.transform.childCount == 0)
        {
            return false;
        }
        else
        {
            for(int i = 0; i < rocketLauncherMovePart.transform.childCount; i++)
            {
                if(rocketLauncherMovePart.transform.GetChild(i).gameObject.name == "Rocket")
                {
                    return true;
                }
            }
        }
        return false;
    }

    void CreateCrossHair()
    {
        crossHair = Instantiate(crossHair);
        crossHair.name = "CrossHair";
    }

    IEnumerator CalculateAim()
    {
        while (true)
        {
            yield return null;

            if (target != null)
            {
                float crossHairX = 0;
                float crossHairY = 0;

                for (int i = 0; i < 10; i++)
                {
                    Vector2 targetingPosition = target.transform.position;

                    crossHairX = (transform.position.x + ((targetingPosition.x - transform.position.x) / (1 - targetSpeed)) * 1);
                    crossHairY = (transform.position.y + ((targetingPosition.y - transform.position.y) / (1 - targetSpeed)) * 1);
                }

                crossHair.transform.position = new Vector2(crossHairX, crossHairY);
            }
        }
    }
}

