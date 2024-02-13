using UnityEngine;

public class MiniMap_camera : MonoBehaviour
{
    public GameObject player;
    public GameObject[] otherplayers;
    public int num_of__players;
    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        arrow = Resources.Load<GameObject>("Arroww");
        otherplayers = GameObject.FindGameObjectsWithTag("Player");
        num_of__players = otherplayers.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, player.transform.eulerAngles.y, transform.eulerAngles.z);
        }
        if(num_of__players != GameObject.FindGameObjectsWithTag("Player").Length)
        {
            players_changed();
        }
    }

    void players_changed()
    {
        otherplayers = GameObject.FindGameObjectsWithTag("Player");
        num_of__players = otherplayers.Length;
        GameObject[] old_arrows = GameObject.FindGameObjectsWithTag("arrow");

        foreach(GameObject old_arrow in old_arrows)
        {
            Destroy(old_arrow);
        }

        foreach(GameObject playr in otherplayers)
        {
            if(playr.transform.position != player.transform.position)
            {
                Vector3 pos = new Vector3(playr.transform.position.x,52.3f, playr.transform.position.x);
                GameObject current_arrow = Instantiate(arrow, pos, Quaternion.Euler(90,0,0));
                current_arrow.GetComponent<arrow>().player = playr;
            }
        }
    }
}
