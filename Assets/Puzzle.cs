using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public static Puzzle instance { get; private set; }
    private List<PuzzleSet> puzzleSets = new();

    public Transform nullPos;

    private void Awake()
    {
        instance = this;
    }
    public PuzzleSet GetPuzzleSet(int x,int y)
    {
        var pos = new Vector2Int(x, y);
        return GetPuzzleSet(pos);
    }
    public PuzzleSet GetPuzzleSet(Vector2Int pos)
    {
        foreach (var pizzle in puzzleSets)
        {
            if (pizzle.SetPos == pos)
            {
                return pizzle;
            }
        }
        return null;
    }

}
