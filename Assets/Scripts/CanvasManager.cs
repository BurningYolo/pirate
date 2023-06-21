using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameObject gameRunningPanel;
    public GameObject gameWonPanel;
    public GameObject gameLostPanel;
    public bool lost = false;
    public bool won = false;
    public GameObject[] boats; 

    private void Start()
    {
        // Deactivate all panels except the main menu panel
        DeactivateAllPanels();
        ActivatePanel(gameRunningPanel);

    }


    public void Update()
    {

        if(!won && !lost)
        {
            Time.timeScale = 1f;

        }

        if (!boats[boats.Length - 1].activeSelf)
        {
            won = true; 
        }


        if (lost)
        {
            try
            {
                FirebaseInitialize.instance.LogEvent1("Game_is_started");
                Debug.Log("Firebase connected Lost");
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
            GameLost();
            

        }
        if (won)
        {
            try
            {
                FirebaseInitialize.instance.LogEvent1("Game_is_started");
                Debug.Log("Firebase connected in Won");
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
            GameWon();
            
        }
    }

    public void PlayGame()
    {
        // Deactivate all panels except the game running panel
        DeactivateAllPanels();
        ActivatePanel(gameRunningPanel);
    }

    public void GameWon()
    {
        // Deactivate all panels except the game won panel
        DeactivateAllPanels();
        ActivatePanel(gameWonPanel);
        Time.timeScale = 0f;
    }

    public void GameLost()
    {
        // Deactivate all panels except the game lost panel
        DeactivateAllPanels();
        ActivatePanel(gameLostPanel);
        Time.timeScale = 0f;
    }

    private void DeactivateAllPanels()
    {
        
        gameRunningPanel.SetActive(false);
        gameWonPanel.SetActive(false);
        gameLostPanel.SetActive(false);
    }

    private void ActivatePanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
