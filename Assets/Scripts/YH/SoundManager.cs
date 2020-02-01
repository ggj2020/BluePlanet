using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AUDIOTYPE
{
    JUMP,
    GETOBJECT,
    SHAKE,
    DIE,
    TABOBJECT,
    BUTTONCLICK
}

public partial class SoundManager : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip getObject;
    public AudioClip shake;
    public AudioClip die;
    public AudioClip tabObject;
    public AudioClip btnClick;

    private AudioSource m_audioSource;
}

public partial class SoundManager : MonoBehaviour
{
    private void Awake()
    {
        Statics.soundManager = this;

        m_audioSource = GetComponent<AudioSource>();
    }

    public void Play( AUDIOTYPE eType )
    {
        switch ( eType )
        {
            case AUDIOTYPE.DIE:         m_audioSource.PlayOneShot( die );       break;
            case AUDIOTYPE.GETOBJECT:   m_audioSource.PlayOneShot( getObject ); break;
            case AUDIOTYPE.JUMP:        m_audioSource.PlayOneShot( jump );      break;
            case AUDIOTYPE.SHAKE:       m_audioSource.PlayOneShot( shake );     break;
            case AUDIOTYPE.TABOBJECT:   m_audioSource.PlayOneShot( tabObject ); break;
            case AUDIOTYPE.BUTTONCLICK: m_audioSource.PlayOneShot( btnClick );  break;
        }
    }
}
