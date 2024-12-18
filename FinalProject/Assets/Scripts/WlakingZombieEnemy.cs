using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//public enum EnemyType
//{
//    Zombie1,
//}

public class WalkingZombieEnemy : MonoBehaviour
{

    //[SerializeField] private EnemyType enemyType;

    [SerializeField] private int damage=20;



    [SerializeField] private float attackingDistance = 2;
    [SerializeField] private float walkingSpeed = 2;

    public bool IsDead { get; private set; }

    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform _player;

    private enum AnimationName
    {
        Idle,
        Walk,
        Attack,
        Dead
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        GameEngine.enemyList.Add(this.gameObject);
    }

    private void ActivateAnimationClip(AnimationName animationName)
    {
        _animator.SetBool(AnimationName.Idle.ToString(), false);
        _animator.SetBool(AnimationName.Walk.ToString(), false);
        _animator.SetBool(AnimationName.Attack.ToString(), false);

        _animator.SetBool(animationName.ToString(), true);

    }

    public void DamagePlayer()
    {
        HealthBar.Instance.TakeDamage(damage);
    }

    public void Dead()
    {
        if (!IsDead)
        {
            _animator.SetTrigger(AnimationName.Dead.ToString());
        }

        IsDead = true;
    }


    private void LateUpdate()
    {
        if (IsDead)
        {
            _agent.enabled = false;
            return;
        }

        var distance = Vector3.Distance(transform.position, _player.position);

        if (!HealthBar.Instance.isAlive)
        {
            _agent.updatePosition = false;
            ActivateAnimationClip(AnimationName.Idle);
            return;
        }


        _agent.SetDestination(_player.position);




        if (distance >= attackingDistance)
        {
            ActivateAnimationClip(AnimationName.Walk);
            _agent.speed = walkingSpeed;
        }
        else
        {
            ActivateAnimationClip(AnimationName.Attack);
        }

    }
}