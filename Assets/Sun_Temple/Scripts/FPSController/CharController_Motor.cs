using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class CharController_Motor : MonoBehaviour
    {
        public float speed = 3.0f;
        public float sensitivity = 60.0f;
        CharacterController character;
        public GameObject cam;
        float moveFB, moveLR;
        float rotHorizontal, rotVertical;
        public bool webGLRightClickRotation = true;
        float gravity = -9.8f;
        CharacterControllerWAnimation ChAnimation;  // Animation controller
        Animator anim;

        public float cameraDistance = 2.0f;  // Kamera ile karakter arasındaki mesafe
        public float cameraHeight = 1.0f;    // Kamera yüksekliği
        public float rotationSpeed = 4.0f;   // Kameranın dönüş hızı
        public GameObject characterCamPos;
        public float cameraVerticalAngleLimit = 70f; // Kamera yukarı ve aşağı hareket sınırı
        private float currentCameraVerticalAngle = 0f; // Mevcut dikey açı
        public bool walk, run, idle;
        void Start()
        {
            ChAnimation = GetComponent<CharacterControllerWAnimation>();  // Access the CharacterControllerWAnimation script
            character = GetComponent<CharacterController>();

            webGLRightClickRotation = false;

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                webGLRightClickRotation = true;
                sensitivity = sensitivity * 1.5f;
            }
        }

        void FixedUpdate()
        {
            // Handle movement inputs
            moveFB = Input.GetAxis("Vertical") * speed;
            moveLR = Input.GetAxis("Horizontal") * speed;

            rotHorizontal = Input.GetAxisRaw("Mouse X") * sensitivity;
            rotVertical = Input.GetAxisRaw("Mouse Y") * sensitivity;

            Vector3 movement = new Vector3(moveLR, gravity, moveFB);

            // Apply movement based on rotation
            movement = transform.rotation * movement;
            character.Move(movement * Time.fixedDeltaTime);

            // Handle character animations
            HandleAnimations();

            // Third-person camera follow and rotation
            HandleCamera();
        }

        void HandleAnimations()
        {
            // Walk animation
            if (moveFB != 0 || moveLR != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    ChAnimation.Run();  // Run if shift is held
                    speed = 5;
                    run = true;
                    walk = false;
                    idle = false;
                }
                else
                {
                    ChAnimation.walk();  // Walk otherwise
                    speed = 3;
                    run = false;
                    walk = true;
                    idle = false;
                }
            }
            else
            {
                ChAnimation.Idle();  // Idle when not moving
                run = false;
                walk = false;
                idle = true;
            }

            // Jump animation
            if (Input.GetButtonDown("Jump") && character.isGrounded)
            {
                ChAnimation.Jump();  // Jump animation when space is pressed
            }

            // Attack animation (mouse left click)
            if (Input.GetMouseButtonDown(0))
            {
                ChAnimation.Attack();  // Attack when left mouse button is clicked
            }
        }

        void HandleCamera()
        {
            // Kamera, karakterin arkasına yerleştirilecek
            Vector3 targetPosition = characterCamPos.transform.position - characterCamPos.transform.forward * cameraDistance + Vector3.up * cameraHeight;

            // Kameranın, karakterin etrafında dönebilmesi için, fare hareketini işleme
            if (webGLRightClickRotation)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    CameraRotation(rotHorizontal, rotVertical);
                }
            }
            else
            {
                CameraRotation(rotHorizontal, rotVertical);
            }

            // Kamerayı karaktere doğru yönlendir
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, Time.deltaTime * rotationSpeed);
            cam.transform.LookAt(characterCamPos.transform.position + Vector3.up * 1f);  // Karakterin biraz üst kısmını hedef al
        }

        void CameraRotation(float rotHorizontal, float rotVertical)
        {
            // Karakterin etrafında yatay döndürme (sağ/sol)
            transform.Rotate(0, rotHorizontal * Time.fixedDeltaTime, 0);

            // Kamera için dikey dönüş (yukarı/aşağı)
            currentCameraVerticalAngle -= rotVertical * Time.fixedDeltaTime;  // Yüksekliği azalt (aşağı)
            currentCameraVerticalAngle = Mathf.Clamp(currentCameraVerticalAngle, -cameraVerticalAngleLimit, cameraVerticalAngleLimit);  // Sınırları uygula

            // Kamerayı güncelle (yukarı/aşağı eğilme)
            cam.transform.localRotation = Quaternion.Euler(currentCameraVerticalAngle, transform.eulerAngles.y, 0);
        }
    }

