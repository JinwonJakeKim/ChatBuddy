using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target, target2;                                    // target to aim for
        private int target_flag, changed;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = false;
            agent.updatePosition = true;

            changed = 1;//target�� �ʱ⿡ �����ǰų� �ٲ���� ���� setDestination�� �ؼ� ȿ���� ���̱����� flag ����
            target_flag = 1;
        }


        private void Update()
        {
            if (changed == 1)
            {   //target�� 2������ ������ �� ������, flag�� ���� ĳ������ target�� �����ȴ�.
                if (target != null && target2 != null)
                {//target �ΰ��� �� ������ ���־����
                    if (target_flag == 1) agent.SetDestination(target.position);//�ʱ⿡�� target�� 1�� �����Ǿ��־� target1�� ����.
                    else if (target_flag == 2) agent.SetDestination(target2.position);
                }
            }
            //target�� ������ ���� �Ÿ��� ���� ��ġ�� �����Ÿ��� ���� ��� ĳ���ʹ� �����δ�. 
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false, false);
                changed = 0;
            }
            else
            {
                //character.Move(Vector3.zero, false, false);//�ƴϸ� ĳ���ʹ� �����.
                if (target_flag == 1) { target_flag = 2; changed = 1; }
                else if (target_flag == 2) { target_flag = 1; changed = 1; }
            }
        }
        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
