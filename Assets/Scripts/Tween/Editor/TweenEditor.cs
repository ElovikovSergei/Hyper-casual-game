using UnityEngine;
using UnityEditor;

namespace Tween
{
    [CustomEditor(typeof(TweenController))]
    public sealed class TweenEditor : Editor
    {
        private TweenController refTarget;

        private readonly string[] tweenTypes = new string[] { "Update", "Fixed", "Late" };
        private int tabID = 0;

        private bool showTweens = false;

        private MonoScript script;

        private void OnEnable()
        {
            refTarget = (TweenController)target;
            script = MonoScript.FromMonoBehaviour(refTarget);
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script:", script, typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();

            showTweens = EditorGUILayout.Foldout(showTweens, "Show Active Tweens");

            if (showTweens)
            {
                tabID = GUILayout.Toolbar(tabID, tweenTypes);

                EditorGUILayout.BeginVertical();

                switch (tabID)
                {
                    case 0:
                        if (refTarget.UpdateTweens == null)
                            break;

                        for (int i = 0; i < refTarget.UpdateTweens.Length; i++)
                        {
                            if (refTarget.UpdateTweens[i] != null)
                            {
                                if (GUILayout.Button(refTarget.UpdateTweens[i].activeId + ": " + refTarget.UpdateTweens[i].GetType()))
                                {
                                    refTarget.UpdateTweens[i].Kill();

                                    break;
                                }
                            }
                        }

                        break;
                    case 1:
                        if (refTarget.FixedTweens == null)
                            break;

                        for (int i = 0; i < refTarget.FixedTweens.Length; i++)
                        {
                            if (refTarget.FixedTweens[i] != null)
                            {
                                if (GUILayout.Button(refTarget.FixedTweens[i].activeId + ": " + refTarget.FixedTweens[i].GetType()))
                                {
                                    refTarget.FixedTweens[i].Kill();

                                    break;
                                }
                            }
                        }

                        break;
                    case 2:
                        if (refTarget.LateTweens == null)
                            break;

                        for (int i = 0; i < refTarget.LateTweens.Length; i++)
                        {
                            if (refTarget.LateTweens[i] != null)
                            {
                                if (GUILayout.Button(refTarget.LateTweens[i].activeId + ": " + refTarget.LateTweens[i].GetType()))
                                {
                                    refTarget.LateTweens[i].Kill();

                                    break;
                                }
                            }
                        }

                        break;
                }

                EditorGUILayout.EndVertical();
            }
        }
    }
}