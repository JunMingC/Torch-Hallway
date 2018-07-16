using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioSource AudioSourceLoop;
    public AudioClip AttackClip;
    public AudioClip WalkClip;
    public float MaxSpeed;

    private Animator Anim;
    private Vector3 NewPosition;
    private bool IsWalk;
    private bool IsAttack;
    private float WalkTimer;

    public void Start()
    {
        Anim = GetComponent<Animator>();
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player") && !IsAttack)
        {
            Attack(collision.gameObject.GetComponentInChildren<Player>());
        }
    }
    
    public void Update()
    {
        if (IsWalk)
        {
            transform.LookAt(NewPosition);

            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("creature1run"))
            {
                transform.position = Vector3.MoveTowards(transform.position, NewPosition, MaxSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, NewPosition) <= 2f)
                {
                    IsWalk = false;
                    IsAttack = false;
                    Anim.SetBool("Walk", IsWalk);
                    AudioSourceLoop.Play();
                }
            }
        }
    }

    public void Chase(Transform Target)
    {
        NewPosition = new Vector3(Target.position.x, transform.position.y, Target.position.z);

        if (Vector3.Distance(transform.position, NewPosition) > 2f)
        {
            if (!IsWalk && Anim.GetCurrentAnimatorStateInfo(0).IsName("creature1Taunt"))
            {
                IsWalk = true;
                Anim.SetBool("Walk", IsWalk);
                Roar();
            }
        }
    }

    public void Stop(Transform Target)
    {
        NewPosition = transform.position;
    }

    public void Roar()
    {
        AudioSourceLoop.Stop();
        AudioSource.PlayOneShot(WalkClip);
    }

    public void Attack(Player player)
    {
        if(!player.torch.activeInHierarchy)
        {
            player.Attacked();
            Anim.SetTrigger("Attack");
            IsAttack = true;
            IsWalk = false;
            Anim.SetBool("Walk", IsWalk);
            AudioSourceLoop.Stop();
            AudioSource.PlayOneShot(AttackClip);
        }
    }
}
