using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitCatalog : MonoBehaviour
{
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private int _costCoin;
    [SerializeField] private Sprite _unitSprite;
    [SerializeField] private Color _cantMakeColor;
    [SerializeField] private RectTransform _makeLineTransform;
    [SerializeField] private RectTransform _paperTransform;
    [SerializeField] private Text _coinText;
    [SerializeField] private Image _unitImage;
    [SerializeField] private Image _unitPaper;

    private bool _catching = false;
    private Vector3 _startPaperPosition;
    private Color _startPaperColor;

    void Awake()
    {
        _startPaperPosition = _paperTransform.position;
        _coinText.text = "" + _costCoin;
        _unitImage.sprite = _unitSprite;
        _startPaperColor = _unitPaper.color;
    }

    void Update()
    {
        if (_catching)
        {
            _paperTransform.position = GetTouchPosition();
        }

        int coin = ButtleSystem.Instance.ParameterManager.Coin;
        if (coin < _costCoin) _unitPaper.color = _cantMakeColor;
        else _unitPaper.color = _startPaperColor;
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

        bool areaIn = currentPosition.y >= _makeLineTransform.position.y;
        bool costOK = ButtleSystem.Instance.ParameterManager.Coin >= _costCoin;
        if (areaIn && costOK)
        {
            CallMakeUnit();
        }
    }

    private void CallMakeUnit()
    {
        var camera = ButtleSystem.Instance.Camera;
        Vector3 touchPosition = GetTouchPosition();
        touchPosition.z = -camera.transform.position.z;
        var worldPosition = camera.ScreenToWorldPoint(touchPosition);

        ButtleSystem.Instance.UnitMaker.MakeUnit(_unitPrefab, worldPosition);
        ButtleSystem.Instance.ParameterManager.CoinAdd(-_costCoin);
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
