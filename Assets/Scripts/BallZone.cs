using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BallZone : MonoBehaviour
{
    [SerializeField]
    private int _zone_index;
    [SerializeField]
    private BallCollector _collector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out var ball)) _collector.AddBall(_zone_index, ball);
    }

}
