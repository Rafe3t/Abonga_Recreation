using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField host, join;
    public int map;

    public void CreateRoom()
    {
        if(map != 0)
        {
            PhotonNetwork.CreateRoom(host.text);
        }
    }
    public void JoinRoom()
    {
        if(map != 0)
        {
            PhotonNetwork.JoinRoom(join.text);
        }
    }

    public override void OnJoinedRoom()
    {
        if (map == 1)
        {
            //city
            PhotonNetwork.LoadLevel(2);
        }
        else if (map == 2)
        {
            //maze
            PhotonNetwork.LoadLevel(3);
        }
    }

    public void selectCity()
    {
        map = 1;
    }

    public void selectMaze()
    {
        map = 2;
    }

    public void exitgame()
    {
        Application.Quit();
    }

}
