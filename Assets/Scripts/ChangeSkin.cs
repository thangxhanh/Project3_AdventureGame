using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController[] anims;
    // Start is called before the first frame update
    void Start()
    {
        foreach (AnimatorOverrideController anim in anims)
        {
            if (anim.name == PlayerPrefs.GetString("SkinName"))
            {
                GetComponent<Animator>().runtimeAnimatorController = anim as RuntimeAnimatorController;
                break;
            }
        }
    }
}