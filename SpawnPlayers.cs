using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerprefab;
    public float minx, maxx, miny, maxy,height;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 randompos = new Vector3(Random.Range(minx, maxx), height, Random.Range(miny, maxy));
        PhotonNetwork.Instantiate(playerprefab.name, randompos,Quaternion.identity);
    }
}
