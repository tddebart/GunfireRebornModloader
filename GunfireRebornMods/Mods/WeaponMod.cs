using Il2CppSystem;
using UnityEngine;
namespace GunfireRebornMods
{
    public class WeaponMod : ModBase
    {
        Vector2 ScrollPos = Vector2.zero;
        public override bool HasConfig { get; set; } = true;
        public override float SliderMin { get; set; } = 0;
        public override float SliderVal { get; set; } = 30f;
        public override float SliderMax { get; set; } = 100f;
        float orig = 0;

        public override void OnGUI()
        {
            var area = new Rect(Screen.width - 175, 25, 150, Screen.height - 50);
            GUI.Box(area, "weapon mods");
            GUILayout.BeginArea(area);
            GUILayout.Space(12);
            ScrollPos = GUILayout.BeginScrollView(ScrollPos, new GUILayoutOption[0]);
            var ws = HeroMoveManager.HeroObj?.BulletPreFormCom?.weapondict;
            foreach (var w in ws)
            {
                var v = w.value.WeaponAttr;
                if (orig == 0) orig = w.value.WeaponAttr.BulletSpeed;
                GUILayout.Label("Weapon : " + w.Value.TypeName, new GUILayoutOption[0]);
                v.Radius = 500.0f;
                v.BulletSpeed = SliderVal;
                v.WieldTime = 0;
                v.UnwieldTime = 0;
                //w.Value.WeaponAttr.LuckyHit = 500;
                //w.Value.WeaponAttr.AttSpeed = 500;
                var dict = w.Value.WeaponAttr.m_PropDict;
                foreach (var a in dict)
                {
                    if (a.Value.PropValueType == PropType.TYPE_INTEGER)
                        GUILayout.Label(a.Key + " : " + a.Value.GetPropValue<System.Int32>(), new GUILayoutOption[0]);
                    else if (a.Value.PropValueType == PropType.TYPE_FLOAT)
                        GUILayout.Label(a.Key + " : " + a.Value.GetPropValue<System.Single>(), new GUILayoutOption[0]);
                    else if (a.Value.PropValueType == PropType.TYPE_LIST_STR)
                        GUILayout.Label(a.Key + " : " + a.Value.GetPropValue<System.String>(), new GUILayoutOption[0]);

                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        public override void OnDisable()
        {
            var ws = HeroMoveManager.HeroObj?.BulletPreFormCom?.weapondict;
            foreach (var w in ws)
            {
                w.value.WeaponAttr.BulletSpeed = orig;
            }
        }
    }
}
