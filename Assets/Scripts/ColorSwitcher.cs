using UnityEngine;
using UnityEngine.UI;

public class ColorSwitcher : MonoBehaviour
{
    private GameObject createdObject;

    public Button RedBtn;
    public Button GreenBtn;
    public Button BlueBtn;

    void Start()
    {
        RedBtn.onClick.AddListener(() => ApplyColor(Color.red));
        GreenBtn.onClick.AddListener(() => ApplyColor(Color.green));
        BlueBtn.onClick.AddListener(() => ApplyColor(Color.blue));
    }

    void ApplyColor(Color colorToApply)
    {
        if (createdObject != null)
        {
            Renderer objRenderer = createdObject.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                objRenderer.material.color = colorToApply;
            }
        }
    }

    public void AssignObject(GameObject obj)
    {
        createdObject = obj;
    }
}
