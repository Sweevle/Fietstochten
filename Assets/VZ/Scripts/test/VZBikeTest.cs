//***********************************************************************
// Copyright 2014 VirZOOM
//***********************************************************************

using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

public class VZBikeTest : MonoBehaviour
{
   TextMesh mText;
   GameObject canvas;
   Text speed;
   Text spins;

   void Start()
   {
      // Lookup text objects
      mText = GetComponent<TextMesh>();
      canvas = GameObject.Find("CanvasUI");
      speed = canvas.transform.Find("Speed").GetComponent<Text>();
      spins = canvas.transform.Find("Spins").GetComponent<Text>();
   }

   void Update()
   {
      var controller = VZPlayer.Controller;

      mText.text = 
         "Headrot: " + controller.HeadRot + "\n" +
         "HeadLean: " + controller.HeadLean + "\n" +
         "HeadBend: " + controller.HeadBend + "\n";

      spins.text = controller.Spins + " spins";
      speed.text = controller.InputSpeed + " m/s";
   }




}
