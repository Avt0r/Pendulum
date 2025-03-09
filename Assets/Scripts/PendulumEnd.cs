using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FixedJoint2D),typeof(BallSpawner))]
public class PendulumEnd : MonoBehaviour
{
    [SerializeField]
    private FixedJoint2D _joint;
    [SerializeField]
    private BallSpawner _spawner;

    public void LetGo()
    {
        if (transform.childCount == 0) return;

        _joint.enabled = false;
        transform.GetChild(0).parent = null;
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.5f);
        _spawner.Spawn();
    }
   
}
