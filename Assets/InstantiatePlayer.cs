using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    [SerializeField] private SpawnPlayers _player;
    [SerializeField] private CinemachineFreeLook _cinemachine;
    void Start()
    {
        _cinemachine.Follow = _player.instantiated.transform;
        _cinemachine.LookAt = _player.GetInstantiated().transform;
    }
}
