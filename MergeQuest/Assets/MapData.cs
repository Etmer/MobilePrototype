using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{
    [SerializeField] private List<Vector3> _fieldPositions = new List<Vector3>();
    [SerializeField] private List<WorldRepresentation> _worldRepresentations = new List<WorldRepresentation>();
    private Rect _playableRect;

    //Positions

    public void AddPosition(Vector3 newPosition)
    {
        _fieldPositions.Add(newPosition);
    }

    public Vector3 GetPosition(int index)
    {
        return _fieldPositions[index];
    }

    //WorldRepresentation

    public void AddWorldRepresenation(WorldRepresentation newWorldRepresentation)
    {
        _worldRepresentations.Add(newWorldRepresentation);
    }

    public WorldRepresentation GetWorldRepresentation(int index)
    {
        return _worldRepresentations[index];
    }

    public void Clear()
    {
        _fieldPositions.Clear();
        _worldRepresentations.Clear();
        _playableRect = Rect.zero;
    }

    public void ChangeRect(float x, float y, float width, float height)
    {
        _playableRect.Set(x, y, width, height);
    }

    public int FieldCount { get { return _fieldPositions.Count; } }

    public Rect PlayeRect { get { return _playableRect; } }

    public WorldRepresentation[] WorldRepresentationsToArray { get { return _worldRepresentations.ToArray(); } }
}
