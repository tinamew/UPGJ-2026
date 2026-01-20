using UnityEngine;

public class CameraMovement : MonoBehaviour
{
   public static CameraMovement instance {  get; private set; }

    private Animator cm_animator;
    private bool isFocusedOnObject = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        cm_animator = GetComponent<Animator>();
    }


    void Update()
    {
        
    }

    public void CameraOnPhoto()
    {
        if (!isFocusedOnObject)
        {
            cm_animator.SetTrigger("isFocused");
            isFocusedOnObject = true;
        }
        else
        {
            cm_animator.SetTrigger("unFocused");
            isFocusedOnObject = false;
        }
    }

    public void CameraOnAmulet()
    {

    }
}
