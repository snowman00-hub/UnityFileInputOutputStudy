using UnityEngine;

public class Sample : MonoBehaviour
{
    public Transform panel;
    public Transform scrollView;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            panel.SetAsLastSibling();
        }

        if(Input.GetKey(KeyCode.Alpha2))
        {
            scrollView.SetAsLastSibling();
        }
    }
}
