using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Framework.AIScripts.AIStateMachineScripts
{
    public enum AIStates
    {
        Wonder,
        Target,
        Attack,
        Flee,
        Death
    }
 
   public delegate void StateDelegate();

    public class StateMachine : MonoBehaviour
    {
        
        private Dictionary<AIStates, StateDelegate> states = new Dictionary<AIStates, StateDelegate>();
 
        [SerializeField] private AIStates currentStateEnemey = AIStates.Wonder;
        [SerializeField] private AIStates currentStateCompanion = AIStates.Target;
        [SerializeField] private Transform switchState;
        [SerializeField] private Transform player;
        [SerializeField] private Transform WayPointParent;
        [SerializeField] private Transform[] wayPoints;
        [SerializeField] private int curWaypoint;
        [SerializeField] private float walkSpeed, runSpeed, SightRange;
        [SerializeField] private NavMeshAgent agent; 
        [SerializeField] private float distanceToPoint, changePoint;
        [SerializeField] private bool isDead;
        
        public void ChangeStateEnemey(AIStates _newState)
        {
            if(_newState != currentStateEnemey)
            {
                currentStateEnemey = _newState;
            }

        }
        public void ChangeStateCompanion(AIStates _newState)
        {
            if(_newState != currentStateCompanion)
            {
                currentStateCompanion = _newState;
            }

        }
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            wayPoints = WayPointParent.GetComponentsInChildren<Transform>();

            states.Add(AIStates.Wonder, Wonder);
            states.Add(AIStates.Target, Target);
            states.Add(AIStates.Attack, Attack);
            states.Add(AIStates.Flee, Flee);
            states.Add(AIStates.Death, Death);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void Wonder()
        {
            if (wayPoints.Length <= 0 || Vector3.Distance(player.position, transform.position) <= SightRange || isDead)
            {
                return;
            }
            currentStateEnemey = AIStates.Wonder;
            
            //Set agent to target
            agent.destination = wayPoints[curWaypoint].position;
            distanceToPoint = Vector3.Distance(transform.position, wayPoints[curWaypoint].position);
            //are we at the waypoint
            if (distanceToPoint <= changePoint)
            {
                //if so go to next  waypoint
                if (curWaypoint < wayPoints.Length - 1)
                {
                    curWaypoint++;
                }
                //if at end of patrol go to start
                else
                {
                    curWaypoint = 1;
                }
            }
            agent.speed = walkSpeed;

        }

        private void Target()
        {
            
        }

        private void Attack()
        {
            
        }

        private void Flee()
        {
            
        }

        private void Death()
        {
            
        }
        
        
    }
    
   
}
