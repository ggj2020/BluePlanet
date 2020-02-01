using UnityEngine;

public partial class Garbage : RunObject
{
    public int objectIndex;

    public bool bActive { get; private set; }
}

public partial class Garbage : RunObject
{
    private void Awake()
    {
        bActive = true;
    }

    public void Deactivate() { bActive = false; }

    private void FixedUpdate()
    {
        if ( Statics.bPause ) return;

        float fDiff = Statics.playerUnit.position.x - position.x;
        if ( fDiff > 10 ) Destroy( gameObject );
    }
}
