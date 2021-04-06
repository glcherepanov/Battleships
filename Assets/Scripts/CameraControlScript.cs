using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControlScript : MonoBehaviour
{
    public GameObject Camera;

    private bool _isWPressed = false;
    private bool _isAPressed = false;
    private bool _isSPressed = false;
    private bool _isDPressed = false;
    private bool _isQPressed = false;
    private bool _isEPressed = false;
    private bool _isShiftPressed = false;
    private bool _isSpacePressed = false;
    private float _speed = 1F; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPressedButtons();
        if ( _isAPressed )
        {
            Camera.transform.position = new Vector3( Camera.transform.position.x - _speed, Camera.transform.position.y, Camera.transform.position.z );
        }

        if ( _isDPressed )
        {
            Camera.transform.position = new Vector3( Camera.transform.position.x + _speed, Camera.transform.position.y, Camera.transform.position.z );
        }

        if ( _isWPressed )
        {
            Camera.transform.position = new Vector3( Camera.transform.position.x, Camera.transform.position.y, Camera.transform.position.z + _speed );
        }

        if ( _isSPressed )
        {
            Camera.transform.position = new Vector3( Camera.transform.position.x, Camera.transform.position.y, Camera.transform.position.z - _speed );
        }

        if ( _isQPressed )
        {
            Camera.transform.rotation = Quaternion.Euler( Camera.transform.rotation.eulerAngles.x, Camera.transform.rotation.eulerAngles.y - _speed, Camera.transform.rotation.eulerAngles.z );
        }

        if ( _isEPressed )
        {
            Camera.transform.rotation = Quaternion.Euler( Camera.transform.rotation.eulerAngles.x, Camera.transform.rotation.eulerAngles.y + _speed, Camera.transform.rotation.eulerAngles.z );
        }

        if ( _isShiftPressed )
        {
            Camera.transform.position = new Vector3( Camera.transform.position.x, Camera.transform.position.y - _speed, Camera.transform.position.z );
        }

        if ( _isSpacePressed )
        {
            Camera.transform.position = new Vector3( Camera.transform.position.x, Camera.transform.position.y + _speed, Camera.transform.position.z );
        }
    }

    private void CheckPressedButtons()
    {
        if ( Input.GetKeyDown( KeyCode.A ) )
        {
            _isAPressed = true;
        }
        if ( Input.GetKeyDown( KeyCode.D ) )
        {
            _isDPressed = true;
        }
        if ( Input.GetKeyDown( KeyCode.W ) )
        {
            _isWPressed = true;
        }
        if ( Input.GetKeyDown( KeyCode.S ) )
        {
            _isSPressed = true;
        }
        if ( Input.GetKeyDown( KeyCode.Q ) )
        {
            _isQPressed = true;
        }
        if ( Input.GetKeyDown( KeyCode.E ) )
        {
            _isEPressed = true;
        }
        if ( Input.GetKeyDown( KeyCode.LeftShift ) )
        {
            _isShiftPressed = true;
        }
        if ( Input.GetKeyDown( KeyCode.Space ) )
        {
            _isSpacePressed = true;
        }

        if ( Input.GetKeyUp( KeyCode.A ) )
        {
            _isAPressed = false;
        }
        if ( Input.GetKeyUp( KeyCode.D ) )
        {
            _isDPressed = false;
        }
        if ( Input.GetKeyUp( KeyCode.W ) )
        {
            _isWPressed = false;
        }
        if ( Input.GetKeyUp( KeyCode.S ) )
        {
            _isSPressed = false;
        }
        if ( Input.GetKeyUp( KeyCode.Q ) )
        {
            _isQPressed = false;
        }
        if ( Input.GetKeyUp( KeyCode.E ) )
        {
            _isEPressed = false;
        }
        if ( Input.GetKeyUp( KeyCode.LeftShift ) )
        {
            _isShiftPressed = false;
        }
        if ( Input.GetKeyUp( KeyCode.Space ) )
        {
            _isSpacePressed = false;
        }
    }
}
