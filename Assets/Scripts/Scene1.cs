using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene1 : MonoBehaviour
{
    public static Scene1 scene1;
    public TMP_InputField inputField;

    public string player_name;

    public GameManager gameManager;

    public Button startButton;

    public void Awake() //only one instance of whaterver u do
    {
        if (scene1 == null)
        {
            scene1 = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(SetPlayerName);

    }
    public void SetPlayerName()
    {
        player_name = inputField.text;
        gameManager.StartGame();
        SceneManager.LoadScene(1);
        

    }
    
}
