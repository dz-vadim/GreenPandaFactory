using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GreenPandaAssets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class UpgradeUI : MonoBehaviour
{
    public AUpgradable Upgradable;

    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI CurrentLevelText;
    public TextMeshProUGUI NextLevelText;

    private void Awake()
    {
        PriceText.text = Upgradable.GetPrice().ToString();
        CurrentLevelText.text = Upgradable.Level.ToString();
        NextLevelText.text = (Upgradable.Level + 1).ToString();
    }

    private void Update()
    {
        
    }
    
    public void Upgrade()
    {
        var price = Upgradable.GetPrice();
        if (price > TopUI.Instance.Coins || Upgradable.IsMax()) return;
        
        TopUI.Instance.Coins -= price;
        Upgradable.Upgrade();
        PriceText.text = Upgradable.GetPrice().ToString();
        CurrentLevelText.text = Upgradable.Level.ToString();
        NextLevelText.text = (Upgradable.Level + 1).ToString();

        UpdateTrees();
    }
    
    private void UpdateTrees()
    {
        StartCoroutine(AnimateProps());
    }

    private IEnumerator AnimateProps()
    {
        bool isScaleDownFinished = false;
        while (!isScaleDownFinished)
        {
           var trees =  FindObjectsOfType<EnvProp>().ToList();

           for (int i = trees.Count - 1; i >= 0; i--)
           {
               if (!trees[i].name.Contains("Tree") && !trees[i].name.Contains("Rock") && !trees[i].name.Contains("Grass") && !trees[i].name.Contains("Bush") && !trees[i].name.Contains("Branch"))
               {
                   trees.Remove(trees[i]);
               }
           }

           foreach (var item in trees)
           {
               float speed = Random.Range(3f, 8f);
               var scale = item.transform.localScale;
               scale.x -= speed * Time.deltaTime;
               if (scale.x < .3f) scale.x = .3f;
               scale.y -= speed * Time.deltaTime;
               if (scale.y < .3f) scale.y = .3f;
               scale.z -= speed * Time.deltaTime;
               if (scale.z < .3f) scale.z = .3f;

               item.transform.localScale = scale;
               
               Debug.Log($"Name: {item.name}, currentScale {item.transform.localScale}");
           }

           isScaleDownFinished = trees.All(t => t.transform.localScale == new Vector3(.3f, .3f, .3f));
           yield return null;
        }
        
        bool isScaleUpFinished = false;
        while (!isScaleUpFinished)
        {
            var  trees =  FindObjectsOfType<EnvProp>().ToList();
           
            for (int i = trees.Count - 1; i >= 0; i--)
            {
                if (!trees[i].name.Contains("Tree") && !trees[i].name.Contains("Rock") && !trees[i].name.Contains("Grass") && !trees[i].name.Contains("Bush") && !trees[i].name.Contains("Branch"))
                {
                    trees.Remove(trees[i]);
                }
            }

            foreach (var item in trees)
            {
                float speed = Random.Range(3f, 8f);
                var scale = item.transform.localScale;
                scale.x += speed * Time.deltaTime;
                if (scale.x > 1) scale.x = 1;
                scale.y += speed * Time.deltaTime;
                if (scale.y > 1) scale.y = 1;
                scale.z += speed * Time.deltaTime;
                if (scale.z > 1) scale.z = 1;

                item.transform.localScale = scale;
                
                Debug.Log($"Name: {item.name}, currentScale {item.transform.localScale}");
            }

            isScaleUpFinished = trees.All(t => t.transform.localScale == Vector3.one);
            yield return null;
        }
    }
}
