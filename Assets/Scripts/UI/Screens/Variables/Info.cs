using UnityEngine.UI;

public class Info : BasicScreen
{
    public Button H;


    void Start()
    {
        H.onClick.AddListener(Home);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        H.onClick.RemoveListener(Home);
    }


    public override void SetScreen()
    {
    }

    public override void ResetScreen()
    {
    }


    private void Home()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }

}
