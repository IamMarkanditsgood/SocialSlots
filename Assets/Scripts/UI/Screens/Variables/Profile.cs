using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : BasicScreen
{
    public Button H;
    public Button i;
    public TMP_InputField name;

    public Image[] achievementsImage;
    public Sprite[] openedAchievements;


    void Start()
    {
        H.onClick.AddListener(Home);
        i.onClick.AddListener(Info);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        H.onClick.RemoveListener(Home);
        i.onClick.RemoveListener(Info);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("Name", name.text);
    }

    public override void SetScreen()
    {

        name.text = PlayerPrefs.GetString("Name", "USER_NAME");
        SetAchievements();
    }

    public override void ResetScreen()
    { 
    }

   

    private void SetAchievements()
    {
        for(int i = 0; i < achievementsImage.Length; i++)
        {
            string key = "Achieve" + i;
            if (PlayerPrefs.GetInt(key) == 1)
            {
                achievementsImage[i].sprite = openedAchievements[i];
            }
        }
    }

    private void Home()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
        PlayerPrefs.SetString("Name", name.text);
    }

    private void Info()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Info);
        PlayerPrefs.SetString("Name", name.text);
    }
}
