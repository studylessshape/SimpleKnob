using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSimulation : MonoBehaviour
{
    public GameObject KonbBut;

    public float rate = 0.01f;

    private KnobButton knob;
    private Vector2 viewUpRight;
    private Vector2 viewDownLeft;
    // Start is called before the first frame update
    void Start()
    {
        knob = KonbBut?.GetComponent<KnobButton>();
        viewUpRight = Camera.main.ViewportToWorldPoint(Vector2.one);
        viewDownLeft = Camera.main.ViewportToWorldPoint(Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (!knob.isEndDrag)
        {
            float euler = knob.curEuler;
            float radius = knob.curCircleRadius;
            transform.position += (Vector3)CalculateMoveVector(euler, radius) * radius * rate;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, euler - 90f));
        }
    }

    private Vector2 CalculateMoveVector(float euler, float radius)
    {
        Vector2 vector = Vector2.zero;
        vector.x = radius * Mathf.Cos(euler * Mathf.PI / 180f);
        vector.y = radius * Mathf.Sin(euler * Mathf.PI / 180f);
        return vector.normalized;
    }
}
