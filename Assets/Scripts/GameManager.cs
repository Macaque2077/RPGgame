using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
#region singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private bool gamestate = GameModel.dummy;

    public GameObject scoreText;
    Text text;

    Person currentPlayer;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("loading save-------------");
        //PlayerManager.instance.loadPlayerSave(GameModel.currentPlayer);
        //PlayerStats.instance.LoadPlayer();

    }

    public void updateScore()
    {
        text = scoreText.GetComponent<Text>();
        text.text = "Score: " + GameModel.currentPlayer.score;
    }

    public void setCurrentPlayer(Person player)
    {
        currentPlayer = player;

    }

    public void createCurrentPlayer()
    {
        PlayerManager.instance.PlayerSaveManager();
    }

    internal void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
