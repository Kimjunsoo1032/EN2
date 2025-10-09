using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{
    private int score_;
    private TMP_Text scoreText_;
    private void Start()
    {
        score_ = 0;
        scoreText_ = GetComponent<TMP_Text>();
    }
    public void SetScore(int score)
    {
        score_ = score;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        scoreText_.text = $"SCORE:{score_:0000000}";
    }
}
