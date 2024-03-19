using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static PuzzlePiece movePiece = null;
    private CanvasGroup canvasGroup;

    private List<PuzzleSet> pieceSet = new();
    private static List<PuzzlePiece> pieces = new();
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        pieces.Add(this);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        movePiece = this;
        transform.SetAsLastSibling();
        pieces.ForEach(piece => piece.PieceMoveIn());
        pieceSet.ForEach(set=> set.SetPiece = null);
        pieceSet.Clear();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        movePiece = null;
        pieces.ForEach(x => x.PieceMoveOut());
        if (pieceSet.Count == 0)
        {
            transform.position = Puzzle.instance.nullPos.transform.position;
        }
    }
    public void AddSet(PuzzleSet set)
    {
        pieceSet.Add(set);
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
