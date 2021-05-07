using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashTextBehavior : MonoBehaviour
{
    private Animator anim;
    private TextMeshProUGUI text;
    private bool justRan;
    private bool Running;
    public TMP_ColorGradient RedGrad;
    public TMP_ColorGradient GreenGrad;


    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        text = gameObject.GetComponent<TextMeshProUGUI>();



    }

    private void Update()
    {
        if (justRan == true) anim.SetBool("TextUp", false);
        else if (Running == true) justRan = true;
    }
    // Update is called once per frame
    public void Activate(bool correctness)
    {
        if (correctness == true)
        {
            anim.SetBool("IsCorrect", true);
            anim.SetBool("TextUp", true);
            text.text = "Correct!";
            text.colorGradientPreset = GreenGrad;

        }
        else
        {
            anim.SetBool("IsCorrect", false);
            anim.SetBool("TextUp", true);
            text.text = "Wrong...";
            text.colorGradientPreset = RedGrad;
        }
        Running = true;
        justRan = false;
    }
}
