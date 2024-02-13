using UnityEngine;
using Photon.Pun;

public class Player_movment : MonoBehaviour
{
    public GameObject _camera;
    public float player_speed,sprint_speed,speedScale_on_air;
    public Rigidbody player_rb;
    public float camera_x_speed, camera_y_speed;
    private float x_rotaion = 0;
    private float current_speed;
    public float jump_force;
    private bool speed_on_jump;
    public bool jumped;
    public bool lock_move;
    private float time_between_jumps;
    public Animator anim;
    public Transform ground_checker;
    private Vector3 current_look;
    private bool iscollided;
    private bool change_dir_speed_on_jump;
    private PhotonView view;
    private GameObject ui;
    private bool respawn_hold;
    private float respawn_time_hold;
    private bool game_paused;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("ui");
        view = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(!view.IsMine)
        {
            Destroy(_camera);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            if(!game_paused)
            {
                camera_movment();
                player_movment();
                jumping();
            }
            animation_player();
            pauseGame();
            resumeGame();
            spawn();
        }
    }

    public void died()
    {
         respawn_time_hold = Time.time;
         respawn_hold = true;
         transform.position = GameObject.FindGameObjectWithTag("respawn_hold").transform.position;
         ui.GetComponent<UI_manager>().dead();
    }

    void spawn()
    {
        if(respawn_hold && respawn_time_hold + 3 < Time.time)
        {
            respawn_hold = false;
            transform.position = GameObject.FindGameObjectWithTag("spawn").transform.position;
            ui.GetComponent<UI_manager>().resume();
        }
    }

    void pauseGame()
    {
        if(Input.GetAxis("Cancel") > 0)
        {
            ui.GetComponent<UI_manager>().pause();
            game_paused = true;
            player_rb.velocity = new Vector3(0, player_rb.velocity.y, 0);
        }
    }
    void resumeGame()
    {
        bool res = ui.GetComponent<UI_manager>().resumed;
        if(res)
        {
            game_paused = false;
        }
    }
    void animation_player()
    {
        anim.SetBool("Jump", jumped);
        anim.SetFloat("Speed",Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")));
        if (Input.GetAxisRaw("Shift") > 0 && Input.GetAxis("Vertical") > 0)
        {
            anim.SetBool("Shift", true);
        }
        else
        {
            anim.SetBool("Shift", false);
        }
    }
    void jumping()
    {
        if(!jumped && Input.GetAxis("Jump") > 0)
        {
            player_rb.AddForce(Vector3.up * jump_force);
            jumped = true;
            time_between_jumps = Time.time;
        }
        if (speed_on_jump && jumped && !change_dir_speed_on_jump)
        {
            lock_move = true;
            player_rb.velocity = new Vector3(current_look.x * current_speed * speedScale_on_air, player_rb.velocity.y, current_look.z * current_speed * speedScale_on_air);
            
        }
        if (jumped && Time.time > time_between_jumps + 0.2f)
        {
            Collider[] colliders = Physics.OverlapSphere(ground_checker.position,0.38f);
            foreach(Collider collider in colliders)
            {
                if (collider.gameObject.tag == "Untagged")
                {
                    jumped = false;
                    lock_move = false;
                    change_dir_speed_on_jump = false;
                }
            }
            if (iscollided && speed_on_jump)
            {
                change_dir_speed_on_jump = true;
                lock_move = false;
            }
            if(change_dir_speed_on_jump)
            {
                player_rb.velocity = new Vector3(transform.forward.x * current_speed * speedScale_on_air, player_rb.velocity.y, transform.forward.z * current_speed * speedScale_on_air);
            }
        }
        if(!jumped)
        {
            bool isSetJump = false;
            Collider[] colliders = Physics.OverlapSphere(ground_checker.position, 0.34f);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.tag == "Untagged")
                {
                    isSetJump = true;
                }
            }
            if(!isSetJump)
            {
                jumped = true;
            }
        }
    }
    void player_movment()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(!lock_move)
        {
            if (Input.GetAxisRaw("Shift") > 0 && vertical > 0 && !jumped)
            {
                player_rb.velocity = new Vector3(transform.forward.x * vertical * sprint_speed, player_rb.velocity.y,transform.forward.z * vertical * sprint_speed) + new Vector3(transform.right.x * horizontal * player_speed,0, transform.right.z * horizontal * player_speed);
                current_speed = vertical * sprint_speed;
                speed_on_jump = true;
                current_look = transform.forward;
            }
            else
            {
                player_rb.velocity = new Vector3(transform.forward.x * vertical * player_speed,player_rb.velocity.y, transform.forward.z * vertical * player_speed) + new Vector3(transform.right.x * horizontal * player_speed,0, transform.right.z * horizontal * player_speed);
                speed_on_jump = false;
            }
        }    
    }
    void camera_movment()
    {
        float mouse_x = Input.GetAxis("Mouse X") * camera_x_speed * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * camera_y_speed * Time.deltaTime;
        x_rotaion -= mouse_y;
        x_rotaion = Mathf.Clamp(x_rotaion, -90, 90);

        transform.Rotate(Vector3.up, mouse_x);
        _camera.transform.localRotation = Quaternion.Euler(x_rotaion, 0, 0);
    }
    private void OnCollisionStay(Collision collision)
    {
        iscollided = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        iscollided = false;
    }
}
