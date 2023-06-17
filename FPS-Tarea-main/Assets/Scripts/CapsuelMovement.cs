using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuelMovement : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento
    public float sensibilidadRaton = 3f; // Sensibilidad del ratón
    public float fuerzaSalto = 5f; // Fuerza de salto
    public float distanciaSuelo = 0.1f; // Distancia para el Raycast del suelo
    public LayerMask sueloLayer; // Layer del suelo

    private Rigidbody rb;
    private float rotacionX = 0f;
    private bool puedeSaltar = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el componente Rigidbody
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor en el centro de la pantalla
    }

    void Update()
    {
        // Movimiento en los ejes X y Z
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Obtener la dirección hacia adelante y hacia los lados del objeto
        Vector3 direccionMovimiento = transform.forward * movimientoVertical;
        direccionMovimiento += transform.right * movimientoHorizontal;

        // Calcular el movimiento
        Vector3 movimiento = direccionMovimiento * velocidad;
        rb.velocity = new Vector3(movimiento.x, rb.velocity.y, movimiento.z);

        // Salto
        if (Input.GetButtonDown("Jump") && puedeSaltar)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            puedeSaltar = false;
        }

        // Rotación en el eje Y
        float rotacionY = Input.GetAxis("Mouse X") * sensibilidadRaton;
        transform.Rotate(0f, rotacionY, 0f);

        // Rotación en el eje X
        rotacionX -= Input.GetAxis("Mouse Y") * sensibilidadRaton;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        // Desbloquear el cursor si se presiona la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void FixedUpdate()
    {
        // Verificar si hay suelo debajo del objeto utilizando Raycast y comparando el layer
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, distanciaSuelo, sueloLayer))
        {
            puedeSaltar = true;
        }
        else
        {
            puedeSaltar = false;
        }
        // Visualizar el rayo de detección de suelo

        Debug.DrawRay(transform.position, Vector3.down * distanciaSuelo, Color.red);
    }
}