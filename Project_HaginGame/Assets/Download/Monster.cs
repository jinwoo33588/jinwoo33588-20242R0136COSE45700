using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
   Transform target;   
   NavMeshAgent nmAgent;
   Animator anim;

   float HP = 0;
   public float lostDistance;

   enum State
   {
      IDLE,
      CHASE,
      ATTACK,
      KILLED
   }

   State state;

   // Start is called before the first frame update
   void Start()
   {
      anim = GetComponent<Animator>();
      nmAgent = GetComponent<NavMeshAgent>();

      HP = 10;
      state = State.IDLE;
      StartCoroutine(StateMachine());
   }

   IEnumerator StateMachine()
   {      
      while (HP > 0)
      {
         yield return StartCoroutine(state.ToString());
      }
   }

   IEnumerator IDLE()
   {
      // ���� animator �������� ���
      var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

      // �ִϸ��̼� �̸��� IdleNormal �� �ƴϸ� Play
      if (curAnimStateInfo.IsName("IdleNormal") == false)
         anim.Play("IdleNormal", 0, 0);

      // ���Ͱ� Idle ������ �� �θ��� �Ÿ��� �ϴ� �ڵ�
      // 50% Ȯ���� ��/��� ���� ����
      int dir = Random.Range(0f, 1f) > 0.5f ? 1 : -1;

      // ȸ�� �ӵ� ����
      float lookSpeed = Random.Range(25f, 40f);

      // IdleNormal ��� �ð� ���� ���ƺ���
      for (float i=0; i< curAnimStateInfo.length; i+=Time.deltaTime)
      {
         transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y + (dir) * Time.deltaTime * lookSpeed, 0f);

         yield return null;
      }
   }

   IEnumerator CHASE()
   {
      var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);

      if (curAnimStateInfo.IsName("WalkFWD") == false)
      {
         anim.Play("WalkFWD", 0, 0);
         // SetDestination �� ���� �� frame�� �ѱ������ �ڵ�
         yield return null;
      }

      // ��ǥ������ ���� �Ÿ��� ���ߴ� �������� �۰ų� ������
      if (nmAgent.remainingDistance <= nmAgent.stoppingDistance)
      {
         // StateMachine �� �������� ����
         ChangeState(State.ATTACK);
      }
      // ��ǥ���� �Ÿ��� �־��� ���
      else if (nmAgent.remainingDistance > lostDistance)
      {
         target = null;         
         nmAgent.SetDestination(transform.position);
         yield return null;
         // StateMachine �� ���� ����
         ChangeState(State.IDLE);
      }
      else
      {  
         // WalkFWD �ִϸ��̼��� �� ����Ŭ ���� ���
         yield return new WaitForSeconds(curAnimStateInfo.length);
      }
   }

   IEnumerator ATTACK()
   {
      var curAnimStateInfo = anim.GetCurrentAnimatorStateInfo(0);
      
      // ���� �ִϸ��̼��� ���� �� Idle Battle �� �̵��ϱ� ������ 
      // �ڵ尡 �� ������ ���� ������ Attack01 �� Play
      anim.Play("Attack01", 0, 0);

      // �Ÿ��� �־�����
      if (nmAgent.remainingDistance > nmAgent.stoppingDistance)
      {
         // StateMachine�� �������� ����
         ChangeState(State.CHASE);
      }
      else
         // ���� animation �� �� �踸ŭ ���
         // �� ��� �ð��� �̿��� ���� ������ ������ �� ����.
         yield return new WaitForSeconds(curAnimStateInfo.length * 2f);
   }

   IEnumerator KILLED()
   {
      yield return null;
   }

   void ChangeState(State newState)
   {
      state = newState;
   }

   private void OnTriggerEnter(Collider other)
   {      
      if (other.name != "Player") return;
      // Sphere Collider �� Player �� �����ϸ�      
      target = other.transform;
      // NavMeshAgent�� ��ǥ�� Player �� ����
      nmAgent.SetDestination(target.position);
      ChangeState(State.CHASE);
   }

   // Update is called once per frame
   void Update()
   {
      if (target == null) return;

      nmAgent.SetDestination(target.position);
   }
}
