using UnityEngine;

public partial class Garbage : RunObject
{
    public int objectIndex;

    private bool m_bActive;
}

public partial class Garbage : RunObject
{
    private void Awake()
    {
        m_bActive = true;
    }

    public void Deactivate() { m_bActive = false; }
}
