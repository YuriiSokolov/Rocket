using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public bool launched = false;
    [SerializeField]
    GameObject crossHair;
    Vector3 targetPosition;
    bool targetSet = false;
    float distance = 0;
    float speed = 1;

    private void Start()
    {
        crossHair = GameObject.FindGameObjectWithTag("CrossHair");
    }

    private void FixedUpdate()
    {
        if(launched == true)
        {
            MoveRocket();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            Debug.Log("Boom!");
            RocketDestroy();
        }
    }

    void RocketDestroy()
    {
        Destroy(this.gameObject);
    }

    void MoveRocket()
    {
        GetTarget();

        if(transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed);
        }
        else
        {
            Debug.Log("Rocket Lost");
            RocketDestroy();
        }
    }

    void GetTarget()
    {
        if (targetSet == false)
        {
            targetPosition = crossHair.transform.position;
            LookAt();
            targetSet = true;
        }
    }

    void LookAt()
    {
        var dir = targetPosition - transform.position;
        var angle = 360 - Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
