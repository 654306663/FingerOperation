using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TwoFingerZoom : MonoBehaviour
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
        Zoom_PC();
#else
        Zoom();
#endif
    }

    private float currentZoomScale = 1;
    private float zoomScaleMax = 2;
    private float zoomScaleMin = 0.3f;
    private float zoomScaleCo = 0.003f;   //缩放系数
    Touch oldTouch1;  //上次触摸点1(手指1)
	Touch oldTouch2;  //上次触摸点2(手指2)
    private void Zoom()
    {
		if (Input.touchCount <= 1) {
			return;
		}

		Touch touch1 = Input.GetTouch(0);
		Touch touch2 = Input.GetTouch(1);
 
		//启用双指，尚未旋转
		if (touch2.phase == TouchPhase.Began) {
			oldTouch2 = touch2;
			oldTouch1 = touch1;
			return;
		}
		if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved) {
            float oldDistance = Vector2.Distance(oldTouch2.position, oldTouch1.position);
            float curDistance = Vector2.Distance(touch2.position, touch1.position);
            currentZoomScale += (curDistance - oldDistance) * zoomScaleCo;
            currentZoomScale = Mathf.Clamp(currentZoomScale, zoomScaleMin, zoomScaleMax);
            transform.DOScale(Vector3.one * currentZoomScale, 0.3f);
 
            float clampX = Mathf.Clamp(rectTransform.anchoredPosition.x, -rectTransform.sizeDelta.x * transform.localScale.x / 2, rectTransform.sizeDelta.x * transform.localScale.x / 2);
            float clampY = Mathf.Clamp(rectTransform.anchoredPosition.y, -rectTransform.sizeDelta.y * transform.localScale.y / 2, rectTransform.sizeDelta.y * transform.localScale.y / 2);
            Vector2 endValue = new Vector2(clampX, clampY);
            rectTransform.DOAnchorPos(endValue, 0.3f);

			oldTouch1 = touch1;
			oldTouch2 = touch2;
		}
    }

    private void Zoom_PC()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            currentZoomScale -= 0.3f;
            currentZoomScale = Mathf.Clamp(currentZoomScale, zoomScaleMin, zoomScaleMax);
            transform.DOScale(Vector3.one * currentZoomScale, 0.3f);
            
            float clampX = Mathf.Clamp(rectTransform.anchoredPosition.x, -rectTransform.sizeDelta.x * transform.localScale.x / 2, rectTransform.sizeDelta.x * transform.localScale.x / 2);
            float clampY = Mathf.Clamp(rectTransform.anchoredPosition.y, -rectTransform.sizeDelta.y * transform.localScale.y / 2, rectTransform.sizeDelta.y * transform.localScale.y / 2);
            Vector2 endValue = new Vector2(clampX, clampY);
            rectTransform.DOAnchorPos(endValue, 0.3f);
        }
	    if (Input.GetAxis("Mouse ScrollWheel") > 0)
	    {
            currentZoomScale += 0.3f;
            currentZoomScale = Mathf.Clamp(currentZoomScale, zoomScaleMin, zoomScaleMax);
            transform.DOScale(Vector3.one * currentZoomScale, 0.3f);
            
            float clampX = Mathf.Clamp(rectTransform.anchoredPosition.x, -rectTransform.sizeDelta.x * transform.localScale.x / 2, rectTransform.sizeDelta.x * transform.localScale.x / 2);
            float clampY = Mathf.Clamp(rectTransform.anchoredPosition.y, -rectTransform.sizeDelta.y * transform.localScale.y / 2, rectTransform.sizeDelta.y * transform.localScale.y / 2);
            Vector2 endValue = new Vector2(clampX, clampY);
            rectTransform.DOAnchorPos(endValue, 0.3f);
	    }

    }
    
    public void Reset() {
        currentZoomScale = 1;
        transform.DOScale(Vector3.one * currentZoomScale, 0.3f);
    }
}
