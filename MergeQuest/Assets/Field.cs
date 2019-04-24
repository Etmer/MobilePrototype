using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Field 
{
    private Vector2 _screenPosition;
    private Ingredient _ingredient;

    public Field(Vector2 position)
    {
        _screenPosition = position;
    }

    public Vector2 GetScreenPosition { get { return _screenPosition; } }
    public Ingredient GetIngredient { get { return _ingredient; } }
    public void SetIngredient(Ingredient newIngredient)
    {
        _ingredient = newIngredient;
    }
    public bool HasContent { get { return _ingredient != null; } }
    public void DecoupleIngredient()
    {
        _ingredient = null;
    }
}
