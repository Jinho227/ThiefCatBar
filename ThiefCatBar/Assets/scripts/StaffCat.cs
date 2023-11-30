using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffCat : MonoBehaviour
{
    protected bool isHovering = false;
    protected bool isPressed = false;
    protected Vector3 originalScale;
    protected List<Vector3> positionPoint;

    protected SpriteRenderer mySpriteRenderer;
    public Sprite changeSprite;
    public Sprite startSprite;

    protected void Awake()
    {
        positionPoint = new List<Vector3>();
        for (int i = 0; i < transform.childCount; i++)
        {
            positionPoint.Add(transform.GetChild(i).position);
        }
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        mySpriteRenderer.sprite = startSprite;
        originalScale = transform.localScale;
    }

    protected void OnMouseEnter()
    {
        if (!isPressed)
        {
            isHovering = true;
            transform.localScale = originalScale * 1.1f;
        }
    }

    protected void OnMouseExit()
    {
        if (!isPressed)
        {
            isHovering = false;
            transform.localScale = originalScale;
        }
    }

    protected virtual void OnMouseDown()
    {
        if (isHovering && !isPressed)
        {
            Debug.Log("¹öÆ° Å¬¸¯µÊ!");
            transform.localScale = originalScale;
            isPressed = true;
            mySpriteRenderer.sprite = changeSprite;
        }
    }
}
