using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform doorFrame;
    [SerializeField] private int diraction = 1;
    private bool isOpen;
    
    public void UseDoor()
    {
        Quaternion rotation = doorFrame.rotation;
        if (!isOpen)
        {
            rotation.eulerAngles += new Vector3(0, diraction*90, 0);
            doorFrame.rotation = rotation;
            isOpen = true;
        }
        else
        {
            rotation.eulerAngles -= new Vector3(0, diraction*90, 0);
            doorFrame.rotation = rotation;
            isOpen = false;
        }
    }

    public void View(TextMeshProUGUI text)
    {
        if (isOpen)
            text.text = $"������� F ����� ������� �����";
        else
            text.text = $"������� F ����� ������� �����";

    }
}
