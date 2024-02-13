using UnityEngine;

public class player_sound : MonoBehaviour
{
    public AudioSource audioo;
    public Animator anim;
    private bool played;

    // Update is called once per frame
    void Update()
    {
        if(anim.GetBool("Jump") == false && anim.GetFloat("Speed") > 0.1f && !played)
        {
            audioo.Play();
            played = true;
        }
        
        if(anim.GetBool("Jump") == true || anim.GetFloat("Speed") < 0.1f)
        {
            audioo.Pause();
            played = false;
        }
    }
}
