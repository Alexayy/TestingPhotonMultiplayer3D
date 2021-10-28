using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject instantiated;

    private Transform _instancedTransform;

    private Vector3 randomPosition;
    
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    private float num1;
    private float num2;

    private void Start()
    {
        randomPosition = new Vector3(Random.Range(minX, maxX), 20, Random.Range(minZ, maxZ)); 
        instantiated = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }

    public GameObject GetInstantiated()
    {
        return instantiated.GetComponent<ThirdPersonController>().LookAt();
    }
    public Transform GetSomething()
    {
        return instantiated.GetComponent<ThirdPersonController>().transform;
    }
}
