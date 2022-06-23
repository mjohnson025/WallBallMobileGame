using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float lives = 3f;
    Rigidbody _rb; 
    bool started;
    bool gameOver = false;
    public GameManager gameManager;
    public GameObject diamondParticle;
    public GameObject multiParticle;
    public int score;
    private Animator animator;
    private Animator anim2;
    private Spawner spawner;
    bool dead = false;
    Vector3 lastBallPos;


   void Awake() {
        _rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();    
        animator = GameObject.Find("Tap to Begin").GetComponent<Animator>();
        anim2 = GameObject.Find("TitleScreen").GetComponent<Animator>();
        gameManager.UpdateLives(lives);
    }

    // Update is called once per frame
    void Update()
    {
        if (!started && !gameManager.gameOver){
            if(Input.GetMouseButtonDown(0)){
                 _rb.velocity = new Vector3 (_speed, 0, 0);
                 started = true;
                 dead = false;
                 animator.SetTrigger("gameStart");
                 anim2.SetTrigger("gameStart");
               

            }
        }

        if(!Physics.Raycast(transform.position, Vector3.down, 1f)){
/*                 if(lives >=2 && !dead){
                    transform.position = lastBallPos;
                    Debug.Log(lastBallPos);
                    //transform.position = new Vector3(-.079f, .5f, .079f);
                    lives --;
                }
                else if (lives == 1){

                    Camera.main.GetComponent<CameraFollow>().gameOver = true;
                    gameManager.GameOver(score);
                } */
           // transform.position = new Vector3(-.079f, .5f, .079f);
            Camera.main.GetComponent<CameraFollow>().gameOver = true;
            gameManager.GameOver(score);
            started = false;
            dead = true;
            //gameManager.UpdateLives(lives);
            _rb.velocity = new Vector3(0, -25f, 0);

            
        }
        
       // CalculateMovement();
       if (Input.GetMouseButtonDown(0) && !gameManager.gameOver){
           SwitchDirection();
           Debug.DrawRay(transform.position, Vector3.down, Color.red);
       }
       if (score >= 50 && score < 100){
           _speed = 6f;
       }
       else if(score >= 100 && score < 150){
           _speed = 7f;
       }
        else if(score >= 150 && score < 200){
           _speed = 8f;
       }
        else if(score >= 200){
           _speed = 9f;
       }
    }


    void SwitchDirection(){
        if (_rb.velocity.z > 0){
            _rb.velocity = new Vector3(_speed, 0, 0);
        }

        else if(_rb.velocity.x > 0) {
            _rb.velocity= new Vector3(0, 0, _speed);
        }
    }

    //TODO Make this a case statement for multiple item types. Alternately, move this logic to the items' script.
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag);
        if (other.tag == "Diamond"){
           GameObject diamondPart = Instantiate(diamondParticle, other.gameObject.transform.position, Quaternion.identity) as GameObject;
            score++;
            gameManager.UpdateScore(score);
            Destroy(other.gameObject);
            Destroy(diamondPart, 1f);
        }
        else if(other.tag == "multi"){
            Debug.Log("multi");
            GameObject diamondPart = Instantiate(multiParticle, other.gameObject.transform.position, Quaternion.identity) as GameObject;
            score+=10;
            gameManager.UpdateScore(score);
            Destroy(other.gameObject);
            Destroy(diamondPart, 1f);
        }
        
    }
}
