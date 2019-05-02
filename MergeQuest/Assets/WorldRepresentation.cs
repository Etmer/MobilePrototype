using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldRepresentation
{
    [SerializeField] private Transform _worldTransform;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private int _index;

    public WorldRepresentation(Transform worldTransform, SpriteRenderer renderer, int index)
    {
        _worldTransform = worldTransform;
        _startPosition = _worldTransform.position;
        _renderer = renderer;
        _index = index;
    }

    public void PickUp()
    {
    }

    public void Move(Vector2 Position)
    {
        _worldTransform.position = Position;
    }

    public void ReturnToStart()
    {
        _worldTransform.position = _startPosition;
    }

    public void ChangeSprite(Sprite targetSprite)
    {
        _renderer.sprite = targetSprite;
    }

    public void DeleteSprite()
    {
        _renderer.sprite = null;
        ReturnToStart();
    }

    public Sprite GetSprite()
    {
        return _renderer.sprite;
    }

    public void Lift(bool state)
    {
        if (state)
        {
            _renderer.sortingOrder = 4;
        }
        else
        {
            _renderer.sortingOrder = 2;
        }
    }

    public int WorldIndex { get { return _index; } }
}
