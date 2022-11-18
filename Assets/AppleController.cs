using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    private Transform _transform;
    private GameObject _gameObject;

    private PhotonView _view;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _gameObject = gameObject;
        _view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (_view.IsMine)
        {
            if (Input.GetMouseButton(0 /* Left */))
            {
                Debug.Log("Vao get mouse");
                var position = transform.position;
                _transform.position = new Vector3((Input.mousePosition.x - position.x) / 100, (Input.mousePosition.y - position.y) / 100, 0);
                return;
            }
            // Vector3 input = new Vector3((Input.GetAxisRaw("Horizontal")), Input.GetAxisRaw("Horizontal"), 0);
            // _transform.position += input.normalized * Time.deltaTime;
        }
        
    }
}
