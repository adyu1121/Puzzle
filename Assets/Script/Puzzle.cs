using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static Puzzle instance { get; private set; }

    public List<PuzzleSet> PuzzleSets = new();
    public Transform nullPos;

    private Vector2 DragStartPos;
    private Vector2 LastDragpos;

    private void Awake()
    {
        instance = this;
    }
    public void SetPiece()
    {
        for (int i = 0;i < 2;i++)
        {
            for (int j = 0; j < 2; j++)
            {
                PuzzleSet set = GetPuzzleSet(i, j);
                if (set == null) return;
                if(set.SetPiece == null) return;
                if (set.SetPiece.ColorCode == new Color(1, 1, 1)) return;
            }
        }

        Debug.Log("Clear");
    }

    public PuzzleSet GetPuzzleSet(int x,int y)
    {
        var pos = new Vector2Int(x, y);
        return GetPuzzleSet(pos);
    }
    public PuzzleSet GetPuzzleSet(Vector2Int pos)
    {
        foreach (var pizzle in PuzzleSets)
        {
            if (pizzle.SetPos == pos)
            {
                return pizzle;
            }
        }
        return null;
    }

    private void Update()
    {
#if UNITY_STANDALONE_WIN
        float wheel = Input.GetAxisRaw("Mouse ScrollWheel");
        transform.localScale += new Vector3(wheel, wheel);
#elif UNITY_ANDROID
#elif UNITY_IOS
#endif
        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0); //첫번째 손가락 터치를 저장
            Touch touchOne = Input.GetTouch(1); //두번째 손가락 터치를 저장

            //터치에 대한 이전 위치값을 각각 저장함
            //처음 터치한 위치(touchZero.position)에서 이전 프레임에서의 터치 위치와 이번 프로임에서 터치 위치의 차이를 뺌
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; //deltaPosition는 이동방향 추적할 때 사용
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 각 프레임에서 터치 사이의 벡터 거리 구함
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude는 두 점간의 거리 비교(벡터)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 거리 차이 구함(거리가 이전보다 크면(마이너스가 나오면)손가락을 벌린 상태_줌인 상태)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            transform.localScale += new Vector3(deltaMagnitudeDiff, deltaMagnitudeDiff);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition = eventData.position - DragStartPos + LastDragpos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragStartPos = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        LastDragpos = transform.localPosition;
    }
}
