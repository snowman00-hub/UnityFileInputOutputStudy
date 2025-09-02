using UnityEngine;

public class Movement : MonoBehaviour
{
    public static readonly string AxisHorizontal = "Horizontal";
    public static readonly string AxisVertical = "Vertical";

    public float speed = 5f;
    
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float h = Input.GetAxis(AxisHorizontal);
        float v = Input.GetAxis(AxisVertical);

        Vector3 dir = new Vector3(h, 0, v);
        rb.linearVelocity = dir.normalized * speed;
    }
}
