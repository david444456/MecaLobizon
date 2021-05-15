using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform transformPlayer;

    [SerializeField] Vector3 distanceToPlayer;

    void LateUpdate()
    {
        transform.position = transformPlayer.position + distanceToPlayer;
    }
}
