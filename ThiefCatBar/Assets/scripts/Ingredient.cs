using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public int timeToSteal { get; private set; }
    public int weightClass { get; private set; }
    public string name { get; private set; }
    public IngredientData ingredientData;

    public void Setup()
    {
        timeToSteal = ingredientData.timeToSteal;
        weightClass = ingredientData.weightClass;
        name = ingredientData.name;
    }
}
