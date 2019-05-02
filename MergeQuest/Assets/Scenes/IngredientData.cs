using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientData : ScriptableObject
{
    [SerializeField] public string Id { get; private set; }
    public IngredientType IngredientDataType { get; private set; }
    public Sprite IngredientSprite { get; private set; }

    public void Init(string id, IngredientType ingredientDataType, Sprite sprite)
    {
        Id = id;
        IngredientDataType = ingredientDataType;
        IngredientSprite = sprite;
    }
}
