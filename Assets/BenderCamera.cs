//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using UnityEngine;
[ExecuteInEditMode]
public class BenderCamera : MonoBehaviour
{
    [Range(0, 0.5f)]
    public float extraCullHeight;
    public Camera _camera;

    private void Start()
    {
        if (_camera == null)
            _camera = GetComponent<Camera>();

        OnPreCull();
    }

    void Update() {
        OnPreCull();
    }

    private void OnPreCull()
    {
        float ar = _camera.aspect;
        float fov = _camera.fieldOfView;
        float viewPortHeight = Mathf.Tan(Mathf.Deg2Rad * fov * 0.5f);
        float viewPortwidth = viewPortHeight * ar;

        float newfov = fov * (1 + extraCullHeight);
        float newheight = Mathf.Tan(Mathf.Deg2Rad * newfov * 0.5f);
        float newar = viewPortwidth / (newheight);

        _camera.projectionMatrix = Matrix4x4.Perspective(newfov, newar, _camera.nearClipPlane, _camera.farClipPlane);
    }

    private void OnPreRender()
    {
        _camera.ResetProjectionMatrix();
    }
}