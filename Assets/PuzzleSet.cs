using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PuzzleSet : MonoBehaviour, IDropHandler
{
    public Vector2Int SetPos { get; private set; }

    public PuzzlePiece SetPiece;

    public void OnDrop(PointerEventData eventData)
    {
        if (SetPiece != null) return;

        var piece = PuzzlePiece.movePiece;

        SetPiece = piece;
        piece.transform.position = transform.position;
        piece.transform.SetAsFirstSibling();
        piece.AddSet(this);
    }
}
