using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private int _score = 0;
    [SerializeField] private ParticleSystem _particle;

    public int Score => _score;

    private void Start()
    {
        _particle.Stop();
    }

    public void MakeABoom()
    {
        StartCoroutine(Boom());
    }

    private IEnumerator Boom()
    {
        yield return new WaitForSeconds(1f);
        
        _particle.Play();
        Destroy(gameObject, 0.5f);
    }
}
