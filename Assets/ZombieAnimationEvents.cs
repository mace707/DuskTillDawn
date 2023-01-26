using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationEvents : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    private Animator ZombieAnimator;

    private Transform PlayerTransform;

    [SerializeField] private GameObject ZombieMarkerPrefab;

    private GameObject ZombieMarkerInstance;

    public enum STATE
    {
        STATE_IDLE,
        STATE_LOOK_AROUND,
        STATE_WALK,
        STATE_SCREAM,
        STATE_RUN,
        STATE_ATTACK_1,
        STATE_ATTACK_2,
        STATE_DODGE,
        STATE_DAMAGE,
        STATE_DEATH,
    };

    public STATE CurrentState = STATE.STATE_IDLE;
    public Quaternion TargetRotation;
    [SerializeField] private AudioClip ZombieScream;
    [SerializeField] private WeaponScript Weapon; 

    public void SetMovementSpeed(float speed = 0)
    {
        Speed = speed;
    }

    public void PlayScreamSound()
    {
        AudioSource.PlayClipAtPoint(ZombieScream, transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        ZombieAnimator = GetComponent<Animator>();
        StartCoroutine(ChangeState());
        TargetRotation = transform.rotation;
        PlayerTransform = Statics.GetPlayerTransform();
    }

    IEnumerator ChangeState()
    {        
        CurrentState = STATE.STATE_IDLE;
        yield return new WaitForSeconds(5.0f);

        CurrentState = STATE.STATE_LOOK_AROUND;
        yield return new WaitForSeconds(5.33f);

        CurrentState = STATE.STATE_WALK;
        TargetRotation *= Quaternion.AngleAxis(Random.Range(-180, 180), transform.up);
        var waitTime = Random.Range(10, 30);
        yield return new WaitForSeconds(waitTime);

        yield return ChangeState();
    }

    public void SetState(int state)
    {
        CurrentState = (STATE)state;
    }

    void DestroyMarker()
    {
        Destroy(ZombieMarkerInstance);
    }

    // Update is called once per frame
    void Update()
    {        
        if (Vector3.Distance(transform.position, PlayerTransform.position) >= 1.5)
        {
            if (CurrentState == STATE.STATE_ATTACK_1)
                CurrentState = STATE.STATE_RUN;

            transform.position += transform.forward * Speed * Time.deltaTime;
            if (ZombieMarkerInstance)
            {
                Invoke("DestroyMarker", 2.5f);
            }
        }
        else
        {
            CurrentState = STATE.STATE_ATTACK_1;

            if (!ZombieMarkerInstance)
            {
                ZombieMarkerInstance = Instantiate(ZombieMarkerPrefab, Statics.GetRotionalMarkerHolder());
                var ZombieMarker = ZombieMarkerInstance.GetComponent<Marker>();
                ZombieMarker.Construct(PlayerTransform, transform);
            }
            else
            {
                CancelInvoke("DestroyMarker");
            }

        }

        ZombieAnimator.SetInteger("State", (int)CurrentState);

        if (CurrentState == STATE.STATE_RUN)
        {
            Vector3 enemyToPlayer = PlayerTransform.position - transform.position;
            enemyToPlayer.y = 0; // keep the direction strictly horizontal
            TargetRotation = Quaternion.LookRotation(enemyToPlayer, Vector3.up);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, 2f * Time.deltaTime);
    }

    public void EnableWeapon()
    {
        Weapon.Enable();
    }

    public void DisableWeapon()
    {
        Weapon.Disable();
    }

    public void Kill()
    {
        Destroy(gameObject);
        Destroy(ZombieMarkerInstance);
    }
}
