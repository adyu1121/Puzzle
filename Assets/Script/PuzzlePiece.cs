using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static PuzzlePiece movePiece = null;

    private CanvasGroup canvasGroup;
    private PuzzleSet[] pieceSet = null;
    private static List<PuzzlePiece> pieces = new();

    public Vector2Int[] Size; 
    public Color ColorCode;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        pieces.Add(this);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        pieces.ForEach(piece => piece.PieceMoveIn());

        movePiece = this;
        transform.SetAsLastSibling();
        OutPiece();

        pieceSet = null;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        pieces.ForEach(x => x.PieceMoveOut());

        movePiece = null;
        transform.SetAsFirstSibling();

        if (pieceSet == null)
        {
            transform.position = Puzzle.instance.nullPos.transform.position;
        }
    }
    public void SetPiece(PuzzleSet[] sets, Vector2 pos)
    {
        pieceSet = sets;
        transform.position = pos;
        transform.SetAsFirstSibling();
        foreach(var set in sets)
        {
            set.SetPiece = this;
        }
    }
    public void OutPiece()
    {
        if (pieceSet == null) return;
        foreach (var set in pieceSet)
        {
            set.SetPiece = null;
        }
    }
    public void PieceMoveIn()
    {
        canvasGroup.blocksRaycasts = false;
    }
    public void PieceMoveOut()
    {
        canvasGroup.blocksRaycasts = true;
    }
}
