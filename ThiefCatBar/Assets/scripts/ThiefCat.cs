using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEditor.Progress;

public class ThiefCat : StaffCat
{
    public Ingredient ingredientPrefab;
    public Ingredient[] ingredients;

    public Transform ingredientCreatePoint;

    private Ingredient ingredient;

    public int speed;

    private bool isArrivedStorage = false;

    protected override void OnMouseDown()
    {
        if (isHovering && !isPressed)
        {
            Debug.Log("��ư Ŭ����!");
            transform.localScale = originalScale;
            isPressed = true;
            StartCoroutine(GoToSteal());
            mySpriteRenderer.sprite = changeSprite;
        }
    }

    IEnumerator GoToSteal()
    {
        int targetWayPointIndex = 1; // ������ ���� ��� �ε���
        Vector2 targetWayPoint = positionPoint[targetWayPointIndex];
        while ((Vector2)transform.position != targetWayPoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(StealIngredients());
        yield break;

    }

    IEnumerator StealIngredients() // � ��Ḧ �����ϰ� �� ���ð����� ��ٸ��� ����ҷ� �̵��ϴ� �Լ� ȣ��
    {
        ingredient = CreateIngredient();
        yield return new WaitForSeconds(ingredient.timeToSteal);
        Debug.Log("��ƿ ��");
        StartCoroutine(GoToStorage());
        yield break;
    }

    Ingredient CreateIngredient()
    {
        Ingredient tempingredient = ingredients[Random.Range(0, ingredients.Length)];

        ingredient = Instantiate(tempingredient, transform.position, ingredientCreatePoint.rotation);
        ingredient.Setup();
        ingredient.transform.SetParent(transform);
        return ingredient;
    }

    IEnumerator GoToStorage() // ����ҷ� ���� 1�ʵ��� ����ҿ� ����.
    {
        int targetWayPointIndex = 2;
        Vector2 targetWayPoint = positionPoint[targetWayPointIndex];
        while ((Vector2)transform.position != targetWayPoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            yield return null;
        }

        while (!isArrivedStorage)
        {
            yield return null;
        }
        isArrivedStorage = false;
        StartCoroutine(GoToSteal());
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Storage"))
        {
            StartCoroutine(AddIngredientToStorage(collision));
        }
    }

    IEnumerator AddIngredientToStorage(Collider2D storageCollider)
    {
        yield return new WaitForSeconds(1);

        Storage storage = storageCollider.GetComponent<Storage>();
        if (storage != null)
        {
            storage.StoreItem(ingredient);
        }
        isArrivedStorage = true;
        yield break;
    }
}
