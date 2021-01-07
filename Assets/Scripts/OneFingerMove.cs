using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OneFingerMove : MonoBehaviour
{
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = transform as RectTransform;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        Move_PC();
#else
        Move();
#endif
    }

    private float moveCo = 3f;    //移动系数
    private Vector2 endValue = Vector2.zero;
    private void Move()
    {
        if (Input.touchCount != 1)
        {
            return;
        }

        Touch touch1 = Input.GetTouch(0);
        if (touch1.phase == TouchPhase.Moved)
        {
            endValue = rectTransform.anchoredPosition + touch1.deltaPosition * moveCo;
            float clampX = Mathf.Clamp(endValue.x, -rectTransform.sizeDelta.x * transform.localScale.x / 2, rectTransform.sizeDelta.x * transform.localScale.x / 2);
            float clampY = Mathf.Clamp(endValue.y, -rectTransform.sizeDelta.y * transform.localScale.y / 2, rectTransform.sizeDelta.y * transform.localScale.y / 2);
            endValue = new Vector2(clampX, clampY);
            rectTransform.DOAnchorPos(endValue, 0.3f);
        }
    }

    private Vector3 oldMousePos;
    private void Move_PC()
    {
        if (Input.GetMouseButtonDown(0))
        {
            oldMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 deltaPosition = Input.mousePosition - oldMousePos;
            endValue = rectTransform.anchoredPosition + new Vector2(deltaPosition.x, deltaPosition.y) * moveCo;
            float clampX = Mathf.Clamp(endValue.x, -rectTransform.sizeDelta.x * transform.localScale.x / 2, rectTransform.sizeDelta.x * transform.localScale.x / 2);
            float clampY = Mathf.Clamp(endValue.y, -rectTransform.sizeDelta.y * transform.localScale.y / 2, rectTransform.sizeDelta.y * transform.localScale.y / 2);
            endValue = new Vector2(clampX, clampY);
            rectTransform.DOAnchorPos(endValue, 0.3f);
            
            oldMousePos = Input.mousePosition;
        }
    }

    public void Reset()
    {
        endValue = Vector2.zero;
        rectTransform.DOAnchorPos(endValue, 0.3f);
    }
}
