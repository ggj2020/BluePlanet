using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class HomeCanvas : MonoBehaviour
{
    public Button BtnEasy;
    public Button BtnHard;
    public Button BtnHowToPlay;
}

public partial class HomeCanvas : MonoBehaviour
{
    private void Awake()
    {
        BtnEasy.onClick.AddListener( () =>
        {
            SceneManager.LoadScene( "Run", LoadSceneMode.Single );
        } );
    }
}