using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuActions : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "MenuInicial";
    [SerializeField] private string gameSceneName = "TelaJogo";

    public void IniciaJogo()
    {
        GameControler.init();
        SceneManager.LoadScene(gameSceneName);
    }

    public void Menu()
    {
        GameControler.init();
        SceneManager.LoadScene(menuSceneName);
    }
}
