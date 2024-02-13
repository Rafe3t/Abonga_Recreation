using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class UI_manager : MonoBehaviour
{
    public GameObject pause_menu;
    public GameObject death_menu;
    public GameObject miniMap;
    public bool mazeMap;
    public bool resumed;

    private void Start()
    {
        pause_menu.SetActive(false);
        death_menu.SetActive(false);
    }
    public void pause()
    {
        pause_menu.SetActive(true);
        if (mazeMap)
        {
            miniMap.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        resumed = false;
    }

    public void resume()
    {
        pause_menu.SetActive(false);
        death_menu.SetActive(false);
        resumed = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (mazeMap)
        {
            miniMap.SetActive(true);
        }
    }

    public void dead()
    {
        pause_menu.SetActive(false);
        death_menu.SetActive(true);
        resumed = true;
        if (mazeMap)
        {
            miniMap.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void BackToLobby()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(1);
    }
}
