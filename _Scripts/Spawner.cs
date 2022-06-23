using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Vector3 lastPos;
    float size;
    public GameObject platform;
    public GameObject diamond;
    public GameObject multi;
    public bool gameOver;
    public static Spawner instance;
    public GameManager gameManager;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();    
        lastPos = platform.transform.position;
        size = platform.transform.localScale.x;
        for (int i=0; i < 20; i++){
            SpawnPlatforms();
        }
        InvokeRepeating("SpawnPlatforms", 2f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver){
            CancelInvoke("SpawnPlatforms");
        }
    }

    void SpawnPlatforms(){
        
        int rand = Random.Range(0, 6);
        if (rand < 3) {
            SpawnX();
        }
        else if(rand >= 3){
            SpawnZ();
        }
    }

    void SpawnX(){
        Vector3 pos = lastPos;
        pos.x += size;
        Instantiate(platform, pos, Quaternion.identity);
        lastPos = pos;
        SpawnItem(pos); 
    }

    void SpawnZ(){
        Vector3 pos = lastPos;
        pos.z += size;
        Instantiate(platform, pos, Quaternion.identity);
        lastPos = pos;
        SpawnItem(pos);       
    }

    void SpawnItem(Vector3 pos){
        //base diamond
        int random = Random.Range(0,20);
        if (random < 10){
            Instantiate(diamond, new Vector3(pos.x, pos.y +8, pos.z), diamond.transform.rotation);
        }
        else if(random >18){
            Instantiate(multi, new Vector3(pos.x, pos.y +8, pos.z), diamond.transform.rotation);
        }
        //begin spawning 5-point items once score is above 30
        //rare spawn of lives when score > 50?
    }
}
