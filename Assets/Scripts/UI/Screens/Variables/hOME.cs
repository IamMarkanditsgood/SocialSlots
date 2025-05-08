using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hOME : BasicScreen
{
    public Button profile;
    public Button leaders;
    public Button bonus;
    public Button game1;
    public Button game2;
    public Button game3;
    public Button game4;
    public Button game5;

    void Start()
    {
        profile.onClick.AddListener(Profile);
        leaders.onClick.AddListener(Leaders);
        bonus.onClick.AddListener(Bonuse);

        game1.onClick.AddListener(PlayGame1);
        game2.onClick.AddListener(PlayGame2);
        game3.onClick.AddListener(PlayGame3);
        game4.onClick.AddListener(PlayGame4);
        game5.onClick.AddListener(PlayGame5);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        profile.onClick.RemoveListener(Profile);
        leaders.onClick.RemoveListener(Leaders);
        bonus.onClick.RemoveListener(Bonuse);

        game1.onClick.RemoveListener(PlayGame1);
        game2.onClick.RemoveListener(PlayGame2);
        game3.onClick.RemoveListener(PlayGame3);
        game4.onClick.RemoveListener(PlayGame4);
        game5.onClick.RemoveListener(PlayGame5);
    }


    public override void SetScreen()
    {
    }

    public override void ResetScreen()
    {
    }
    private void Bonuse()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.DaylyBonus);
    }
    private void Leaders()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Leaders);
    }
    private void Profile()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }


    private void PlayGame1()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Game1);
    }
    private void PlayGame2()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Game2);
    }
    private void PlayGame3()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Game3);
    }
    private void PlayGame4()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Game4);
    }
    private void PlayGame5()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Game5);
    }
}
