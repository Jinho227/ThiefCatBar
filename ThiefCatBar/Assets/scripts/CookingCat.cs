using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class CookingCat : StaffCat
{
    public int speed;
    public bool isProcessing = false;
    public Customer customer;
    public bool isOrdering = false;
    public Storage storage;
    public GameObject cookedFood;

    protected override void Start()
    {
        mySpriteRenderer.sprite = startSprite;
        originalScale = transform.localScale;
        StartCoroutine(IsArrivedCustomer());
        
    }

    protected override void OnMouseDown()
    {
        if (isHovering && !isPressed)
        {
            Debug.Log("¹öÆ° Å¬¸¯µÊ!");
            transform.localScale = originalScale;
            isPressed = true;
            mySpriteRenderer.sprite = changeSprite;
            StartCoroutine(customer.GoToBar());
        }
    }

    IEnumerator IsArrivedCustomer()
    {
        while (true)
        {
            if (isPressed && customer.hasArrivedAtBar && !isOrdering && !isProcessing)
            {
                StartCoroutine(GoToTable());
                isProcessing = true;
            }
            yield return null;
        }
    }

    IEnumerator GoToTable()
    {
        int targetWayPointIndex = 1;
        Vector2 targetWayPoint = positionPoint[targetWayPointIndex];
        while ((Vector2)transform.position != targetWayPoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        ReceiveOrder();
        yield break;
    }

    void ReceiveOrder()
    {
        if (!isOrdering)
        {
            isOrdering = true;
            if(storage.storedItems.Find(item => item.name == "potato"))
            {
                StartCoroutine(GoToStorage());
            }
            else
            {
                GameManager.instance.AddReputation(-1);
                StartCoroutine(SendCustomerAway());
            }
        }
    }

    IEnumerator GoToStorage()
    {
        int targetWayPointIndex = 2;
        Vector2 targetWayPoint = positionPoint[targetWayPointIndex];
        while ((Vector2)transform.position != targetWayPoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        TakeIngredient();
        yield break;
    }

    void TakeIngredient()
    {
        Ingredient ingredient = storage.storedItems.Find(item => item.name == "potato");
        if (ingredient != null)
        {
            ingredient.transform.SetParent(transform);
            ingredient.transform.localPosition = new Vector3(0, 0);
            StartCoroutine(GoToStove(ingredient));
        }
    }

    IEnumerator GoToStove(Ingredient ingredient)
    {
        int targetWayPointIndex = 3;
        Vector2 targetWayPoint = positionPoint[targetWayPointIndex];
        while ((Vector2)transform.position != targetWayPoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(Cooking(ingredient));
        yield break;
    }

    IEnumerator Cooking(Ingredient ingredient)
    {
        yield return new WaitForSeconds(3f);
        GameObject food = Instantiate(cookedFood, transform) as GameObject;
        
        Destroy(ingredient.gameObject);

        StartCoroutine(ServeFood(food));
        yield break;
    }

    IEnumerator ServeFood(GameObject food)
    {
        int targetWayPointIndex = 1;
        Vector2 targetWayPoint = positionPoint[targetWayPointIndex];
        while ((Vector2)transform.position != targetWayPoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(food);
        GameManager.instance.AddScore(10);
        GameManager.instance.AddReputation(1);
        StartCoroutine(SendCustomerAway());
        yield break;
    }

    IEnumerator SendCustomerAway()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(customer.ExitBar());
        isProcessing = false;
        isOrdering = false;
        yield break;
    }
}
