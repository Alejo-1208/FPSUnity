using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroy());
    }

    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(6f);
        Destroy(this.gameObject);
    }
}
