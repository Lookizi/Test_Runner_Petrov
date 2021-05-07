using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    private CharacterController charController;
    private AudioSource menuMusic;
    private AudioSource playerSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip takeABullet;
    private Animator animator;
    private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject menu;
    public int bullets = 0;
    private const int HeartsCount = 3;
    private static int Health
    {
        get => PlayerPrefs.GetInt("health", HeartsCount);
        set => PlayerPrefs.SetInt("health", value);
    }
    private static int Bullets
    {
        get => PlayerPrefs.GetInt("bullets", 0);
        set => PlayerPrefs.SetInt("bullets", value);
    }
    public Sprite fullLive;
    public Sprite emptyLive;
    public Image[] hearts;
    [SerializeField] private Text bulletsText;
    [SerializeField] private Score scoreScript;
    private int lineToMove = 1;
    [SerializeField] private float lineDist = 4;
    [SerializeField] private float maxSpeed = 90;
    // private GameObject healthIcon;
    
    // Start is called before the first frame update
    private void Start()
    {
        PlayerPrefs.SetInt("bullets",0);
        animator = GetComponentInChildren<Animator>();
        charController = GetComponent<CharacterController>();
        Time.timeScale = 1;
        
        if (Health <= 0)
        {
            Health = HeartsCount;
        }

        menuMusic = menu.GetComponent<AudioSource>();
        playerSource = GetComponent<AudioSource>();
        playerSource.Play();
        StartCoroutine(SpeedIncrease());
    }

    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        if (SwipeController.swipeUp)
        {
            Jump();
        }
        

        var transformInt = transform;
        var position = transformInt.position;
        var targetPos = position.z * transformInt.forward + position.y * transformInt.up;
        
        switch (lineToMove)
        {
            case 0:
                targetPos += Vector3.left * lineDist;
                break;
            case 2:
                targetPos += Vector3.right * lineDist;
                break;
            case 1 when targetPos.x > 0:
                targetPos -= Vector3.right * lineDist;
                break;
            case 1 when targetPos.x < 0:
                targetPos -= Vector3.left * lineDist;
                break;
        }

        if (transform.position == targetPos)
        {
            return;
        }

        var difference = targetPos - transform.position;
        var moveDir = difference.normalized * (25 * Time.deltaTime);

        charController.Move(moveDir.sqrMagnitude < difference.sqrMagnitude ? moveDir : difference);

        if (!animator.GetBool("Run"))
        {
            playerSource.Stop();
        }
        
    }

    private void Jump()
    {
        if (charController.isGrounded)
        {
            animator.SetTrigger("Jump");
            direction.y = jumpForce;
        }
        playerSource.PlayOneShot(jumpSound);
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        direction.z = speed;
        direction.y += gravity * Time.fixedDeltaTime;
        charController.Move(direction * Time.fixedDeltaTime);
        
        for (var i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < Health ? fullLive : emptyLive;
        }
        
        bulletsText.text = PlayerPrefs.GetInt("bullets").ToString();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("obstacle"))
        {
            print("hit");
            playerSource.Stop();
            StopCoroutine(SpeedIncrease());
            speed = 0;
            Health -= 1;
            if (Health <= 0)
            {
                animator.SetBool("Run", false);
                animator.SetTrigger("Death");
                StartCoroutine(Restart());
                
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            playerSource.PlayOneShot(takeABullet);
            bullets = 30;
            PlayerPrefs.SetInt("bullets", bullets);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator SpeedIncrease()
    {
        if (speed <= maxSpeed)
        {
            yield return new WaitForSeconds(2);
            speed += 1;
            StartCoroutine(SpeedIncrease());
        }
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);
        menu.SetActive(true);
        menuMusic.Play();
        int lastGameScore = int.Parse(scoreScript.scoreText.text.ToString());
        PlayerPrefs.SetInt("lastGameScore", lastGameScore);
        Time.timeScale = 0;
    }
}
