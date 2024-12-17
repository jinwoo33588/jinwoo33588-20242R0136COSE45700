using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plaercon : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform carmeraArm;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = characterBody.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
    }
    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude !=0;
        animator.SetBool("isMove",isMove);
        if(isMove)
        {
            Vector3 lookFoward = new Vector3(carmeraArm.forward.x, 0f, carmeraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(carmeraArm.right.x, 0f, carmeraArm.right.z).normalized;
            Vector3 moveDir = lookFoward * moveInput.y + lookRight * moveInput.x;

            //characterBody.forward = lookFoward;
            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * 5f;
        }
        Debug.DrawRay(carmeraArm.position, new Vector3 (carmeraArm.forward.x, 0f, carmeraArm.forward.z).normalized);
    }
    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = carmeraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;
        if(x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x,335f, 361f);
        }

        carmeraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
