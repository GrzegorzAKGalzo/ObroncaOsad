using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private WolfDetection wolfDetection;
    private bool IsRunning;
    private Transform target;
    Animator m_Animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        wolfDetection = GetComponent<WolfDetection>();
        wolfDetection.OnAggro += WolfDetection_OnAggro;
        m_Animator = GetComponent<Animator>();
    }

    private void WolfDetection_OnAggro(Transform target)
    {
        this.target = target;
        IsRunning = true;
        m_Animator.SetBool("IsRunning", IsRunning);
    }

    private void Update()
    {
        if (target != null)
        {
            navMeshAgent.SetDestination(target.position);
            IsRunning = true;
            m_Animator.SetBool("IsRunning", IsRunning);
          
        }
        else
        {
            IsRunning = false;
            m_Animator.SetBool("IsRunning", IsRunning);
        }

   
    }
}
