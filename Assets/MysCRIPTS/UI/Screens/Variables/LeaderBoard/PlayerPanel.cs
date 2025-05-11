using TMPro;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    public TMP_Text _name;
    public TMP_Text _score;
    public TMP_Text _position;

    public void SetName(string name) => _name.text = name;
    public void SetScore(int score) => _name.text = score.ToString();
    public void SetPos(int pos) => _position.text = pos.ToString();
}
