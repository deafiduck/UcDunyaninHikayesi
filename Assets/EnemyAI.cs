using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Settings")]
    public Transform player; // Oyuncunun Transform'u
    public float detectionRange = 15f; // Algýlama mesafesi
    public float attackRange = 2f; // Saldýrý mesafesi
    public float moveSpeed = 3f; // Hareket hýzý
    public float attackCooldown = 2f; // Saldýrý bekleme süresi
    public int attackDamage = 10; // Hasar miktarý
    public float jumpAttackChance = 0.3f; // Sýçrayarak saldýrma olasýlýðý (0.3 = %30)

    private Animator animator;
    private float lastAttackTime = 0f; // Son saldýrý zamaný
    private bool isDead = false; // Düþman ölü mü?
    private bool isPlayerInRange = false; // Oyuncu algýlandý mý?
    private bool isPlayerInAttackRange = false; // Oyuncu saldýrý mesafesinde mi?

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isPlayerInRange = distanceToPlayer <= detectionRange;
        isPlayerInAttackRange = distanceToPlayer <= attackRange;

        if (isPlayerInRange && !isPlayerInAttackRange)
        {
            ChasePlayer();
        }
        else if (isPlayerInAttackRange)
        {
            AttackPlayer();
        }
        else
        {
            Idle();
        }
    }

    private void ChasePlayer()
    {
        if (isDead) return;

        animator.SetBool("Walking", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);
        animator.SetBool("AttackJump", false);

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Oyuncuya dön
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void AttackPlayer()
    {
        if (isDead || Time.time < lastAttackTime + attackCooldown) return;

        lastAttackTime = Time.time;

        // Rastgele saldýrý tipi seçimi
        int attackType = Random.Range(0, 2); // 0: Normal saldýrý, 1: Sýçrayarak saldýrý

        if (attackType == 0)
        {
            // Normal saldýrý
            animator.SetBool("Attack", true);
            animator.SetBool("AttackJump", false);
            Debug.Log("Normal saldýrý yapýlýyor.");
        }
        else
        {
            // Sýçrayarak saldýrý
            animator.SetBool("AttackJump", true);
            animator.SetBool("Attack", false);
            Debug.Log("Sýçrayarak saldýrý yapýlýyor.");
        }

        animator.SetBool("Walking", false);
        animator.SetBool("Idle", false);

        // Oyuncuya hasar ver
        /*if (player.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Player'a hasar verildi!");
        }*/
    }


    private void Idle()
    {
        if (isDead) return;

        animator.SetBool("Idle", true);
        animator.SetBool("Walking", false);
        animator.SetBool("Attack", false);
        animator.SetBool("AttackJump", false);
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("Die");

        // Ölüm sonrasý hareketi engellemek için tüm animasyonlarý kapat
        animator.SetBool("Idle", false);
        animator.SetBool("Walking", false);
        animator.SetBool("Attack", false);
        animator.SetBool("AttackJump", false);

        // Rigidbody varsa hareketi durdur
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Ölüm sonrasý ek iþlemler (ör. yok etmek)
        Destroy(gameObject, 5f); // 5 saniye sonra düþmaný yok et
    }
}
