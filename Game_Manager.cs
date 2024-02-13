using UnityEngine;
using Photon.Pun;

public class Game_Manager : MonoBehaviour
{
    private PhysicMaterial slip;
    private GameObject[] colliders;
    private GameObject abonga,check_abonga;
    public float raduis_of_abonga;
    public Vector3 startPos_of_abonga;

    // Start is called before the first frame update
    void Start()
    {
        abonga = (GameObject)Resources.Load("Abonga");
        check_abonga = GameObject.FindGameObjectWithTag("abonga");

        if (check_abonga == null)
        {
            GameObject current_abonga = PhotonNetwork.Instantiate(abonga.name, startPos_of_abonga, Quaternion.identity);
            current_abonga.GetComponent<Abonga>().radius_search = raduis_of_abonga;
        }

        slip = (PhysicMaterial)Resources.Load("slip");
        colliders = FindObjectsOfType<GameObject>();
        foreach(GameObject obj in colliders)
        {
            Collider colld = obj.GetComponent<Collider>();
            if(colld != null)
            {
                colld.material = slip;
            }
        }
    }

}
