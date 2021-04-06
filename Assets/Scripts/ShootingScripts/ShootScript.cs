using Photon.Pun;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public float Speed = 20;
    public GameObject Explosion;
    public float Delta = 10.0f;

    private bool _isShooting = false;
    private Rigidbody _rb;
    private Vector3 _startPosition;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startPosition = transform.position;
    }

    private void OnTriggerEnter( Collider other )
    {
        Debug.Log( "Попал! " + other.name );

        Debug.Log( transform.position );

        Explosion.transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.z );
        Explosion.GetComponent<ParticleSystem>().Play();

        _rb.constraints = RigidbodyConstraints.FreezePosition;
        transform.position = _startPosition;
        _isShooting = false;
    }

    public void MakeShoot( GameObject ship )
    {
        if ( !_isShooting )
        {
            _isShooting = true;
            _rb.constraints = RigidbodyConstraints.None;

            Vector3 forceVector = new Vector3( ship.transform.position.x - transform.position.x + Delta, 10 - transform.position.y, ship.transform.position.z - transform.position.z + Delta ) * Speed;

            _rb.AddForce( forceVector, ForceMode.Force );
        }
    }

    public void Respawn()
    {
        _rb.constraints = RigidbodyConstraints.FreezePosition;
        transform.position = _startPosition;
        _isShooting = false;
    }
}
