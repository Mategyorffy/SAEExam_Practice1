using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Items : MonoBehaviour
{
    [SerializeField] private TMP_Text infoText;

    private void Start()
    {
        infoText.text = $"{gameObject.name}" + "\n (pick up with e)";


        infoText.gameObject.SetActive(false);
    }

    public IEnumerator DisplayInfo()
    {
        infoText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        infoText.gameObject.SetActive(false);
    }
  
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
