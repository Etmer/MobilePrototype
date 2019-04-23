using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private DataModel _model;
    [SerializeField] private Sprite _fieldSprite;
    [SerializeField] private Transform[] _fieldPool;
    private Vector3 _mousePosition;
    private Vector3 _worldPosition;
    private WorldRepresentation _currentWorldRepresentation;
    private int _currentIndex;
    private Ingredient _currentIngredient;
    [SerializeField] private List<WorldRepresentation> _worldSlots = new List<WorldRepresentation>();
    [SerializeField] private Transform _prefab;
    [SerializeField] private Rect _playArea;

    [SerializeField] private Texture _text;

    void Start()
    {
        _model = new DataModel(CreateField());
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMousePosition();
        _worldPosition = Camera.main.ScreenToWorldPoint(_mousePosition);
        _currentIndex = VectorToIndex(_worldPosition - Vector3.zero);
        Debug.Log(_currentIndex);
        if (_playArea.Contains(new Vector2(_worldPosition.x, _worldPosition.y)))
        {
            if (Input.GetMouseButtonDown(0) && _model.GetField(_currentIndex).HasContent)
            {
                _currentIngredient = _model.GetField(_currentIndex).GetIngredient.PickUp();
                _currentWorldRepresentation = _worldSlots[_currentIndex];
                _model.GetField(_currentIndex).DecoupleIngredient();
            }
            if (_currentIngredient != null)
            {
                if (Input.GetMouseButton(0))
                {
                    _currentWorldRepresentation.Move(_worldPosition);
                }
                else
                {
                    if (_model.GetField(_currentIndex).HasContent)
                    {
                        SuperIngredient SI = _currentIngredient + _model.GetField(_currentIndex).GetIngredient;
                        _model.GetField(_currentIndex).SetIngredient(SI);
                        //_currentIngredient.Release(_model.GetField(_currentIndex).GetScreenPosition);
                    }
                    else
                    {
                        _worldSlots[_currentIndex].ChangeSprite(_currentWorldRepresentation.GetSprite());
                        _currentWorldRepresentation.DeleteSprite();
                        _model.GetField(_currentIndex).SetIngredient(_currentIngredient);
                    }
                    _currentIngredient = null;
                }
            }
        }
        else
        {
            if (_currentWorldRepresentation != null)
            {
                _currentWorldRepresentation.ReturnToStart();
            }
        }
    }

    private Vector2 CalculateMousePosition()
    {
        _mousePosition.x = Input.mousePosition.x;
        _mousePosition.y = Input.mousePosition.y;
        _mousePosition.z = 5;
        return _mousePosition;
    }

    private Dictionary<int, Field> CreateField()
    {
        Dictionary<int, Field> map = new Dictionary<int, Field>();
        Vector3 startPosition = Vector3.zero;
        Vector3 FieldPosition = startPosition;
        int index = 0;
        int mapIndex = 0;

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                FieldPosition = startPosition + Vector3.right * _fieldSprite.bounds.size.x * x + Vector3.up * _fieldSprite.bounds.size.y * y;
                _fieldPool[index].position = FieldPosition;
                mapIndex = VectorToIndex(FieldPosition - startPosition);
                Field tempField = new Field(FieldPosition);
                Ingredient tempIngredient = null;
                if (x == 0 && y == 0)
                { 
                tempIngredient = new Ingredient();
                }
                _worldSlots.Add(new WorldRepresentation(_prefab.GetChild(x+(y*3)).GetChild(0).transform, _prefab.GetChild(x + (y * 3)).GetChild(0).GetComponent<SpriteRenderer>()));
                tempField.SetIngredient(tempIngredient);
                map.Add(mapIndex, tempField);
                index++;
            }
        }
        _playArea.Set(startPosition.x - _fieldSprite.bounds.size.x/2, startPosition.y - _fieldSprite.bounds.size.y/2, 3 * _fieldSprite.bounds.size.x, 3 * _fieldSprite.bounds.size.y);
        Debug.Log(_worldSlots.Count);
        return map;
    }

    private int VectorToIndex(Vector3 input)
    {
        int x = Mathf.RoundToInt(input.x / _fieldSprite.bounds.size.x);
        int y = Mathf.RoundToInt(input.y / _fieldSprite.bounds.size.y);
        return x + (y*3);
    }
}
