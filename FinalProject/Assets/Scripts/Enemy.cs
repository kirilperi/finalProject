using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Zombie1,
    Zombie2,
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;

    [SerializeField] private int damage;
    [SerializeField] private float attackingDistance = 2;
    [SerializeField] private float walkingSpeed = 2;
    [SerializeField] private float runningSpeed = 3;

    public bool IsDead { get; private set; }

    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform _player;

    private enum AnimationName
    {
        Idle,
        Walk,
        Run,
        Attack,
        Dead
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set movement speed based on type
        _agent.speed = (enemyType == EnemyType.Zombie2) ? runningSpeed : walkingSpeed;
    }

    private void ActivateAnimationClip(AnimationName animationName)
    {
        if (IsDead) return; // Prevent animation changes after death

        _animator.SetBool(AnimationName.Idle.ToString(), false);
        _animator.SetBool(AnimationName.Walk.ToString(), false);
        _animator.SetBool(AnimationName.Run.ToString(), false);
        _animator.SetBool(AnimationName.Attack.ToString(), false);

        _animator.SetBool(animationName.ToString(), true);
    }

    public void DamagePlayer()
    {
        if (!IsDead)
        {
            HealthBar.Instance.TakeDamage(damage);
        }
    }

    public void Dead()
    {
        if (IsDead) return; // Avoid multiple calls to Dead()

        _animator.SetTrigger(AnimationName.Dead.ToString());
        IsDead = true;
        _agent.enabled = false; // Disable movement after death
        Destroy(gameObject, 2f); // Optional: Destroy the object after a delay
    }

    private void LateUpdate()
    {
        if (IsDead)
        {
            return; // No further actions if dead
        }

        if (!HealthBar.Instance.isAlive)
        {
            _agent.isStopped = true; // Stop movement if the player is dead
            ActivateAnimationClip(AnimationName.Idle);
            return;
        }

        var distance = Vector3.Distance(transform.position, _player.position);
        _agent.SetDestination(_player.position);

        // Behavior based on type and distance
        if (distance >= attackingDistance)
        {
            if (enemyType == EnemyType.Zombie2)
            {
                ActivateAnimationClip(AnimationName.Run);
                _agent.speed = runningSpeed;
            }
            else if (enemyType == EnemyType.Zombie1)
            {
                ActivateAnimationClip(AnimationName.Walk);
                _agent.speed = walkingSpeed;
            }
        }
        else
        {
            ActivateAnimationClip(AnimationName.Attack);
        }
    }

}
