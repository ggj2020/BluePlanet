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
    [SerializeField] private SpriteRenderer alien;
    [SerializeField] private Sprite alienRun1;
    [SerializeField] private Sprite alienRun2;

    IEnumerator PlayAnimation()
    {
        while(true)
        {
            alien.sprite = alienRun2;
            yield return new WaitForSeconds(0.1f);

            alien.sprite = alienRun1;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Start()
    {
        StartCoroutine(PlayAnimation());

        this.weapon.gameObject.SetActive(false);
        this.effectVacuum.gameObject.SetActive(false);
        this.effectLandingOnEarth.gameObject.SetActive(false);

        this.alertBrokenSpaceship.gameObject.SetActive(false);
        this.alertDecideSomething.gameObject.SetActive(false);
        this.alertFoundBluePlanet.gameObject.SetActive(false);
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