using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoFingerRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
    
    private float rotateCo = 1f;    //旋转系数
    Touch oldTouch1;  //上次触摸点1(手指1)
	Touch oldTouch2;  //上次触摸点2(手指2)
    private void Rotate()
    {
		if (Input.touchCount <= 1) {
			return;
		}
 
		Touch touch1 = Input.GetTouch(0);
		Touch touch2 = Input.GetTouch(1);
 
		//启用双指，尚未旋转
		if (touch2.phase == TouchPhase.Began) {
			oldTouch1 = touch1;
			oldTouch2 = touch2;
			return;
		}
		if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved) {
			Vector2 curVec = touch2.position - touch1.position;
			Vector2 oldVec = oldTouch2.position - oldTouch1.position;
			float angle = Vector2.Angle(oldVec, curVec);
			angle *= Mathf.Sign(Vector3.Cross(oldVec, curVec).z);
 
			transform.Rotate(new Vector3(0, 0, angle) * rotateCo);
 
			oldTouch1 = touch1;
			oldTouch2 = touch2;
		}
    }
    
    public void Reset() {
        transform.localEulerAngles = Vector3.zero;
    }
}
