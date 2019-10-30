using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{

    #region Singleton
    public static Login instance;

    void Awake()
    {
        instance = this;
    }
    #endregion
    InputField inputName;

    InputField inputPassword;

    Text outputMessage;

    public GameObject textUsername;

    public GameObject textPassword;

    public GameObject textMessage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkLogin()
    {
        Debug.Log("checking login");
        var DB = new DataService("existing.db");
        //get the components to check username and password
        inputName = textUsername.GetComponent<InputField>();
        inputPassword = textPassword.GetComponent<InputField>();
        outputMessage = textMessage.GetComponent<Text>();
        string username = inputName.text;
        string password = inputPassword.text;

        //check check db with the username
        //check if password is right 
        Person currentplayer = DB.CheckLoginExists(username, password);

        GameModel.currentPlayer = currentplayer;
        
        //load the game scene
        if (currentplayer != null)
        {
            Debug.Log("Player != null");
            SceneManager.LoadScene("Scene1");
            PlayerStats.instance.LoadPlayer();
        }
        else
        {
            Debug.Log("No save found");
            outputMessage.text = "No Login found with that username and password please try again";
            //SceneManager.LoadScene("Scene1");

        }

        //pass the current player to be loaded 
        //Person playerLogin = DataService.GetLogin(username);
        }
    }
