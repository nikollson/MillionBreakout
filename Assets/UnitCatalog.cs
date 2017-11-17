using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitCatalog : MonoBehaviour
{
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private RectTransform _makeLineTransform;
    [SerializeField] private RectTransform _paperTransform;

    private bool _catching = false;
    private Vector3 _startPaperPosition;

    void Awake()
    {
        _startPaperPosition = _paperTransform.position;
    }

    public void OnCatch()
    {
        _catching = true;
    }

    public void OnRelease()
    {
        Vector3 currentPosition = _paperTransform.position;
        _catching = false;
        _paperTransform.position = _startPaperPosition;

        if (currentPosition.y >= _makeLineTransform.position.y)
        {
            var camera = ButtleSystem.Instance.Camera;
            Vector3 touchPosition = GetTouchPosition();
            touchPosition.z = -camera.transform.position.z;
            var worldPosition = camera.ScreenToWorldPoint(touchPosition);
            ButtleSystem.Instance.UnitMaker.MakeUnit(_unitPrefab,worldPosition);
        }
    }

    void Update()
    {
        if (_catching)
        {
            _paperTransform.position = GetTouchPosition();
        }
    }

    private Vector3 GetTouchPosition()
    {
        Vector3 pos = Input.mousePosition;
        if (Input.touchCount != 0)
        {
            pos = Input.touches[0].position;
        }
        return pos;
    }
}
