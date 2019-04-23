using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataModel 
{
    private Dictionary<int, Field> _gameFields = new Dictionary<int, Field>();

    public DataModel(Dictionary<int, Field> fields)
    {
        _gameFields = fields;
    }

    public Field GetField(int index)
    {
        if (index >= 0 && index < _gameFields.Count)
        {
            return _gameFields[index];
        }
        return null;

    }

}
