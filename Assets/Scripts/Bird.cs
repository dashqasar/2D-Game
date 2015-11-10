using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    public static Bird instance;

    public bool isBirdMoving;

    public float force;

    public Vector3 initPos;
	public static bool Onetime = false;
    #endregion

    #region PRIVATE_VARIABLES
    /// <summary>
    /// This animatro reference
    /// </summary>
    private Animator thisAnimator;

    private AnimatorStateInfo stateInfo; 
    #endregion

    #region UNITY_CALLBACKS

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        instance = this;
        initPos = transform.position;
        thisAnimator = GetComponent<Animator>();

		Onetime = true;
    }


    // Update is called every frame, if the MonoBehaviour is enabled
    void Update()
    {
        stateInfo = thisAnimator.GetCurrentAnimatorStateInfo(0);
        if (GameManager.instance.isGameRunning && stateInfo.IsName("Base.BirdFlying") && rigidbody2D.velocity.y < 0)
        {
            thisAnimator.SetBool("IsFly", false);
        }
    }

    public void OnTap(){
        if (GameManager.instance.isGameRunning && !isBirdMoving)
        {
            SoundManager.instance.PlayBirdFly();
            GetComponent<SpriteRenderer>().sortingOrder = 3;
            thisAnimator.SetBool("IsFly", true);
            rigidbody2D.isKinematic = false;
            if (transform.parent != null)
                transform.parent = GameManager.instance.gamePlayPanel.transform;
            rigidbody2D.AddForce(Vector2.up * force);
            isBirdMoving = true;
        }
    }

    void FixedUpdate()
    {
        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Utility.GetPosition()), Vector2.zero);
        //if(hit.transform!=null)
        //    print(hit.transform.name);
        //if (GameManager.instance.isGameRunning && Utility.GetTouchState() && !isBirdMoving)
        //{
        //    SoundManager.instance.PlayBirdFly();
        //    GetComponent<SpriteRenderer>().sortingOrder = 3;
        //    thisAnimator.SetBool("IsFly", true);
        //    rigidbody2D.isKinematic = false;
        //    if (transform.parent != null)
        //        transform.parent = GameManager.instance.gamePlayPanel.transform;
        //    rigidbody2D.AddForce(Vector2.up * force);
        //    isBirdMoving = true;
        //}
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (rigidbody2D.velocity.y < 0 && other.transform.CompareTag("ExitPoint"))
        {
			if(Onetime)
			{
//				Debug.Log("sound");
				thisAnimator.SetBool("IsCrash",true);
				rigidbody2D.velocity = new Vector2(Random.Range(-2.5f,2.5f),0);
				rigidbody2D.gravityScale = 0.2f; 
				StartCoroutine("WaitAndActive");

					Onetime = false;
          	 	 GameManager.instance.GameComplete();
			}
        }
    }
    #endregion

    #region PRIVATE_METHODS
    #endregion

    #region PUBLIC_METHODS
    public void SetDefaultPosition() {
        transform.parent = GameManager.instance.gamePlayPanel.transform;
        transform.position = initPos;
        rigidbody2D.isKinematic = true;
        isBirdMoving = false;
        thisAnimator.SetBool("IsFly", false);
        thisAnimator.SetBool("IsCrash", false);
        thisAnimator.SetBool("IsEnd", true);

    }

    public void StopCo(){
        StopCoroutine("WaitAndActive");
        gameObject.SetActive(true);
    }
    #endregion

    #region COROUTINES
    IEnumerator WaitAndActive() {
        yield return new WaitForSeconds(0);
        gameObject.SetActive(false);
    }
    #endregion

}
