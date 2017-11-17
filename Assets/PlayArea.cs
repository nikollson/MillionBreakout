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
        touchPos.z = 10;
        var camera = ButtleSystem.Instance.Camera;
        var movePos = camera.ScreenToWorldPoint(touchPos);

        ButtleSystem.Instance.PlayerBar.SetMovePosition_Touch(movePos);
    }
}
