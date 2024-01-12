using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;
using Slider = UnityEngine.UI.Slider;

public class PlayerController : MonoBehaviour
{
    // Déclaration des constantes
    private static readonly Vector3 FlipRotation = new Vector3(0, 180, 0);
    private static readonly Vector3 CameraPosition = new Vector3(15, 3, 0);
    private static readonly Vector3 InverseCameraPosition = new Vector3(-15, 3, 0);
    [SerializeField] Slider EnergySlider;
    public float energyPoints;
    public bool canAttack = true;
    float delayCounter = 0f;
    private Transform m_GroundCheck;
    private float groundDistance = 0.4f;
    private Transform m_staffTransform;
    private Transform m_SpawnPoint;
    private GameObject m_Hair;
    [SerializeField] private Material m_PBR_Default;
    [SerializeField] private Material m_BlueMat;
    
    // Déclaration des variables
    bool _Grounded { get; set; }
    bool _Flipped { get; set; }
    Animator _Anim { get; set; }
    Rigidbody _Rb { get; set; }
    Camera _MainCamera { get; set; }
    WeaponHandler _WeaponHandler { get; set;}

    bool isBlocking = false;
    float blockDuration = 0f;
    TextMeshProUGUI remainingHP;
    MainWeapon weapon;
    
    //Spells
    private bool isPoweredUp;
    private AudioSource spellAura;
    
    //Sounds
    private SoundManager m_SoundManager;
    private bool SprintingSoundIsPlaying;
    private bool WalkingSoundIsPlaying;

    [SerializeField] private GameObject sword; 
    [SerializeField] private GameObject axe; 
    [SerializeField] private GameObject lance; 
    [SerializeField] private GameObject mace; 
    [SerializeField] private GameObject staff; 

    // Valeurs exposées
    [SerializeField]
    float MoveSpeed = 5.0f;

    [SerializeField]
    float SprintSpeedBoost = 1.5f;

    [SerializeField]
    float JumpForce = 10f;

    [SerializeField]
    float SprintDrain;

    [SerializeField]
    float AttackDrain;

    [SerializeField]
    float SpellDrain;

    [SerializeField]
    float JumpDrain;

    [SerializeField]
    float energyDelay = 1000f;

    [SerializeField]
    float energyRegenRate = 0.1f;

    [SerializeField]
    float maxEnergy = 100f;

    [SerializeField]
    float perfectBlockWindow = 60f;

    [SerializeField]
    GameObject blockVFX;

    [SerializeField]
    LayerMask WhatIsGround;
    
    [SerializeField]
    public GameObject spellObject;

    public ParticleSystem dustParticle;

    [System.NonSerialized]
    public float damageBuff = 0f;

    [System.NonSerialized]
    public float defenseBuff = 0f;

    private GameManager m_GameManager;
    [SerializeField] private GameObject VictoryScreen;

    [System.NonSerialized]
    public bool isPlayerInputAllowed = true;

    // Awake se produit avant le Start. Il peut être bien de régler les références dans cette section.
    void Awake()
    {
        _Anim = GetComponent<Animator>();
        _Rb = GetComponent<Rigidbody>();
        _WeaponHandler = GetComponent<WeaponHandler>();
        _MainCamera = Camera.main;
    }

    // Utile pour régler des valeurs aux objets
    void Start()
    {
        m_GroundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        _Flipped = false;
        GameObject hpTextObject = GameObject.Find("remainingHP");
        remainingHP = hpTextObject == null ? null : hpTextObject.GetComponent<TextMeshProUGUI>();
        m_SpawnPoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
        m_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        m_Hair = GameObject.Find("Hair01");
        
        //Spells
        isPoweredUp = false;
        spellAura = GameObject.Find("SpellAura").GetComponent<AudioSource>();

        energyPoints = maxEnergy;
        EnergySlider.maxValue = maxEnergy;
    }

