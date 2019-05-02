using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CSVParser : MonoBehaviour
{
    [SerializeField] private TextAsset _csvTable;
    [SerializeField] private string[] _content;
    public Dictionary<int,string> combinations = new Dictionary<int,string>();
    public static CSVParser instance;
    public static int lineLength;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        List<string> tempContent = new List<string>();
        
        string[] csvDataLines = _csvTable.text.Split('\n');
        string[] firstLine = csvDataLines[0].Split(';');

        //Lines start at index 1 because 1 is for the designer and irrelevant for csv parsing
        for (int i = 2; i < csvDataLines.Length; i++)
        {
            string[] row = csvDataLines[i].Split(';');
            for (int h = 2; h < row.Length; h++)
            {
                int x = i - 2;
                int y = h - 2;
                int index = x + (y * firstLine.Length);
                lineLength = firstLine.Length;
                if (row[h] != "none" && row[h] != "none\r")
                {
                    combinations.Add(index,row[h]);
                }
            }
        }
        _content = tempContent.ToArray();
    }
}
