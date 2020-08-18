using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryView : MonoBehaviour
{
    public GameObject Skin1;
    public GameObject Skin2;
    public GameObject Skin3;

    private float _animDuration = .5f;
    private Animator _anim;
    private int _currentSkinLevel = 1;
    
    void Start()
    {
        _anim = GetComponent<Animator>();
        UpdateSkin(_currentSkinLevel);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var skin = _currentSkinLevel + 1;
            SetSkinLevel(skin);
        }
    }

    public void SetSkinLevel(int skinLevel)
    {
        _anim.SetBool("isUpgrading", true);

        _currentSkinLevel = skinLevel;

        StartCoroutine(WaitForSkinUpdate());
    }

    private IEnumerator WaitForSkinUpdate()
    {
        yield return new WaitForSeconds(_animDuration / 2);
        _anim.SetBool("isUpgrading", false);
        UpdateSkin(_currentSkinLevel);
    }
    
    private void UpdateSkin(int skinLevel)
    {
        Skin1.SetActive(skinLevel == 1);
        Skin2.SetActive(skinLevel == 2);
        Skin3.SetActive(skinLevel == 3);
    }
}
