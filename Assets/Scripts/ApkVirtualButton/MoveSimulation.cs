using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSimulation : MonoBehaviour
{
    public KnobButton knob;
    public float rate = 0.01f;

    
    private Vector2 viewUpRight;
    private Vector2 viewDownLeft;
    // Start is called before the first frame update
    void Start()
    {
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
            Vector2 vector = CalculateMoveVector(euler, radius) * radius * rate * Time.deltaTime;
            Vector2 newPos = (Vector2)transform.position + vector;
            newPos.x = Mathf.Clamp(newPos.x, viewDownLeft.x, viewUpRight.x);
            newPos.y = Mathf.Clamp(newPos.y, viewDownLeft.y, viewUpRight.y);
            transform.position = newPos;
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
