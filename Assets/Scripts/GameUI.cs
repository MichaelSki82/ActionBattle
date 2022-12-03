using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _hurdLevelText;
    [SerializeField] private TMP_Text _ScoreText;

    public void UpdateScoreAndLevel()
    {
        _hurdLevelText.text = $"Level {Stats.Level}";
        _ScoreText.text = "Score: " + Stats.Score.ToString("D4");

    }

    public void UpdateHp(int hp)
    {
        _hpText.text = $"HP: {hp}";
    }
}
