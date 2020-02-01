using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] private SpriteRenderer alertBrokenSpaceship = null;
    [SerializeField] private SpriteRenderer alertDecideSomething = null;
    [SerializeField] private SpriteRenderer alertFoundBluePlanet = null;

    [SerializeField] private SpriteRenderer effectLandingOnEarth = null;
    [SerializeField] private SpriteRenderer effectVacuum = null;

    [SerializeField] private SpriteRenderer weapon = null;

    [SerializeField] private Sprite happy = null;
    [SerializeField] private Sprite sad = null;
    [SerializeField] private Sprite decided = null;

    void Start()
    {
        this.weapon.gameObject.SetActive(false);
        this.effectVacuum.gameObject.SetActive(false);
        this.effectLandingOnEarth.gameObject.SetActive(false);

        this.alertBrokenSpaceship.gameObject.SetActive(false);
        this.alertDecideSomething.gameObject.SetActive(false);
        this.alertFoundBluePlanet.gameObject.SetActive(false);
    }

    private Sprite GetCharacterEmotionSprite(CharacterEmotionState state)
    {
        if ( state == CharacterEmotionState.Happy )
        {
            return this.happy;
        }
        else if ( state == CharacterEmotionState.Sad )
        {
            return this.sad;
        }
        else if ( state == CharacterEmotionState.Decided )
        {
            return this.decided;
        }
        else
        {
            return null;
        }
    }

    private GameObject GetAlertGameObject(AlertType alertType)
    {
        if ( alertType == AlertType.BrokenSpaceShip )
        {
            return alertBrokenSpaceship.gameObject;
        }
        else if ( alertType == AlertType.Realized )
        {
            return alertFoundBluePlanet.gameObject;
        }
        else if ( alertType == AlertType.Decided )
        {
            return alertDecideSomething.gameObject;
        }
        else
        {
            return null;
        }

    }

    public void Show(Cut cut)
    {
        var characterEmotionSprite = GetCharacterEmotionSprite(cut.characterEmotionState);
        if ( characterEmotionSprite != null )
        {
            GetComponent<SpriteRenderer>().sprite = characterEmotionSprite;
        }

        if ( cut.characterActionState == CharacterActionState.VacuumSomethings )
        {
            this.weapon.gameObject.SetActive(true);
            this.effectVacuum.gameObject.SetActive(true);
        }
        else if ( cut.characterActionState == CharacterActionState.LandingOnEarth )
        {
            this.weapon.gameObject.SetActive(false);
            this.effectVacuum.gameObject.SetActive(false);
            this.effectLandingOnEarth.gameObject.SetActive(true);
        }

        if ( cut.alerts != null ) {
            cut.alerts.ToList().ForEach((keyValue) => {
                var alertGameObject = GetAlertGameObject(keyValue.Key);
                if ( alertGameObject != null )
                {
                    alertGameObject.SetActive(keyValue.Value == VisibleState.Show);
                }
            });
        }
    }
}