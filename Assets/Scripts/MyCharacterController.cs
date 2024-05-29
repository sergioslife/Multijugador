using System.Collections; // Importar el espacio de nombres System.Collections para usar colecciones básicas como Listas y Diccionarios.
using System.Collections.Generic; // Importar el espacio de nombres System.Collections.Generic para usar colecciones genéricas.
using UnityEngine; // Importar el espacio de nombres de Unity para acceder a las clases y funcionalidades de Unity.

public class MyCharacterController : MonoBehaviour
{
    public float walkSpeed = 5f; // Velocidad de caminata del personaje.
    public float runSpeed = 10f; // Velocidad de carrera del personaje.
    public float gravity = -9.81f; // Valor de la gravedad que afecta al personaje.
    public float jumpHeight = 2f; // Altura del salto del personaje.

    CharacterController controller; // Referencia al componente CharacterController del personaje.
    Vector3 velocity; // Vector que almacena la velocidad vertical del personaje.

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Obtener el componente CharacterController adjunto al GameObject.
    }

    void Update()
    {
        // Verificar si el jugador está en el suelo.
        bool isGrounded = controller.isGrounded;

        // Definir la velocidad de movimiento basada en si se está corriendo o caminando.
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Calculando movimiento horizontal y vertical del personaje.
        float horizontalInput = Input.GetAxis("Horizontal"); // Entrada horizontal del teclado.
        float verticalInput = Input.GetAxis("Vertical"); // Entrada vertical del teclado.
        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput; // Dirección de movimiento del personaje.

        // Aplicar movimiento al personaje.
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

        // Saltar si el jugador está en el suelo y presiona la tecla de salto.
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calcular la velocidad vertical para el salto.
        }

        // Aplicar gravedad al personaje.
        velocity.y += gravity * Time.deltaTime; // Ajustar la velocidad vertical con la gravedad.
        controller.Move(velocity * Time.deltaTime); // Aplicar movimiento vertical al personaje.
    }
}