    // Vérifie les entrées de commandes du joueur
    void Update()
    {
        if (!m_GameManager.destroyingDoor && isPlayerInputAllowed)
        {
            //On vérifie si le player est grounded
            _Grounded = Physics.CheckSphere(m_GroundCheck.transform.position, groundDistance, WhatIsGround);

            m_staffTransform = _WeaponHandler.StaffTransform;

            // Délai avant de regénérer l'énergie du joueur.
            delayCounter++;
            if (delayCounter >= energyDelay)
            {
                energyPoints += energyRegenRate;

                if(energyPoints >= maxEnergy) {
                    delayCounter = 0f;
                }
            }

            // update the energy slider
            if (EnergySlider != null)
            {
                EnergySlider.value = energyPoints;
            }

            if (_WeaponHandler != null)
            {
                weapon = _WeaponHandler.getCurrentWeapon();
                //weapon.isAttacking = false;
            }

            var horizontal = Input.GetAxis("Horizontal") * MoveSpeed;
            if (Input.GetKey(KeyCode.LeftShift) && _Grounded && energyPoints > 0.0) horizontal = horizontal * SprintSpeedBoost;
            var control = Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl);
            bool rightClick = Input.GetMouseButton(1) && _WeaponHandler.isStaffInArsenal();
            var leftClick = Input.GetMouseButtonDown(0);

            isBlocking = control;

            manageBlockDuration();

            _Anim.SetBool("isMoving", Input.GetAxisRaw("Horizontal") != 0 && !isBlocking);
            _Anim.SetBool("isBlocking", control);

            if (rightClick)
            {
                _WeaponHandler.changeToStaff();
            }
            else if (leftClick && _WeaponHandler.isStaffEquipped)
            {
                _WeaponHandler.changeToEquippedWpn();
            }

            _Anim.SetBool("isUsingSpell", rightClick);

            if (leftClick && !isBlocking && canAttack)
            {
                if (energyPoints > 0)
                {
                    _Anim.ResetTrigger(weapon.triggerName);
                    _Anim.SetTrigger(weapon.triggerName);
                }
            }

            if (!isBlocking)
            {
                HorizontalMove(horizontal);

                if (Input.GetKey(KeyCode.LeftShift) && _Grounded)
                {
                    if (energyPoints > 0 && horizontal != 0)
                    {
                        energyPoints = energyPoints - SprintDrain;
                        delayCounter = 0f;
                        if (Math.Abs(horizontal) > 0.5) MakeDust();
                        if (!SprintingSoundIsPlaying)
                        {
                            StartCoroutine(PlaySprintingSound());
                        }

                        _Anim.SetBool("isSprinting", true);
                        _Rb.velocity = new Vector3(_Rb.velocity.x * 2, _Rb.velocity.y, _Rb.velocity.z);
                    }
                    else
                    {
                        _Anim.SetBool("isSprinting", false);
                        StopDust();
                    }
                }
                else
                {
                    if (_Anim.GetBool("isSprinting"))
                    {
                        _Rb.velocity = new Vector3(_Rb.velocity.x / 2, _Rb.velocity.y, _Rb.velocity.z);
                    }

                    if (!WalkingSoundIsPlaying && _Grounded)
                    {
                        StartCoroutine(PlayWalkingSound());
                    }

                    _Anim.SetBool("isSprinting", false);
                    StopDust();
                }

                if (rightClick)
                {
                    if (energyPoints > 0)
                    {
                        energyPoints = energyPoints - SpellDrain;
                        delayCounter = 0f;
                        StartCoroutine(LaunchSpell());
                    }
                }
            }

            if (_Anim.GetBool("isMoving") == false)
            {
                m_SoundManager.PauseSprintSound();
            }

            //Spells
            if (Input.GetMouseButtonDown(2) && _WeaponHandler.isStaffEquipped)
            {
                if (!isPoweredUp)
                {
                    spellAura.Play();
                    StartCoroutine(PowerUpSpells());
                }
            }

            FlipCharacter(horizontal);
            CheckJump();
        }
    }

    // Gère le mouvement horizontal
    void HorizontalMove(float horizontal)
    {
        _Rb.velocity = new Vector3(_Rb.velocity.x, _Rb.velocity.y, horizontal);
        _Anim.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
    }

    // Gère le saut du personnage, ainsi que son animation de saut
    void CheckJump()
    {
        if (_Grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if(energyPoints > 0)
                {
                    m_SoundManager.PauseSprintSound();
                    energyPoints = energyPoints - JumpDrain;
                    delayCounter = 0f;
                    _Rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
                }
            }
        }
    }

    // Gère l'orientation du joueur et les ajustements de la camera
    void FlipCharacter(float horizontal)
    {
        if (horizontal < 0 && !_Flipped)
        {
            _Flipped = true;
            transform.Rotate(FlipRotation);
            _MainCamera.transform.Rotate(-FlipRotation);
            _MainCamera.transform.localPosition = InverseCameraPosition;
        }
        else if (horizontal > 0 && _Flipped)
        {
            _Flipped = false;
            transform.Rotate(-FlipRotation);
            _MainCamera.transform.Rotate(FlipRotation);
            _MainCamera.transform.localPosition = CameraPosition;
        }
    }

    // Collision avec le sol
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "NewWeapon") {
            switch (coll.gameObject.name)
            {
                case "MaceCollectible":
                    _WeaponHandler.addNewItemToArsenal(mace);
                    break;
                case "AxeCollectible":
                    _WeaponHandler.addNewItemToArsenal(axe);
                    break;
                case "StaffCollectible":
                    _WeaponHandler.addNewItemToArsenal(staff);
                    break;
                case "SpearCollectible":
                    _WeaponHandler.addNewItemToArsenal(lance);
                    break;
                default:
                    break;
            }
            Destroy(coll.gameObject.transform.GetChild(0).gameObject);
            Destroy(coll.gameObject);
        }
        
        // On s'assure de bien être en contact avec le sol
        if ((WhatIsGround & (1 << coll.gameObject.layer)) == 0)
            return;

        // Évite une collision avec le plafond
        if (coll.relativeVelocity.y > 0)
        {
            _Grounded = true;
        }
    }

    public float handleProjectileCollision(Collider coll, float damageValue) {
        float damage = 0f;
        // Colision avec un projectile ennemi
        if (isBlockingCorrectSide(coll.GetComponent<Rigidbody>())) {
            // Si le block est effectué assez rapidement, le block est parfait.
            if(blockDuration < perfectBlockWindow) {
                // Ajout d'un effet visuel pour indiquer que le block est parfait.
                Vector3 vfxPosition = GameObject.Find("Shield08").transform.position;
                GameObject blockEffect = Instantiate(blockVFX, vfxPosition, new Quaternion(0, 0, 0, 1));
                blockEffect.GetComponent<ParticleSystem>().Play();
            } else {
                damage = damageValue / 2;
            }
        } else {
            damage = damageValue;
        }
    
        return damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ocean"))
        {
            StartDeathAnimation();
        }

        if (other.CompareTag("VictoryTrigger"))
        {
            StartVictoryScreen();
        }
    }

    bool isBlockingCorrectSide(Rigidbody collidedRb) {
        return collidedRb.position.z > _Rb.position.z && !_Flipped && isBlocking;
    }

    void manageBlockDuration() {
        if(isBlocking) {
            blockDuration++;
        } else {
            blockDuration = 0f;
        }
    }

    public void drainAttackEnergy() {
        energyPoints = energyPoints - AttackDrain;
        energyPoints = energyPoints < 0 ? 0 : energyPoints;
        delayCounter = 0f;
    }

    public void setWeaponAttackState(bool newState) {
        weapon.isAttacking = newState;
    }

    IEnumerator LaunchSpell()
    {
        yield return new WaitForSeconds(0.25f);
        manageSpellAttack();
    }
    
    bool spellCasted()
    {
        if (GameObject.Find("Spell(Clone)") != null)
        {
            return true;
        }

        return false;
    }

    void manageSpellAttack()
    {
        if (!spellCasted())
        {
            GameObject spell = Instantiate(spellObject, m_staffTransform.position, Quaternion.identity);
            m_SoundManager.PlaySpellLaunch();
            Rigidbody projectileBody = spell.GetComponent<Rigidbody>();
            float SpellDirection = 0;
            if (this.transform.rotation.y > 0)
            {
                SpellDirection = -1;
            }
            else
            {
                SpellDirection = 1;
            }
            projectileBody.velocity = new Vector3(0f, 0f, 5f * SpellDirection);
            Destroy(spell, 3f);
        }
    }

    void MakeDust()
    {
        if(!dustParticle.isPlaying)
            dustParticle.Play();
    }

    void StopDust()
    {
        if(dustParticle.isPlaying)
            dustParticle.Stop();
    }
    
    IEnumerator PlaySprintingSound()
    {
        SprintingSoundIsPlaying = true;
        m_SoundManager.PlaySprintSound();
        yield return new WaitForSeconds(0.180f);
        SprintingSoundIsPlaying = false;
    }
    
    IEnumerator PlayWalkingSound()
    {
        WalkingSoundIsPlaying = true;
        m_SoundManager.PlaySprintSound();
        yield return new WaitForSeconds(0.260f);
        WalkingSoundIsPlaying = false;
    }
    
    IEnumerator PowerUpSpells()
    {
        m_Hair.GetComponent<MeshRenderer>().material = m_BlueMat;
        _Anim.SetBool("IsPoweringUp", true);
        yield return new WaitForSeconds(0.2f);
        _Anim.SetBool("IsPoweringUp", false);
        isPoweredUp = true;
        GameManager.SetSpellDamage(10f);
        yield return new WaitForSeconds(5.00f);
        GameManager.SetSpellDamage(5f);
        isPoweredUp = false;
        m_Hair.GetComponent<MeshRenderer>().material = m_PBR_Default;
    }

    public void StartDeathAnimation() {
        isPlayerInputAllowed = false;
        _Rb.velocity = new Vector3(0f, 0f, 0f);
        _Anim.SetBool("isBlocking", false);
        _Anim.SetBool("isSprinting", false);
        _Anim.SetBool("isMoving", false);

        m_SoundManager.StopMusic();
        _Anim.ResetTrigger("defeat");
        _Anim.SetTrigger("defeat");
    }
    
    public void StartVictoryScreen() {
        Time.timeScale = 0f;
        m_SoundManager.StopMusic();
        VictoryScreen.SetActive(true);
    }
}
