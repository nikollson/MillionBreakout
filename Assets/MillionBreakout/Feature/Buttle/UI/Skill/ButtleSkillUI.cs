using UnityEngine;
using System.Collections;
using MillionBreakout;
using UnityEngine.UI;

public class ButtleSkillUI : MonoBehaviour
{
    public Image TouchPanel;
    public Text SkillCountText;
    public Animator Animator;
    public GameObject SkillInfo;

    public GameObject TapSkillDiscription;
    public GameObject SwipeSkillDiscription;

    private ButtleSkillBase _callBack;
    private bool _animationMoving = false;

    public void Awake()
    {
        TouchPanel.gameObject.SetActive(false);
        SkillInfo.SetActive(false);
    }

    public void StartSkill(ButtleSkillBase callBack)
    {
        _callBack = callBack;

        TouchPanel.gameObject.SetActive(true);
        SkillInfo.SetActive(true);

        StartAnimation();
        SetControllMode(callBack.ControllMode);

        SkillCountText.text = "" + _callBack.GetSkillCount();
    }

    public void SetControllMode(ButtleSkillBase.ControllModeType controllMode)
    {
        TapSkillDiscription.SetActive(controllMode == ButtleSkillBase.ControllModeType.Tap);
        SwipeSkillDiscription.SetActive(controllMode == ButtleSkillBase.ControllModeType.Swipe);
    }

    public void EndSkill()
    {
        _callBack = null;
        TouchPanel.gameObject.SetActive(false);
        SkillInfo.SetActive(false);
        
        if(_animationMoving)
            StopAnimation();

        SkillCountText.text = "";
    }

    public void StartAnimation()
    {
        _animationMoving = true;
        Animator.SetBool("StartSkill", true);
        Animator.SetBool("EndSkill", false);
    }

    public void StopAnimation()
    {
        _animationMoving = false;
        Animator.SetBool("StartSkill", false);
        Animator.SetBool("EndSkill", true);
    }
    

    public void OnPointerDown()
    {
        if(_callBack==null)
            return;

        if (_animationMoving)
        {
            StopAnimation();
        }
        ;
        _callBack.OnPointerDown(GetPointerPosition());
        SkillCountText.text = "" + _callBack.GetSkillCount();
    }

    public void OnPointerUp()
    {
        if (_callBack == null)
            return;

        if (_animationMoving)
        {
            StopAnimation();
        }

        _callBack.OnPointerUp(GetPointerPosition());
        SkillCountText.text = "" + _callBack.GetSkillCount();
    }

    public void OnPointerMove()
    {
        if (_callBack == null)
            return;

        if (_animationMoving)
        {
            StopAnimation();
        }

        _callBack.OnPointerMove(GetPointerPosition());
        SkillCountText.text = "" + _callBack.GetSkillCount();
    }

    public Vector2 GetPointerPosition()
    {
        var pos = Input.mousePosition;

        if (Input.touchCount != 0)
        {
            pos = Input.touches[0].position;
        }

        pos.z = -ButtleSystem.Camera.transform.position.z;

        var ret = ButtleSystem.Camera.ScreenToWorldPoint(pos);
        return ret;
    }
}
