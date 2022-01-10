using UI;
using UnityEngine;

/// <summary>
/// This script handle all the control code, so detecting when the users click on a unit or building and selecting those
/// If a unit is selected it will give the order to go to the clicked point or building when right clicking.
/// </summary>
public class UserControl : MonoBehaviour
{
    public Camera gameCamera;
    public float panSpeed = 10.0f;
    public GameObject marker;

    private Unit _mSelected ;

    private void Start()
    {
        marker.SetActive(false);
    }

    private void Update()
    {
        var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        gameCamera.transform.position += new Vector3(move.y, 0, -move.x) * (panSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            var ray = gameCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                //the collider could be children of the unit, so we make sure to check in the parent
                var unit = hit.collider.GetComponentInParent<Unit>();
                _mSelected = unit;

                //check if the hit object have a IUIInfoContent to display in the UI
                //if there is none, this will be null, so this will hid the panel if it was displayed
                var uiInfo = hit.collider.GetComponentInParent<UIMainScene.IUIInfoContent>();
                UIMainScene.Instance.SetNewInfoContent(uiInfo);
            }
        }
        else if (_mSelected != null && Input.GetMouseButtonDown(1))
        {
            //right click give order to the unit
            var ray = gameCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                var building = hit.collider.GetComponentInParent<Building>();

                if (building != null)
                {
                    _mSelected.GoTo(building);
                }
                else
                {
                    _mSelected.GoTo(hit.point);
                }
            }
        }

        MarkerHandling();
    }

    // Handle displaying the marker above the unit that is currently selected (or hiding it if no unit is selected)
    private void MarkerHandling()
    {
        if (_mSelected == null && marker.activeInHierarchy)
        {
            marker.SetActive(false);
            marker.transform.SetParent(null);
        }
        else if (_mSelected != null && marker.transform.parent != _mSelected.transform)
        {
            marker.SetActive(true);
            marker.transform.SetParent(_mSelected.transform, false);
            marker.transform.localPosition = Vector3.zero;
        }
    }
}