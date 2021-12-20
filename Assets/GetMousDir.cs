using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMousDir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public Rigidbody rb;


    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = this.transform.position.z;

        Vector3 direction = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        direction.z = 0;

        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.velocity = direction;
            
        }
        

        Debug.Log(Methods.ZDistanceMousePos(Camera.main.transform.position.z).ChangeZ(0));

        Debug.DrawRay(transform.position, ((-Methods.ZDistanceMousePos(Camera.main.transform.position.z)).normalized * 5f).ChangeZ(this.transform.position.z));
        
    }

}
