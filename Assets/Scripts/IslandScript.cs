using Photon.Pun;
using UnityEngine;

public class IslandScript : MonoBehaviour
{
    public bool IsAlive = true;
    public GameObject Fire;
    public GameObject Explosion;

    public void Crash()
    {
        IsAlive = false;
        Fire.GetComponent<ParticleSystem>().Play();
        Explosion.GetComponent<ParticleSystem>().Play();
    }
}
