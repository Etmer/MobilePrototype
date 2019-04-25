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
    public int index { get; private set; }

    private Vector3 _originPosition;

    private IngredientStates _currentState;

    public IngredientType ingredientType { get; private set; }

    public Ingredient(IngredientType typeOfIngredient)
    {
        ingredientType = typeOfIngredient;
        if (ingredientType == IngredientType.Volunteer || ingredientType == IngredientType.Poison)
        {
            index = 0;
        }
        else
        {
            index = 1;
        }
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
    
    public int SpriteIndex { get { return (int)ingredientType; } }
}
