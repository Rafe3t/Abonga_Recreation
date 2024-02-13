using UnityEngine;
using UnityEngine.AI;

public class Abonga : MonoBehaviour
{
    private Collider[] colliders;
    public Transform player;
    public GameObject[] respawns;
    public NavMeshAgent agent;
    public float radius_search;
    private float check_time;
    private float chase_player_time;
    private float watchover;
    // Start is called before the first frame update
    void Start()
    {
        respawns = GameObject.FindGameObjectsWithTag("Respawn");
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            searchplayer();
            moveRandom();
        }
        else
        {
            if(check_time + 15 < Time.time)
            {
                player = null;
                check_time = Time.time;
            }
            if(player != null && chase_player_time + 0.2 <Time.time)
            {
                chaseplayer();
                chase_player_time = Time.time;
            }
        }
    }

    void searchplayer()
    {
        colliders = Physics.OverlapSphere(transform.position, radius_search);
        foreach(Collider colld in colliders)
        {
            if(colld.gameObject.tag == "Player")
            {
                player = colld.gameObject.transform;
                check_time = Time.time;
            }
        }
    }

    void chaseplayer()
    {
        agent.SetDestination(new Vector3(player.position.x,transform.position.y,player.position.z));
    }

    void moveRandom()
    {
        if(Time.time > watchover + 10)
        {
            Transform thisOne = respawns[Random.Range(0, 2)].transform;
            agent.SetDestination(thisOne.position);
            watchover = Time.time;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player_movment>().died();
            //died code
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player_movment>().died();
            //died code
        }
    }

}
