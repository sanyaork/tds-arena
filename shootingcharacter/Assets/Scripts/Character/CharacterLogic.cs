﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLogic : MonoBehaviour
{
    public GameObject Camera;
    public GameObject weapon;

    private Vector3 movementDirection;
    private Vector3 aimingDirection;

    public float desiredAimingWeight = 0.0f;
    public float desiredLeftHandWeight = 0.0f;

////////////////////////////////////////////////////////////////////////////////////


    ////////////////////////////////////////////////////////////////////////////////////
    public CharacterController controller
    {
        get; private set;
    }

    public Animator animator
    {
        get; private set;
    }

    private void Start()
    {
        this.controller = GetComponent<CharacterController>();
        this.animator = GetComponent<Animator>();
    }

    public void Move(bool toggle, Vector3 direction)
    {
        this.animator.SetBool(CharacterHashes.Moving, toggle);
        this.movementDirection = direction;
    }

    public void Run(bool toggle)
    {
        this.animator.SetBool(CharacterHashes.Running, toggle);
    }

    public void Aim(bool toggle, Vector3 direction)
    {
        this.animator.SetBool(CharacterHashes.Aiming, toggle);
        this.aimingDirection = direction;
    }

    public void Shoot(bool toggle, Vector3 direction)
    {
        this.animator.SetBool(CharacterHashes.Shooting, toggle);

        if(toggle)
        {
            this.Aim(toggle, direction);
        }
    }

    public void AnimationShoot()
    {
        this.weapon.GetComponent<RangedWeapon>().Shoot();
    }

    private void Update()
    {
        // player camera rotation to the cursor
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        }
        // Update movement direction parameters.
        if(this.animator.GetBool(CharacterHashes.Moving))
        {
            this.animator.SetFloat(CharacterHashes.MovementX, this.movementDirection.x, 0.15f, Time.deltaTime);
            this.animator.SetFloat(CharacterHashes.MovementZ, this.movementDirection.z, 0.15f, Time.deltaTime);
        }
        else
        {
            this.animator.SetFloat(CharacterHashes.MovementX, 0.0f, 0.15f, Time.deltaTime);
            this.animator.SetFloat(CharacterHashes.MovementZ, 0.0f, 0.15f, Time.deltaTime);
        }

        // Update aiming direction parameters.
        if(this.animator.GetBool(CharacterHashes.Aiming))
        {
            this.animator.SetFloat(CharacterHashes.AimingX, this.aimingDirection.x, 0.1f, Time.deltaTime);
            this.animator.SetFloat(CharacterHashes.AimingZ, this.aimingDirection.z, 0.1f, Time.deltaTime);
        }
        else
        {
            this.animator.SetFloat(CharacterHashes.AimingX, 0.0f, 0.1f, Time.deltaTime);
            this.animator.SetFloat(CharacterHashes.AimingZ, 0.0f, 0.1f, Time.deltaTime);
            this.animator.SetFloat(CharacterHashes.StrafingX, 0.0f, 0.1f, Time.deltaTime);
            this.animator.SetFloat(CharacterHashes.StrafingZ, 0.0f, 0.1f, Time.deltaTime);
        }

        // Update aiming weight parameter.
        // Todo: Do not aim unless still or shooting, allowing strafe animations with weapon retracted.
        if(this.animator.GetBool(CharacterHashes.Shooting))
        {
            animator.SetFloat(CharacterHashes.AimingWeight, this.desiredAimingWeight, 0.3f, Time.deltaTime);
        }
        else
        {
            if(this.animator.GetBool(CharacterHashes.Aiming))
            {
                animator.SetFloat(CharacterHashes.AimingWeight, 0.0f, 0.3f, Time.deltaTime);
            }
            else
            {
                animator.SetFloat(CharacterHashes.AimingWeight, 0.0f, 0.1f, Time.deltaTime);
            }
            
        }

        // Update left hand weight parameter.
        animator.SetFloat(CharacterHashes.LeftHandWeight, this.desiredLeftHandWeight, 0.1f, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Apply gravity to the character controller.
        this.controller.Move(Physics.gravity * 0.1f * Time.fixedDeltaTime);
    }


////////////////////////////////////////////////////////////////////////////////////////////
 
}
