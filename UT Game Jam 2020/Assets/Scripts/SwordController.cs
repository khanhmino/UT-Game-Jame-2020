using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public GameObject sword;
    public float pickupRange;
    public LayerMask pickupLayers;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PointToMouse();
        LookForPickups();
        if (Input.GetMouseButtonDown(0))
        {
            SwingBlade();
        }
    }

    private GameObject[] LookForPickups()
    {
        Collider2D[] foundPickups = Physics2D.OverlapCircleAll(transform.position, pickupRange, pickupLayers);
        GameObject[] pickupables = new GameObject[foundPickups.Length];
        for(int i = 0; i < pickupables.Length; i++)
        {
            print("Found pickup " + i);
            pickupables[i] = foundPickups[i].gameObject;
        }
        return pickupables;
    }

    private void PointToMouse()
    {
        Vector3 mousePos;
        Vector3 objPos;


        mousePos = Input.mousePosition;
        mousePos.z = 10; //The distance between the camera and object
        objPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objPos.x;
        mousePos.y = mousePos.y - objPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    public void PickUpBlade(PickupController pickup)
    {

    }

    public void DiscardBlade()
    {

    }
    public void SwingBlade()
    {
        anim.SetTrigger("Swinging");
    }
}
