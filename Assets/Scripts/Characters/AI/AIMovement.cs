using UnityEngine;
using System.Collections;

public class AIMovement : CharacterMovement
{
    public Transform[] waypoints;   // Waypoint noktaları dizisi
    private int currentWaypointIndex;
    private bool isWaiting = false; // AI'nın durakladığını belirten durum

    public override void Start()
    {
        base.Start();
        characterType = CharacterType.AI;
        currentWaypointIndex = 0;
    }

    void FixedUpdate()
    {
        if (!canMove) return; // Eğer hareket devre dışıysa hiçbir işlem yapma
        if (!isFell && !isWaiting) // AI düşmediyse ve duraklamıyorsa
        {
            MoveForward();
        }

        CheckFalling();
    }

    private void MoveForward()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            SetRunningAnimation(false);
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // Yalnızca x ve z ekseninde dönme sağlamak için y eksenini sıfırlıyoruz
        direction.y = 0;

        // AI karakteri hedef waypoint'e doğru yönlendirme
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // AI karakteri ileri doğru hareket ettir
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        SetRunningAnimation(true);

        // Waypoint'e ulaşıldığında bir sonraki waypoint'e geçme toleransını artır
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.5f)
        {
            StartCoroutine(PauseAtWaypoint());
        }
    }

    private IEnumerator PauseAtWaypoint()
    {
        isWaiting = true;
        SetRunningAnimation(false);
        yield return new WaitForSeconds(0.1f); // 0.1 saniye durakla
        currentWaypointIndex++;
        isWaiting = false;
    }

    public override void CheckFalling()
    {
        if (transform.position.y < fallThreshold && !isFell)
        {
            isFell = true;
            SetFallingAnimation(true);
            SetRunningAnimation(false);
            canMove = false; // Düşme anında hareketi devre dışı bırak
        }
    }

    // Düşme animasyonu tamamlandığında çağrılacak metot
    public override void OnStandUpComplete()
    {
        isFell = false;
        SetFallingAnimation(false);
        canMove = true; // Düşme animasyonu tamamlandıktan sonra hareketi geri aç
    }

    public override void ResetPosition()
    {
        int randomXCoordinate = Random.Range(-6, 6);
        transform.position = new Vector3(randomXCoordinate, 1.537001f, 1f);
        rb.velocity = Vector3.zero;
        currentWaypointIndex = 0;
        canMove = true; // Sıfırlama durumunda hareketi tekrar aç
    }
}
