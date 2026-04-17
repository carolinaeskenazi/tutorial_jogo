using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameControler
{
    private static int collectableCount;
    private static int totalCollectableCount;
    private static float elapsedTime;
    private const string EndGameSceneName = "TelaFinal";
    private static bool collectableCountInitialized;

    public static bool gameOver;
    private static bool hasLoadedEndGameScene;

    public static void init()
    {
        collectableCount = 0;
        totalCollectableCount = 0;
        elapsedTime = 0f;
        collectableCountInitialized = false;
        gameOver = false;
        hasLoadedEndGameScene = false;
    }

    public static void SetCollectableCount(int count)
    {
        collectableCount = count;
        totalCollectableCount = count;
        collectableCountInitialized = true;
        gameOver = false;
    }

    public static void Collect()
    {
        if (gameOver)
        {
            return;
        }

        if (!collectableCountInitialized)
        {
            return;
        }

        collectableCount--;
        Debug.Log($"Collectables restantes: {collectableCount}");

        if (collectableCount <= 0)
        {
            collectableCount = 0;
            gameOver = true;
            LoadEndGameScene();
        }
    }

    public static void Enemy()
    {
        gameOver = true;
        LoadEndGameScene();
    }

    public static bool IsCollectableCountInitialized()
    {
        return collectableCountInitialized;
    }

    public static void UpdateTimer(float deltaTime)
    {
        if (gameOver || !collectableCountInitialized)
        {
            return;
        }

        elapsedTime += deltaTime;
    }

    public static int GetCollectedCount()
    {
        return totalCollectableCount - collectableCount;
    }

    public static int GetTotalCollectableCount()
    {
        return totalCollectableCount;
    }

    public static float GetElapsedTime()
    {
        return elapsedTime;
    }

    public static string GetFormattedElapsedTime()
    {
        int totalSeconds = Mathf.FloorToInt(elapsedTime);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return $"{minutes:00}:{seconds:00}";
    }

    private static void LoadEndGameScene()
    {
        if (hasLoadedEndGameScene)
        {
            return;
        }

        hasLoadedEndGameScene = true;
        SceneManager.LoadScene(EndGameSceneName);
    }
}
