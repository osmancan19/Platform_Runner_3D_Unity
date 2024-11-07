using UnityEngine;

public class UIPainterManager : MonoBehaviour
{
    PaintableObject[] paintableObjects;
    private bool fingerSlided;

    void Start()
    {
        fingerSlided = false;
        paintableObjects = FindObjectsOfType<PaintableObject>();
    }

    void Update()
    {
        toggleObjects(false);

        if (Input.GetMouseButton(0))
        {          

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                PaintableObject painterManager = hit.transform.gameObject.GetComponent<PaintableObject>();
                if (painterManager != null)
                {
                    if(!fingerSlided && UIManager.Instance.IsFingerSlideActive())
                    {
                        fingerSlided = true;
                        UIManager.Instance.FingerSlided();
                    }
                    painterManager.PaintMask(hit.point, hit.normal);
                }
            }
        }

        toggleObjects(true);
    }

    private void toggleObjects(bool isToggle)
    {
        foreach (PaintableObject var in paintableObjects)
        {
            var.gameObject.GetComponent<Renderer>().enabled = isToggle;
        }
    }
}
