using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CSVParser : MonoBehaviour
{
    [SerializeField] private TextAsset _csvTable;
    [SerializeField] private string[] _content;
    private void Start()
    {
        List<string> tempContent = new List<string>();

        string[] csvDataLines = _csvTable.text.Split('\n');

        //Lines start at index 2 because 0-1 is for the designer and irrelevant for csv parsing
        for (int i = 2; i < csvDataLines.Length; i++)
        {
            string[] row = csvDataLines[i].Split(';');
            var tempRow = row.Skip(2);
            foreach (var s in tempRow)
            {
                tempContent.Add(s);
            }
        }
        _content = tempContent.ToArray();
    }
}
