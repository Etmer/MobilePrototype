using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private SpriteManager _spriteManager;
    [SerializeField] private Transform _parentGameObject;

    public Dictionary<int, Field> CreateField(ref MapData data)
    {
        Dictionary<int, Field> map = new Dictionary<int, Field>();
        Vector3 startPosition = _parentGameObject.transform.position;
        Vector3 FieldPosition = startPosition;
        int index = 0;
        int mapIndex = 0;

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                FieldPosition = startPosition + Vector3.right * GameMetrics.SpriteWidth * x + Vector3.up * GameMetrics.SpriteHeight * y + Vector3.forward/1000 * y;
                data.AddPosition( FieldPosition);
                mapIndex = GameMetrics.VectorToIndex(FieldPosition - startPosition);
                Field tempField = new Field(FieldPosition);
                Ingredient tempIngredient = CreateIngredient();
                _parentGameObject.GetChild(index).transform.position = FieldPosition;
                if (tempIngredient != null)
                {
                    _parentGameObject.GetChild(index).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = SetSprite(tempIngredient.ingredientType);
                }
                data.AddWorldRepresenation(new WorldRepresentation(_parentGameObject.GetChild(index).GetChild(0).transform, _parentGameObject.GetChild(index).GetChild(0).GetComponent<SpriteRenderer>(), index));
                tempField.SetIngredient(tempIngredient);
                map.Add(mapIndex, tempField);
                index++;
            }
        }
        data.ChangeRect(startPosition.x - GameMetrics.SpriteWidth / 2, startPosition.y - GameMetrics.SpriteHeight / 2, 3 * GameMetrics.SpriteWidth, 3 * GameMetrics.SpriteHeight);
        return map;
    }

    private Ingredient CreateIngredient()
    {
        int index = Random.Range(1, 10);

        switch (index)
        {
            case 1:
                return new Ingredient(IngredientType.Volunteer);
            case 2:
                return new Ingredient(IngredientType.Female);
            case 3:
                return new Ingredient(IngredientType.Poison);
            case 4:
                return new Ingredient(IngredientType.PotionOfElectricity);
            case 5:
                return new Ingredient(IngredientType.PotionOfLife);
            case 6:
                return new Ingredient(IngredientType.Water);
            default:
                return null;
        }
    }

    private Sprite SetSprite(IngredientType ingredientType)
    {
        return GameManager.instance.GameBackEnd.GetSprite("ID-" + (((int)ingredientType)).ToString("D2"));
    }
}
