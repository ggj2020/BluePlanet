using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour, IPointerClickHandler
{
    private Queue<Cut> cuts = new Queue<Cut>(new Cut[] {
        new Cut() {
            characterActionState = CharacterActionState.Appear,
            characterEmotionState = CharacterEmotionState.Happy,
            alerts = null,
            environments = null,
        },
        new Cut() {
            characterActionState = CharacterActionState.NoneChange,
            characterEmotionState = CharacterEmotionState.Sad,
            alerts = new Dictionary<AlertType, VisibleState> {
                {AlertType.BrokenSpaceShip, VisibleState.Show},
            },
            environments = null,
        },
        new Cut() {
            characterActionState = CharacterActionState.FindSolution,
            characterEmotionState = CharacterEmotionState.NoneChange,
            alerts = null,
            environments = new Dictionary<EnvironmentType, VisibleState> {
                {EnvironmentType.Sun, VisibleState.Show},
                {EnvironmentType.Moon, VisibleState.Show},
            },
        },
        new Cut() {
            characterActionState = CharacterActionState.VacuumSomethings,
            characterEmotionState = CharacterEmotionState.NoneChange,
            alerts = null,
            environments = new Dictionary<EnvironmentType, VisibleState> {
                {EnvironmentType.Sun, VisibleState.Hidden},
                {EnvironmentType.Moon, VisibleState.Hidden},
            },
        },
        new Cut() {
            characterActionState = CharacterActionState.NoneChange,
            characterEmotionState = CharacterEmotionState.Happy,
            alerts = new Dictionary<AlertType, VisibleState> {
                {AlertType.Realized, VisibleState.Show},
            },
            environments = new Dictionary<EnvironmentType, VisibleState> {
                {EnvironmentType.Earth, VisibleState.Show},
            },
        },
        new Cut() {
            characterActionState = CharacterActionState.NoneChange,
            characterEmotionState = CharacterEmotionState.NoneChange,
            alerts = new Dictionary<AlertType, VisibleState> {
                {AlertType.Decided, VisibleState.Show},
                {AlertType.Realized, VisibleState.Hidden},
            },
            environments = null,
        },
        new Cut() {
            characterActionState = CharacterActionState.LandingOnEarth,
            characterEmotionState = CharacterEmotionState.Decided,
            alerts = null,
            environments = null,
        }
    });

    [SerializeField] private Alien alien;
    [SerializeField] private Environment environment;

    void Start()
    {
        Next();
    }

    void Next()
    {
        if ( cuts.Count == 0 )
        {
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }
        else
        {
            Show(cuts.Dequeue());
        }        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Next();
    }

    void Show(Cut cut)
    {
        this.alien.Show(cut);
        this.environment.Show(cut);
    }
}

public enum CharacterEmotionState {
    NoneChange,
    Happy,
    Sad,
    Decided,
}

public enum CharacterActionState {
    NoneChange,
    Appear,
    FindSolution,
    VacuumSomethings,
    LandingOnEarth,
}

public enum AlertType {
    BrokenSpaceShip,
    Realized,
    Decided,
}

public enum EnvironmentType {
    Sun,
    Moon,
    Earth,
}

public enum VisibleState {
    Hidden,
    Show,
}

public struct Cut {
    public CharacterActionState characterActionState;
    public CharacterEmotionState characterEmotionState;
    public Dictionary<AlertType, VisibleState> alerts;
    public Dictionary<EnvironmentType, VisibleState> environments;

}
