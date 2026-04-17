using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject endGamePanel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalTimeText;
    [SerializeField] private string scorePrefix = "Pontuação: ";
    [SerializeField] private string timePrefix = "Tempo: ";
    [SerializeField] private string finalScorePrefix = "Pontuação final: ";
    [SerializeField] private string finalTimePrefix = "Tempo final: ";
    [SerializeField] private string gameplaySceneName = "TelaJogo";

    void Start()
    {
        if (SceneManager.GetActiveScene().name == gameplaySceneName)
        {
            GameControler.SetCollectableCount(GameObject.FindGameObjectsWithTag("Coletavel").Length);
        }

        UpdateEndGameState();
        UpdateScoreText();
        UpdateTimeText();
        UpdateFinalSummaryText();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == gameplaySceneName)
        {
            GameControler.UpdateTimer(Time.deltaTime);
        }

        UpdateEndGameState();
        UpdateScoreText();
        UpdateTimeText();
        UpdateFinalSummaryText();
    }

    private void UpdateEndGameState()
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(GameControler.gameOver);
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText == null || !GameControler.IsCollectableCountInitialized())
        {
            return;
        }

        scoreText.text = $"{scorePrefix}{GameControler.GetCollectedCount()}/{GameControler.GetTotalCollectableCount()}";
    }

    private void UpdateTimeText()
    {
        if (timeText == null || !GameControler.IsCollectableCountInitialized())
        {
            return;
        }

        timeText.text = $"{timePrefix}{GameControler.GetFormattedElapsedTime()}";
    }

    private void UpdateFinalSummaryText()
    {
        if (!GameControler.IsCollectableCountInitialized())
        {
            return;
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = $"{finalScorePrefix}{GameControler.GetCollectedCount()}/{GameControler.GetTotalCollectableCount()}";
        }

        if (finalTimeText != null)
        {
            finalTimeText.text = $"{finalTimePrefix}{GameControler.GetFormattedElapsedTime()}";
        }
    }
}
