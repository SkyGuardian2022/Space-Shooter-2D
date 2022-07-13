using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{

    [SerializeField]
    private float _speed = 8.0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        
        if(transform.position.y > 7f)
        {
            
            if (transform.parent != null)
            {

                Destroy(transform.parent.gameObject);

            }
            //check if this object has a parent.
            //destroy parent too!
            Destroy(this.gameObject);
        }
    }
}
