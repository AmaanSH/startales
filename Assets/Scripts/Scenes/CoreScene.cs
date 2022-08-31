using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScene : MonoBehaviour
{
    public virtual IEnumerator Play()
    {
        yield return null;
    }

    public virtual void Next()
    {
        Director.Next();
    }
}
