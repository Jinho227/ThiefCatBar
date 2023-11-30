using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable/Ingredientdata", fileName = "Ingredientdata")]
public class IngredientData : ScriptableObject
{
    public int timeToSteal;
    public int weightClass;
    public string name;
}
