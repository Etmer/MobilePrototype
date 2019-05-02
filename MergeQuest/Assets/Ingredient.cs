using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientStates
{
    Idle,
    Dangling,
    Alligning

}

public class Ingredient
{
    private Vector3 _originPosition;

    private IngredientStates _currentState;

    public IngredientType ingredientType { get; private set; }

    public Ingredient(IngredientType typeOfIngredient)
    {
        ingredientType = typeOfIngredient;
        SpriteId = "ID-" + ((int)ingredientType).ToString("D2");
    }

    public Ingredient(IngredientData data)
    {
        ingredientType = data.IngredientDataType;
    }

    private void ChangeState(IngredientStates desiredState)
    {
        switch (GetState())
        {
            case IngredientStates.Idle:
                if (desiredState == IngredientStates.Dangling)
                {
                    _currentState = desiredState;
                }
                break;
            case IngredientStates.Dangling:
                if (desiredState == IngredientStates.Idle)
                {
                    _currentState = IngredientStates.Alligning;
                }
                if (desiredState == IngredientStates.Alligning)
                {
                    _currentState = desiredState;
                }
                break;
            case IngredientStates.Alligning:
                if (_currentState == IngredientStates.Dangling)
                {
                    _currentState = desiredState;
                }
                break;
        }
    }

    public IngredientStates GetState()
    {
        return _currentState;
    }
    
    public string SpriteId { get; private set; }
}
