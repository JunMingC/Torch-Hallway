using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerFPS;
    private Animator Anim;

    public void Start()
    {
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CharacterController controller = playerFPS.m_CharacterController;
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        float horizontalSpeed = horizontalVelocity.magnitude;
        Anim.SetFloat("Speed", horizontalSpeed);
    }
}
