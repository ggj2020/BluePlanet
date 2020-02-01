using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleSceneLoader : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene("Puzzle", LoadSceneMode.Additive);
    }
}
