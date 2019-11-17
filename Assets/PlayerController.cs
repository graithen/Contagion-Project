using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    private PhotonView PV;

    //CHARACTER CONTROL
    public float movementSpeed = 2;
    public float sprintSpeed = 5;
    private bool sprinting;


    //CHARACTER GENERATION
    public GameObject charPortrait; //the char portrait active on the player avatar
    public Sprite[] charPortraitArray; //the portraits stored to be randomly assigned
    public int charPortraitNumber = 0;
    public TextMeshPro charName;
    public string[] charNameArray;
    public int charNameNumber = 0;
    public TextMeshPro Alias;

    //INFECTION
    public bool Infection = false;
    public Sprite InfectedFace;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            gameObject.name = "LocalPlayer";
            InitiateCharacter();
            //GetComponent<AudioListener>().enabled = true;
            //Instantiate(Camera, this.gameObject.transform);
        }
        if (!PV.IsMine)
        {
            gameObject.name = "Player" + gameObject.GetComponent<PhotonView>().ViewID;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
            ListenForInput();

        if (Infection)
            InfectPlayer();
    }

    void ListenForInput()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            sprinting = !sprinting;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            sprinting = !sprinting;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Disconnect
        }
    }

    #region Local Functions
    void Movement()
    {
        float posX = gameObject.transform.position.x;
        float posY = gameObject.transform.position.y;

        if (!sprinting)
            transform.position = new Vector2(posX + Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, posY + Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);
        if (sprinting)
            transform.position = new Vector2(posX + Input.GetAxis("Horizontal") * sprintSpeed * Time.deltaTime, posY + Input.GetAxis("Vertical") * sprintSpeed * Time.deltaTime);
    }

    void InitiateCharacter()
    {
        int random = Random.Range(0, charPortraitArray.Length);
        charPortraitNumber = random;
        charPortrait.GetComponent<SpriteRenderer>().sprite = charPortraitArray[charPortraitNumber];

        if (charPortraitNumber <= (charPortraitArray.Length / 2))
        {
            random = Random.Range(0, charPortraitArray.Length / 2);
            charNameNumber = random;
        }
        if (charPortraitNumber >= (charPortraitArray.Length / 2))
        {
            random = Random.Range(charPortraitArray.Length / 2, charPortraitArray.Length);
            charNameNumber = random;
        }

        charName.text = charNameArray[charNameNumber];

        Color customColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        this.gameObject.GetComponent<SpriteRenderer>().color = customColor;

        string alias = PhotonNetwork.LocalPlayer.NickName;
        Alias.text = alias;

        PV.RPC("RPC_PushCharacterInitiate", RpcTarget.AllBuffered, charPortraitNumber, charNameNumber, alias, customColor.r, customColor.g, customColor.b);
    }

    public void InfectPlayer()
    {
        Debug.Log("Player infected");
        PV.RPC("RPC_InfectPlayerSync", RpcTarget.AllBuffered, Infection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Infected")
        {
            if(PV.IsMine)
            {
                InfectPlayer();
            }
        }
    }
    #endregion


    #region Network Functions
    [PunRPC]
    void RPC_PushCharacterInitiate(int portNum, int nameNum, string alias, float red, float green, float blue)
    {
        charPortrait.GetComponent<SpriteRenderer>().sprite = charPortraitArray[portNum];
        charName.text = charNameArray[nameNum];
        Color custCol = new Color(red, green, blue);
        this.gameObject.GetComponent<SpriteRenderer>().color = custCol;
        Alias.text = alias;
    }
    [PunRPC]
    void RPC_InfectPlayerSync(bool infection)
    {
        Debug.Log("Pushing infection state update to network");
        Infection = infection;
        gameObject.tag = "Infected";
        charPortrait.GetComponent<SpriteRenderer>().sprite = InfectedFace;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
    }
    #endregion
}
