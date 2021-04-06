﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GunfireRebornMods
{
    public class ModManager : MonoBehaviour
    {
        public ModManager(IntPtr intPtr) : base(intPtr) { }
        public List<ModBase> Mods = new List<ModBase>();
        void OnGUI()
        {
            foreach (var mod in Mods)
                if (mod.Enabled) mod.OnGUI();
            if (Cursor.lockState == CursorLockMode.Locked) return;
            Rect area = new Rect(0, 25, 150, 800);
            //area.yMax = 700;
            GUI.Box(area, "shalzuth's mods");
            GUILayout.BeginArea(area);
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            GUILayout.Space(40);
            foreach (var mod in Mods)
            {
                string Val = mod.SliderVal.ToString();
                var val = GUILayout.Toggle(mod.Enabled, mod.GetType().Name, new GUILayoutOption[0]);
                if (val != mod.Enabled)
                {
                    if (val) mod.OnEnable();
                    else mod.OnDisable();
                    mod.Enabled = val;
                }
                if (mod.Enabled && mod.HasConfig)
                {
                    GUILayout.TextField(Val, 5, new GUILayoutOption[0]);
                    mod.SliderVal = GUILayout.DoHorizontalSlider(mod.SliderVal, mod.SliderMin, mod.SliderMax, new GUIStyle(GUI.skin.horizontalSlider), new GUIStyle(GUI.skin.horizontalSliderThumb), new GUILayoutOption[0]);
                }
                if (mod.Enabled)
                    mod.OnGUI();
            }
            GUILayout.EndVertical();
            //GUILayout.EndArea();
        }
        void Update()
        {
            foreach (var mod in Mods)
                if (mod.Enabled) mod.Update();
        }
        void OnDisable()
        {
            foreach (var mod in Mods)
                if (mod.Enabled) mod.OnDisable();

        }
    }
}