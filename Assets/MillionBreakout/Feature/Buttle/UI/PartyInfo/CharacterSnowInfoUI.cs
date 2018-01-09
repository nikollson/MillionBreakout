using System;
using UnityEngine;
using System.Collections;
using MillionBreakout;
using UnityEngine.UI;

public class CharacterSnowInfoUI : MonoBehaviour
{

    public Image MaxCharaImage;
    public Image NormalCharaImage;

    public Image TouchPanel;

    public Slider SnowTowerSlider;

    public Image[] StockImages;

    public Action OnSkillPanelTouch;

    private CharacterSetting _characterSetting;
    private int _characterIndex;

    public void Initialize(CharacterSetting setting, int characterIndex)
    {
        _characterSetting = setting;
        _characterIndex = characterIndex;

        NormalCharaImage.sprite = setting.Images.NormalPose;
        MaxCharaImage.sprite = setting.Images.MaxPose;

        foreach (var image in StockImages)
        {
            image.sprite = setting.Images.SkillBall;
        }

        SetSnow(0, 0);
    }

    public void SetSnow(int stockNum, float chargePer)
    {
        if (stockNum > 0)
        {
            MaxCharaImage.gameObject.SetActive(true);
            NormalCharaImage.gameObject.SetActive(false);

            TouchPanel.gameObject.SetActive(true);
        }
        else
        {
            MaxCharaImage.gameObject.SetActive(false);
            NormalCharaImage.gameObject.SetActive(true);

            TouchPanel.gameObject.SetActive(false);
        }

        for (int i = 0; i < StockImages.Length; i++)
        {
            StockImages[i].gameObject.SetActive(i < stockNum);
        }

        SnowTowerSlider.value = chargePer;
    }

    public void SkillPanelTouched()
    {
        var chara = ButtleSystem.Party.Characters[_characterIndex];

        int level = chara.Level;
        int cost = chara.NextSkillCost();

        chara.UseSkill();


        ButtleSystem.SkillManager.StartSkill(_characterSetting.Buttle.Skill, level, cost);
    }
}
