using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("WWWWWWWWWWWWWWW Connected");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("WWWWWWWWWWWWWWW Joined Lobby");
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Scenes/Lobby");
        Debug.Log("WWWWWWWWWWWWWWW Lobby loaded?");
    }
}
