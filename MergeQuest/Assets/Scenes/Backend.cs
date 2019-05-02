using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backend
{
    [SerializeField] private Dictionary<string, IngredientData> _ingredientData = new Dictionary<string, IngredientData>();

    public Sprite GetSprite(int index)
    {
        string Id = CSVParser.instance.combinations[index];
        return _ingredientData[Id].IngredientSprite;
    }
    public Sprite GetSprite(string Id)
    {
        return _ingredientData[Id].IngredientSprite;
    }

    public IngredientData GetData(int index)
    {
        string Id = CSVParser.instance.combinations[index];
        return _ingredientData[Id];
    }

    public Backend()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("SpriteAtlas");

        for (int i = 0; i < sprites.Length; i++)
        {
            IngredientData data = ScriptableObject.CreateInstance<IngredientData>();
            string Id = string.Format("ID-{0}", (i).ToString("D2"));
            data.Init(Id, (IngredientType)(i), sprites[i]);
            _ingredientData.Add(Id, data);
        }
    }
}
