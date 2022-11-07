using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    
    private Animator _anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        //null check player
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        
        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
 
            Player player = other.transform.GetComponent<Player>();
        if (player != null)
            {

                player.Damage();

            }

            if (_anim != null && _anim.isActiveAndEnabled)
            {
                _anim.Play("OnEnemyDeath");
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 1.5f);

        
        }


        if (other.tag == "Laser")
        {

            Destroy(other.gameObject);
        if (_player != null)
            {

                _player.AddScore(10);

            }
        if (_anim != null && _anim.isActiveAndEnabled)
            {
                _anim.Play("OnEnemyDeath");
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 1.5f);
        
        }




    }
}
