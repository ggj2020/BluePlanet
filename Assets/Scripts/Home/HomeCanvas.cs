using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public partial class HomeCanvas : MonoBehaviour
{
    public Button BtnEasy;
    public Button BtnHard;
}

public partial class HomeCanvas : MonoBehaviour
{
    private void Awake()
    {
        BtnEasy.onClick.AddListener(OnStartGame);
        BtnHard.onClick.AddListener(OnStartGame);
    }

    void OnStartGame()
    {
        SceneManager.LoadScene( "Run", LoadSceneMode.Single );
    }
}