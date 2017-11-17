using UnityEngine;
using System.Collections;

public class PlayArea : MonoBehaviour {

    public void Play()
    {
        Vector3 touchPos = Input.mousePosition;
        if(Input.touchCount!=0)
        {
            touchPos = Input.touches[0].position;
        }
        var camera = ButtleSystem.Instance.Camera;
        touchPos.z = -camera.transform.position.z;
        var movePos = camera.ScreenToWorldPoint(touchPos);

        ButtleSystem.Instance.PlayerBar.SetMovePosition_Touch(movePos);
    }
}
