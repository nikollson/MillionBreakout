using UnityEngine;
using UnityEngine.UI;

public class UIParameterSetter : MonoBehaviour
{

    [SerializeField] private Text _coinText;
    [SerializeField] private Slider _hpSlider;

    void Update()
    {
        var parameter = ButtleSystem.Instance.ParameterManager;
        _coinText.text = "" + parameter.Coin;
        _hpSlider.value = parameter.HP / parameter.HPMax;
    }
}
