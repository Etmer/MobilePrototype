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

    public Ingredient()
    {
    }

    public static SuperIngredient operator +(Ingredient lhs, Ingredient rhs)
    {
        Debug.Log("Created new SuperIngredient");
        return new SuperIngredient();
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
}
