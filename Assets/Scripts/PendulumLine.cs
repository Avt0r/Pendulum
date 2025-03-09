using UnityEngine;

[RequireComponent(typeof(LineRenderer),typeof(HingeJoint2D))]
public class PendulumLine : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _line_renderer;
    [SerializeField]
    private Transform _end_point;
    
    private void Update()
    {
        _line_renderer.SetPosition(0,_end_point.position);
        _line_renderer.SetPosition(1,transform.position);
    }
}
