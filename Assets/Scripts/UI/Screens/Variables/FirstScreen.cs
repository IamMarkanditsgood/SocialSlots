using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstScreen : BasicScreen
{
    public Button play;

    private void Start()
    {
        play.onClick.AddListener(PlayGame);
    }

    private void OnDestroy()
    {
        play.onClick.RemoveListener(PlayGame);

    }
    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
    }

    private void PlayGame()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}
