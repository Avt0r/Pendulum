using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint2D))]
public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private FixedJoint2D _joint;
    [SerializeField]
    private List<Ball> _spawn_objects;
    [SerializeField]
    private List<Ball> _list;

    private void Start()
    {
        FillList();
        Spawn();
    }

    public void Spawn()
    {
        var b = Instantiate(_list[0].gameObject, transform);

        _joint.connectedBody = b.GetComponent<Rigidbody2D>();
        b.transform.position = transform.position;
        _joint.enabled = true;

        _list.RemoveAt(0);
        if( _list.Count == 0)
        {
            FillList();
        }
    }

    private void FillList()
    {
        _list = new List<Ball>();
        List<Ball> copy = new(_spawn_objects);

        for (int i = 0; i < _spawn_objects.Count; i++)
        {
            _list.Add(copy[new System.Random().Next(copy.Count)]);
            copy.Remove(_list[^1]);
        }
    }
}
