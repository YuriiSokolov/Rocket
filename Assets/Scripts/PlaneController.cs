using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    List<Vector2> wayPoints;
    int wayPointID = 0;
    [SerializeField]
    public float speed;

    Vector2 minPos;
    Vector2 maxPos;

    float minX;
    float maxX;
    float minY;
    float maxY;

    private void Awake()
    {
        GetMaxMinPositionXY();
        GenerateWayPoints(Random.Range(0, 4));
        PlaneSpawn();
        speed = Random.Range(0.2f, 0.4f);
        StartCoroutine(CheckBoards());
    }

    private void FixedUpdate()
    {
        MovePlane();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Rocket")
        {
            PlaneDestroy();
        }
    }

    void GenerateWayPoints(int routeType)
    {
        wayPoints = new List<Vector2>();
        //wayPoints.Add(new Vector2(minX, maxY));

        if (routeType == 0)
        {
            FirstTrajectory();
            Debug.Log("Route Type 0");
        }
        else if (routeType == 1)
        {
            SecondTrajectory();
            Debug.Log("Route Type 1");
        }
        else if(routeType == 2)
        {
            ThirdTrajectory();
            Debug.Log("Route Type 2");
        }
        else
        {
            FourthTrajectory();
            Debug.Log("Route Type 3");
        }
    }

    void FirstTrajectory()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;

        while (true)
        {
            if (posX > maxX || posX < minX || posY < minY || posY > maxY)
            {
                break;
            }

            posX = posX + 0.5f;
            posY = posX;

            wayPoints.Add(new Vector2(posX, posY));
        }
    }

    void SecondTrajectory()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;

        while (true)
        {
            if (posX > maxX || posX < minX || posY < minY || posY > maxY)
            {
                break;
            }

            posX = posX + 0.5f;
            posY = Mathf.Pow(posX, 3) + 3;

            wayPoints.Add(new Vector2(posX, posY));
        }
    }

    void ThirdTrajectory()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;

        while (true)
        {
            if (posX > maxX || posX < minX || posY < minY || posY > maxY)
            {
                break;
            }

            posX = posX + 0.5f;
            posY = Mathf.Pow((posX - 1), 4) + 5 * Mathf.Pow(posX, 3) + 8 * Mathf.Pow(posX, 2) + 3 * posX;

            wayPoints.Add(new Vector2(posX, posY));
        }
    }

    void FourthTrajectory()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;

        while (true)
        {
            if (posX > maxX || posX < minX || posY < minY || posY > maxY)
            {
                break;
            }

            posX = posX + 0.5f;
            posY = Mathf.Sqrt(Mathf.Abs(posX)) + 1;

            wayPoints.Add(new Vector2(posX, posY));
        }
    }

    void PlaneDestroy()
    {
        Destroy(this.gameObject);
    }

    void PlaneSpawn()
    {
        transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    void MovePlane()
    {
        if (wayPoints.Count > 0)
        {
            if (Vector2.Distance(wayPoints[wayPointID], transform.position) <= 0)
            {
                if(wayPointID < wayPoints.Count - 1)
                {
                    wayPointID++;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointID], speed);
        }
    }

    void GetMaxMinPositionXY()
    {
        minPos = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxPos = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        minX = minPos.x + 2.5f * transform.localScale.x;
        minY = minPos.y + transform.localScale.y;
        maxX = maxPos.x - 2.5f * transform.localScale.x;
        maxY = maxPos.y - 1.5f * transform.localScale.y;
    }


    IEnumerator CheckBoards()
    {
        while (true)
        {
            yield return null;
            if (transform.position.x > maxX || transform.position.x < minX || transform.position.y > maxY || transform.position.y < minY)
            {
                PlaneDestroy();
                Debug.Log("Plane Lost");
                yield break;
            }
        }
    }
}
