using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterStatusInfoUI : MonoBehaviour
{

    public Text[] LevelText;

    public Slider HpSlider;

    public void SetLevelText(int num)
    {
        string str = "" + num;

        foreach (var text in LevelText)
        {
            text.text = str;
        }
    }

    public void SetHpPer(float per)
    {
        HpSlider.value = per;
    }
}
