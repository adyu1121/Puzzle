using System.Collections.Generic;
using UnityEngine;

public class CSVLoder : MonoBehaviour
{
    List<Dictionary<string, string>> Load(string path)
    {
        TextAsset text = Resources.Load<TextAsset>(path);
        List<Dictionary<string, string>> result = new();

        string[] lines = text.text.Split('\n');
        if (lines.Length <= 0) Debug.LogError("CSV if Empty");

        string[] keys = lines[0].Split(',');
        for(int i = 0; i < keys.Length;i++)
        {
            keys[i] = keys[i].Replace(" ", string.Empty);
            keys[i] = keys[i].Replace("\n", string.Empty);
            keys[i] = keys[i].Replace("\r", string.Empty);
        }

        for (int i = 1;i < lines.Length; i++)
        {
            var line = new Dictionary<string, string>();
            result.Add(line);
            string[] values = lines[i].Split(',');
            for (int j = 0;j < keys.Length;j++)
            {
                result[i - 1].Add(keys[j], values[j]
                    .Replace(" ", string.Empty)
                    .Replace("\n", string.Empty)
                    .Replace("\r", string.Empty));
            }
        }
        return result;
    }
}
