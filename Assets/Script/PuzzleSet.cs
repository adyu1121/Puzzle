using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PuzzleSet : MonoBehaviour, IDropHandler
{
    public Vector2Int SetPos;
    public PuzzlePiece SetPiece;

    private void Start()
    {
        Puzzle.instance.PuzzleSets.Add(this);
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (SetPiece != null) return;
        if (PuzzlePiece.movePiece == null) return;
        var piece = PuzzlePiece.movePiece;

        PuzzleSet[] puzzleSets = new PuzzleSet[piece.Size.Length];
        for (int i = 0;i < puzzleSets.Length; i++)
        {
            PuzzleSet posSet = Puzzle.instance.GetPuzzleSet(SetPos + piece.Size[i]);
            if (posSet != null && posSet.SetPiece == null)
            {
                puzzleSets[i] = posSet;
            }
            else return;
        }

        PuzzlePiece.movePiece.SetPiece(puzzleSets, transform.position);
        Puzzle.instance.SetPiece();
    }
}
