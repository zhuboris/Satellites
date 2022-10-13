using Gameplay.Player;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreDisplay : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            PlayerScore.Changed += OnScoreChanged;
        }

        private void OnDisable()
        {
            PlayerScore.Changed -= OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            _text.text = score.ToString();
        }
    }
}