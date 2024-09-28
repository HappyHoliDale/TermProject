using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputManager : MonoBehaviour
{
    Collider2D hitObj = null;

    private Camera _maincamera;

    //에셋 액션에 정의된 액선
    private InputAction click;
  

    private void Awake()
    {
        //액션 참조
        var inputActions = new InputActionMap("");

        //passthrough 설정
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

