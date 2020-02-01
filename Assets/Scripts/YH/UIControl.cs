using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UIControl : MonoBehaviour
{
    public GameObject UIStatus;
    public GameObject UIStageInfo;
    public GameObject UIPause;

    public Button BtnPause;
    public Button BtnResume;
    public Button BtnTitle;
}

public partial class UIControl : MonoBehaviour
{
    private void Start()
    {
        BtnPause.onClick.AddListener( () => 
        {
            // 일시정지
            UIPause.SetActive( true );
            Statics.bPause = true;
        } );

        BtnResume.onClick.AddListener( () =>
        {
            // 계속하기
            UIPause.SetActive( false );
            Statics.bPause = false;
        } );

        BtnTitle.onClick.AddListener( () =>
        {
            // 타이틀로 가기
            OnStageEnd();
        } );
    }

    public void OnStageStart()
    {
        StartCoroutine( CoroUIStageInfo( true ) );
    }
    public void OnStageEnd()
    {
        StartCoroutine( CoroUIStageInfo( false, 100 ) );
    }

    private IEnumerator CoroUIStageInfo( bool bStart, int nProgress = 0 )
    {
        UIStageInfo.SetActive( true );
        Transform tText = UIStageInfo.transform.Find( "Text" );
        Text text = tText.GetComponent<Text>();
        Color c = text.color;
        c.a = 0;
        int nAlpha = ( int )( c.a * 255.0f );
        text.color = c;

        string sStep = bStart ? "START!" : $"{nProgress}%\nREPAIR!";
        text.text = $"<color=#0000FF{nAlpha.ToString("X2")}>easy</color> <color=#ff0000{nAlpha.ToString( "X2" )}>STAGE:1\n{sStep}</color>";

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
                    yield return new WaitForSeconds( 1 );
                }
                nAlpha = ( int )( c.a * 255.0f );
                text.text = $"<color=#0000FF{nAlpha.ToString( "X2" )}>easy</color> <color=#ff0000{nAlpha.ToString( "X2" )}>STAGE:1\n{sStep}</color>";
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
                text.text = $"<color=#0000FF{nAlpha.ToString( "X2" )}>easy</color> <color=#ff0000{nAlpha.ToString( "X2" )}>STAGE:1\n{sStep}</color>";
            }
            
            yield return new WaitForSeconds( 0 );
        }

        UIStageInfo.SetActive( false );
    }
}
