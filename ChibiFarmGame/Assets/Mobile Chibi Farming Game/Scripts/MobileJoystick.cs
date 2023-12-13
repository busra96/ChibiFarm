using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{

    [Header(" Elements ")] 
    [SerializeField] private RectTransform JoystickOutline;
    [SerializeField] private RectTransform JoystickKnob;


    [Header(" Settings ")] 
    [SerializeField] private float moveFactor;
    private Vector3 move;
    private Vector3 clickedPosition;
    private bool canControl;
    
    // Start is called before the first frame update
    void Start()
    {
        HideJoystick();
    }

    // Update is called once per frame
    void Update()
    {
        if(canControl)
            ControlJoystick();
    }


    public void ClickedOnJoystickZoneCallback()
    {
        clickedPosition = Input.mousePosition;
        JoystickOutline.position = clickedPosition;
        
        ShowJoystick();
    }
    
    private void ShowJoystick()
    {
        JoystickOutline.gameObject.SetActive(true);
        canControl = true;
    }

    private void HideJoystick()
    {
        JoystickOutline.gameObject.SetActive(false);
        canControl = false;
    }

    private void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - clickedPosition;

        float moveMagnitude = direction.magnitude * moveFactor / Screen.width;

        moveMagnitude = Mathf.Min(moveMagnitude, JoystickOutline.rect.width / 2);
        
        move = direction.normalized * moveMagnitude;

        Vector3 targetPosition = clickedPosition + move;
        
        JoystickKnob.position = targetPosition;
        
        if(Input.GetMouseButtonUp(0))
            HideJoystick();

    }

    public Vector3 GetMoveVector()
    {
        return move;
    }
}
