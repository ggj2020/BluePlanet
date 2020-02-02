using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class UIControl : MonoBehaviour
{
    public GameObject UIStatus;
    public GameObject UIStageInfo;
    public GameObject UIPause;

    public Button BtnPause;
    public Button BtnResume;
    public Button BtnTitle;

    public Material MatProgress;
}

public partial class UIControl : MonoBehaviour
{
    private void Awake()
    {
        Statics.uiControl = this;
    }
    void OnEnable()
    {
        EventManager.StartListening(typeof(GameOverEvent), OnStageEnd);
    }
    void OnDisable()
    {
        EventManager.StopListening(typeof(GameOverEvent), OnStageEnd);
    }
    private void Start()
    {
        BtnPause.onClick.AddListener( () => 
        {
            // 일시정지
            Statics.soundManager.Play( AUDIOTYPE.BUTTONCLICK );
            UIPause.SetActive( true );
            Statics.bPause = true;
        } );

        BtnResume.onClick.AddListener( () =>
        {
            // 계속하기
            Statics.soundManager.Play( AUDIOTYPE.BUTTONCLICK );
            UIPause.SetActive( false );
            Statics.bPause = false;
        } );

        BtnTitle.onClick.AddListener( () =>
        {
            // 타이틀로 가기
            Statics.soundManager.Play( AUDIOTYPE.BUTTONCLICK );
            SceneManager.LoadScene( "Menu", LoadSceneMode.Single );
        } );

        OnStageStart();
    }

    private void FixedUpdate()
    {
        float fCurrentHeight = MatProgress.GetFloat( "_Height" );
        float fTargetHeight = ( float )Statics.nProgress / Constant.PROGRESS_MAX;
        MatProgress.SetFloat( "_Height", Mathf.Lerp( fCurrentHeight, fTargetHeight, 0.1f ) );
    }

    public void OnStageStart()
    {
        Transform tText = UIStatus.transform.Find( "Text" );
        Text text = tText.GetComponent<Text>();
        text.text = $"<color=#0000FF>easy</color> <color=#ff0000>STAGE:{Statics.nStage}\n</color>";

        StartCoroutine( CoroUIStageInfo( true ) );
    }
    private void OnStageEnd(IEvent param)
    {
        StartCoroutine( CoroUIStageInfo( false ) );
    }

    private IEnumerator CoroUIStageInfo( bool bStart )
    {
        UIStageInfo.SetActive( true );
        Transform tText = UIStageInfo.transform.Find( "Text" );
        Text text = tText.GetComponent<Text>();
        Color c = text.color;
        c.a = 0;
        int nAlpha = ( int )( c.a * 255.0f );
        text.color = c;

        string sStep = bStart ? "START!" : $"{Statics.nProgress}%\nREPAIR!";
        text.text = $"<color=#0000FF{nAlpha.ToString("X2")}>easy</color> <color=#ff0000{nAlpha.ToString( "X2" )}>STAGE:{Statics.nStage}\n{sStep}</color>";

        bool bFadeIn = true;

        while ( true )
        {
            if ( bFadeIn )
            {
                c.a += 0.01f;
                if ( c.a >= 1 )
                {
                    bFadeIn = false;
                    c.a = 1;
                    yield return new WaitForSeconds( 2 );
                }
                nAlpha = ( int )( c.a * 255.0f );
                text.text = $"<color=#0000FF{nAlpha.ToString( "X2" )}>easy</color> <color=#ff0000{nAlpha.ToString( "X2" )}>STAGE:{Statics.nStage}\n{sStep}</color>";
            }
            else
            {
                c.a -= 0.01f;
                if ( c.a <= 0 )
                {
                    c.a = 0;
                    text.color = c;
                    break;
                }
                nAlpha = ( int )( c.a * 255.0f );
                text.text = $"<color=#0000FF{nAlpha.ToString( "X2" )}>easy</color> <color=#ff0000{nAlpha.ToString( "X2" )}>STAGE:{Statics.nStage}\n{sStep}</color>";
            }
            
            yield return new WaitForSeconds( 0 );
        }

        UIStageInfo.SetActive( false );

        if ( !bStart )
        {
            Statics.nStage++;
            Statics.nProgress = 0;

            SceneManager.LoadScene( "Run", LoadSceneMode.Single );
        }
    }
}
