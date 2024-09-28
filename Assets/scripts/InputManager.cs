using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputManager : MonoBehaviour
{
    Collider2D hitObj = null;

    private Camera _maincamera;

    //���� �׼ǿ� ���ǵ� �׼�
    private InputAction click;
  

    private void Awake()
    {
        //�׼� ����
        var inputActions = new InputActionMap("");

        //passthrough ����
        click = inputActions.AddAction("Click", binding: "<Mouse>/leftButton", type: InputActionType.PassThrough);

        click.performed += OnClick;
        click.canceled += OnRelease;

        click.Enable();

        _maincamera = Camera.main;
       // _playerinput =gameObject.AddComponent<PlayerInput>();

    }
    private void OnClick(InputAction.CallbackContext context)
    {
      
            var rayhit = Physics2D.GetRayIntersection(_maincamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
            if (!rayhit.collider) return;
              if (hitObj == null)
              {
                    hitObj = rayhit.collider;
                   Debug.Log(rayhit.collider.gameObject.name);
              }

    }

    private void OnRelease(InputAction.CallbackContext context)
    {
        hitObj=null;
        Debug.Log("Release");
    }




}

