using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

public class text : MonoBehaviour
{
    TextMesh mText;

   void Start()
   {
      // Lookup text objects
      mText = GetComponent<TextMesh>();
   }

   void Update()
   {
      var controller = VZPlayer.Controller;

      mText.text = 
         "Connected: " + controller.IsBikeConnected();
            
   }
}
