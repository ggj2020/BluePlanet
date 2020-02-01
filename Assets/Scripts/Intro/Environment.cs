using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private SpriteRenderer environmentMoon = null;
    [SerializeField] private SpriteRenderer environmentSun = null;
    [SerializeField] private SpriteRenderer environmentEarth = null;

    void Start()
    {
        environmentMoon.gameObject.SetActive(false);
        environmentSun.gameObject.SetActive(false);
        environmentEarth.gameObject.SetActive(false);
    }

    public void Show(Cut cut)
    {
        if ( cut.environments != null ) {
            cut.environments.ToList().ForEach((keyValue) => {
                if ( keyValue.Key == EnvironmentType.Moon )
                {
                    environmentMoon.gameObject.SetActive(keyValue.Value == VisibleState.Show);
                }
                else if ( keyValue.Key == EnvironmentType.Sun )
                {
                    environmentSun.gameObject.SetActive(keyValue.Value == VisibleState.Show);
                }
                else if ( keyValue.Key == EnvironmentType.Earth )
                {
                    environmentEarth.gameObject.SetActive(keyValue.Value == VisibleState.Show);
                }
            });
        }
    }
}
