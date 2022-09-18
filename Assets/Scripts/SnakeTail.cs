using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Transform SnakeHead;
    public float Diameter;

    private List<Transform> Tail = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();

    private void Awake()
    {
        positions.Add(SnakeHead.position);
    }

    private void Update()
    {
        float distance = ((Vector3)SnakeHead.position - positions[0]).magnitude;

        if (distance > Diameter)
        {
            // Направление от старого положения головы, к новому
            Vector3 direction = ((Vector3)SnakeHead.position - positions[0]).normalized;

            positions.Insert(0, positions[0] + direction * Diameter);
            positions.RemoveAt(positions.Count - 1);

            distance -= Diameter;
        }

        for (int i = 0; i < Tail.Count; i++)
        {
            Tail[i].position = Vector3.Lerp(positions[i + 1], positions[i], distance / Diameter);
        }
    }

    public void AddTail()
    {
        Transform Sphere = Instantiate(SnakeHead, positions[positions.Count - 1], Quaternion.identity, transform);
        Tail.Add(Sphere);
        positions.Add(Sphere.position);
    }

    public void RemoveTail()
    {
        Destroy(Tail[0].gameObject);
        Tail.RemoveAt(0);
        positions.RemoveAt(1);
    }

}
