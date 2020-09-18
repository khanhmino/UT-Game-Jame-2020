using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public GameObject sword;
    public float pickupRange;
    public LayerMask pickupLayers;
    public Animator anim;
    public bool swinging;



    //Outline vars
    public GameObject outlinedPickup = null;
    public Material noAlpha;
    public Material outline;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PointToMouse();
        FindClosestPickup().GetComponentInChildren<SpriteRenderer>();
        if (Input.GetMouseButtonDown(0))
        {
            SwingBlade();
        }


        GameObject newClosestOutline = FindClosestPickup();
        if(newClosestOutline != outlinedPickup)
        {
            outlinedPickup.GetComponentInChildren<SpriteRenderer>().material = noAlpha;
            outlinedPickup = newClosestOutline;
            outlinedPickup.GetComponentInChildren<SpriteRenderer>().material = outline;
        }

    }

    /*
     * Returns a GameObject that is the closest pickup within pickRange of the sword
     * Returns NULL if no pickups are nearby
     */ 
    private GameObject FindClosestPickup()
    {
        Collider2D[] foundPickups = Physics2D.OverlapCircleAll(transform.position, pickupRange, pickupLayers);

        GameObject closestPickup = null;
        float distFromClosest = pickupRange * 2;
        for(int i = 0; i < foundPickups.Length; i++)
        {
            var distFromi = Vector2.Distance(foundPickups[i].transform.position, transform.position);
            if(distFromi < distFromClosest)
            {
                closestPickup = foundPickups[i].gameObject;
                distFromClosest = distFromi;
            }
        }
        return closestPickup;
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
