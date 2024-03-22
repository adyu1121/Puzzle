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
            Touch touchZero = Input.GetTouch(0); //ù��° �հ��� ��ġ�� ����
            Touch touchOne = Input.GetTouch(1); //�ι�° �հ��� ��ġ�� ����

            //��ġ�� ���� ���� ��ġ���� ���� ������
            //ó�� ��ġ�� ��ġ(touchZero.position)���� ���� �����ӿ����� ��ġ ��ġ�� �̹� �����ӿ��� ��ġ ��ġ�� ���̸� ��
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; //deltaPosition�� �̵����� ������ �� ���
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // �� �����ӿ��� ��ġ ������ ���� �Ÿ� ����
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude�� �� ������ �Ÿ� ��(����)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // �Ÿ� ���� ����(�Ÿ��� �������� ũ��(���̳ʽ��� ������)�հ����� ���� ����_���� ����)
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
