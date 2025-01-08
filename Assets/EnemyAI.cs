using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Settings")]
    public Transform player; // Oyuncunun Transform'u
    public float detectionRange = 15f; // Alg�lama mesafesi
    public float attackRange = 2f; // Sald�r� mesafesi
    public float moveSpeed = 3f; // Hareket h�z�
    public float attackCooldown = 2f; // Sald�r� bekleme s�resi
    public int attackDamage = 10; // Hasar miktar�
    public float jumpAttackChance = 0.3f; // S��rayarak sald�rma olas�l��� (0.3 = %30)

    private Animator animator;
    private float lastAttackTime = 0f; // Son sald�r� zaman�
    private bool isDead = false; // D��man �l� m�?
    private bool isPlayerInRange = false; // Oyuncu alg�land� m�?
    private bool isPlayerInAttackRange = false; // Oyuncu sald�r� mesafesinde mi?

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

        // Oyuncuya d�n
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void AttackPlayer()
    {
        if (isDead || Time.time < lastAttackTime + attackCooldown) return;

        lastAttackTime = Time.time;

        // Rastgele sald�r� tipi se�imi
        int attackType = Random.Range(0, 2); // 0: Normal sald�r�, 1: S��rayarak sald�r�

        if (attackType == 0)
        {
            // Normal sald�r�
            animator.SetBool("Attack", true);
            animator.SetBool("AttackJump", false);
            Debug.Log("Normal sald�r� yap�l�yor.");
        }
        else
        {
            // S��rayarak sald�r�
            animator.SetBool("AttackJump", true);
            animator.SetBool("Attack", false);
            Debug.Log("S��rayarak sald�r� yap�l�yor.");
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

        // �l�m sonras� hareketi engellemek i�in t�m animasyonlar� kapat
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

        // �l�m sonras� ek i�lemler (�r. yok etmek)
        Destroy(gameObject, 5f); // 5 saniye sonra d��man� yok et
    }
}
