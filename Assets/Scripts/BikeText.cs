//***********************************************************************
// Copyright 2014 VirZOOM
//***********************************************************************

using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

public class BikeText : MonoBehaviour
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
