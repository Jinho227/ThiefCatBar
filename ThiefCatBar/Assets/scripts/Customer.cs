using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    Transform pathHolder; // 경로 포인트를 담고 있는 GameObject
    GameObject orderButton;
    Vector2[] waypoints;


    public float speed = 5;

    public bool isFoodReceived = false;
    public bool hasArrivedAtBar = false;

    public struct SteamedPotato
    {
        public int cookingTime;
        public List<string> ingredients;
    }


    private void Awake()
    {
        pathHolder = GetComponent<Transform>();
    }

    void Start()
    {
        Transform orderTransform = transform.Find("Order");
        orderButton = orderTransform.gameObject;
        orderButton.SetActive(false);
        waypoints = new Vector2[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
        }
        transform.position = waypoints[0];
    }
    public IEnumerator GoToBar()
    {
        int targetWaypointIndex = 1;
        Vector2 targetWaypoint = waypoints[targetWaypointIndex];
        while ((Vector2)transform.position != targetWaypoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        hasArrivedAtBar = true;
        CreateOrder();
    }

    void CreateOrder()
    {

        orderButton.SetActive(true);
    }

    public IEnumerator ExitBar()
    {
        orderButton.SetActive(false);
        hasArrivedAtBar = false;
        int targetWaypointIndex = 0;
        Vector2 targetWaypoint = waypoints[targetWaypointIndex];
        while ((Vector2)transform.position != targetWaypoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(GoToBar());
    }
}
