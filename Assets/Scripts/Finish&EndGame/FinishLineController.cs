using UnityEngine;

public class FinishLineController : MonoBehaviour
{
    [SerializeField] PaintableObject paintableObject;
    [SerializeField] Transform targetPosition; // Karakterin gideceği hedef pozisyon
    [SerializeField] private Transform cameraTargetPosition;
    [SerializeField] Animator cameraAnimator; // Kameranın animasyonu için referans
    [SerializeField] RuntimeAnimatorController breakdanceSequence;
    private Camera mainCamera;
    private CameraFollow cameraFollow; // Kamera takip script'i
    private PlayerMovement playerMovement; // Player hareketini kontrol eden script
    private bool isPlayerFinished; // Oyuncunun finish'e ulaşıp ulaşmadığını kontrol eder
    private bool isMovingToTarget;
    private bool cameraFollowingPlayer;
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        mainCamera = Camera.main;
        isPlayerFinished = false;
        isMovingToTarget = false;
        cameraFollowingPlayer = true;
        cameraFollow = mainCamera.GetComponent<CameraFollow>(); // CameraFollow script'ine referans
    }
    private void Update()
    {
        // Eğer karakter hedef pozisyona doğru hareket ediyorsa
        if (isMovingToTarget)
        {
            MovePlayerToTarget();
        }

        // Kamerayı Player'dan ayırıp hedef konuma hareket ettirme
        if (!cameraFollowingPlayer)
        {
            MoveCameraToTarget();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterMovement character = other.GetComponent<CharacterMovement>();

        if (character != null && character.GetCharacterType() == CharacterType.AI && !character.GetHasFinished())
        {
            character.SetHasFinished(true); // AI karakter finish line'a ulaştı
        }
        // Player finish çizgisine temas ettiğinde
        if (other.CompareTag("Player") && !isPlayerFinished)
        {
            SoundManager.instance.PlayFinishLineSound();
            isPlayerFinished = true; // Tekrar tetiklenmesini engellemek için

            // Oyuncu kontrolünü devre dışı bırak
            playerMovement.DisableMovement();

             // Player'ın sıralamasını sabitle
            UICharacterRankManager.instance.SetPlayerHasFinished();

            // Karakteri hedef pozisyona doğru hareket ettir
            isMovingToTarget = true;

            // Kamerayı takip modundan çıkar
            cameraFollowingPlayer = false;
            cameraFollow.enabled = false;            
        }
    }
    private void MovePlayerToTarget()
    {
        // Karakteri hedef pozisyona doğru hareket ettir
        float step = 6.3f * Time.deltaTime; // Hareket hızı
        playerMovement.transform.position = Vector3.MoveTowards(playerMovement.transform.position, targetPosition.position, step);
        
        // Karakterin yönünü (-x, +z) eksenine doğru döndür
        playerMovement.transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 1));

        // Eğer karakter hedef pozisyona ulaştıysa hareketi durdur
        if (Vector3.Distance(playerMovement.transform.position, targetPosition.position) < 0.1f)
        {
            isMovingToTarget = false;
            playerMovement.transform.position = targetPosition.position;
            playerMovement.SetRunningAnimation(false);
            playerMovement.DisableMovement();
            playerMovement.transform.rotation = Quaternion.LookRotation(Vector3.forward);
            
            // Duvar boyama UI'sini aç
            UIManager.Instance.ShowPaintingUI();

            paintableObject.IsPaintable(true);

            ClearScreen();

            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponentInChildren<Animator>().runtimeAnimatorController = breakdanceSequence;
            Transform marker = player.transform.Find("PlayerMarker");
            if (marker != null)
            {
                Destroy(marker.gameObject);
            }
        }
    }
    //Oyuncu arka tarafı görmediği için objeleri silip performansı arttırıyoruz
    private void ClearScreen()
    {

        Destroy(GameObject.FindWithTag("Platform"));
        Destroy(GameObject.FindWithTag("Coin"));            
        Destroy(GameObject.FindWithTag("Obstackle"));
        CharacterPool.instance.GameOver();
    }

    private void MoveCameraToTarget()
    {
        // Kamerayı hedef pozisyona doğru yumuşak bir şekilde hareket ettirir
        float step = 10f * Time.deltaTime; // Kamera hareket hızı
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, cameraTargetPosition.position, step);

        // Kameranın rotasyonunu hedef X açısına göre ayarla (X ekseni 8 derece, diğer açılar hedefin mevcut açısıyla eşleşir)
        Quaternion targetRotation = Quaternion.Euler(8f, cameraTargetPosition.eulerAngles.y, cameraTargetPosition.eulerAngles.z);
        
        // Rotasyon hızını artırarak daha hızlı dönüş sağla
        float fastRotationSpeed = 5f * Time.deltaTime; // Rotasyon hızını artırır
        mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, targetRotation, fastRotationSpeed);

        // Kamera hedef pozisyona ve rotasyona ulaştığında
        if (Vector3.Distance(mainCamera.transform.position, cameraTargetPosition.position) < 0.1f &&
            Quaternion.Angle(mainCamera.transform.rotation, targetRotation) < 0.1f)
        {
            mainCamera.transform.position = cameraTargetPosition.position;
            mainCamera.transform.rotation = targetRotation;
            cameraFollowingPlayer = true; // Kamera hareketini durdurur
        }
    }
}


