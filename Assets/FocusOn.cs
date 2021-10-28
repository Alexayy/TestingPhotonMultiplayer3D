using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class FocusOn : MonoBehaviour
{
    public DepthOfField dof;

    public SpawnPlayers players;
    // Start is called before the first frame update
    void Start()
    {
        dof.focalTransform = players.GetSomething();
    }
}
