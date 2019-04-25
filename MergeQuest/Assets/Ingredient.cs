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

    public Ingredient  Combine(Ingredient target)
    {
        IngredientType newType = CombinedIngredientType( target.ingredientType);
        return new Ingredient(newType);
    }
    
    public Ingredient PickUp()
    {
        ChangeState(IngredientStates.Dangling);
        return this;
    }

    public void Release(Vector3 closestPosition)
    {
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

    private IngredientType CombinedIngredientType(IngredientType target)
    {
        Debug.Log(target + " " + ingredientType);
        switch (ingredientType)
        {
            case IngredientType.Volunteer:
                if (target == IngredientType.Poison)
                {
                    return IngredientType.Zombie;
                }
                break;
            case IngredientType.Poison:
                if (target == IngredientType.Volunteer)
                {
                    return IngredientType.Zombie;
                }
                break;
            case IngredientType.Zombie:
                    return IngredientType.Zombie;
            default:
                throw new System.Exception();
        }
        return ingredientType;
    }
}
