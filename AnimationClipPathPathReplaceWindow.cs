using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Yorozu.EditorTool
{
	public class AnimationClipPathPathReplaceWindow : EditorWindow
	{
		[MenuItem("Tools/ReplaceAnimationClipPath")]
		private static void ShowWindow()
		{
			var window = GetWindow<AnimationClipPathPathReplaceWindow>();
			window.titleContent = new GUIContent("AnimationClipPath");
			window.Show();
		}

		[SerializeField]
		private AnimationClip _animationClip;

		private string[] _paths;
		private int _selectIndex;
		private string _replacePath;
		private Vector2 _scrollPosition;

		private void OnGUI()
		{
			using (var check = new EditorGUI.ChangeCheckScope())
			{
				_animationClip = EditorGUILayout.ObjectField(
					"Target AnimationClip",
					_animationClip,
					typeof(AnimationClip),
					false
				) as AnimationClip;

				if (check.changed)
				{
					CachePath();
				}
			}

			if (_paths == null)
				return;

			if (_selectIndex >= 0 && _selectIndex < _paths.Length)
			{
				using (new EditorGUILayout.VerticalScope("box"))
				{
					EditorGUILayout.LabelField("Prev Path", _paths[_selectIndex]);
					_replacePath = EditorGUILayout.TextField("Replace Path", _replacePath);
					if (GUILayout.Button("Replace"))
					{
						ReplacePath();
					}
				}
			}

			using (var scroll = new EditorGUILayout.ScrollViewScope(_scrollPosition))
			{
				_scrollPosition = scroll.scrollPosition;
				for (var i = 0; i < _paths.Length; i++)
				{
					var path = _paths[i];
					using (new EditorGUI.DisabledScope(_selectIndex == i))
					{
						if (GUILayout.Button(path, EditorStyles.miniLabel))
						{
							_selectIndex = i;
						}
					}
				}
			}
		}

		/// <summary>
		/// Path をキャッシュする
		/// </summary>
		private void CachePath()
		{
			_selectIndex = -1;
			if (_animationClip == null)
			{
				_paths = null;
				return;
			}

			var list = new List<string>();
			var bindings = AnimationUtility.GetCurveBindings(_animationClip);
			foreach (var binding in bindings)
			{
				if (list.Contains(binding.path))
					continue;

				list.Add(binding.path);
			}

			_paths = list.ToArray();
		}

		private void ReplacePath()
		{
			if (_animationClip == null || string.IsNullOrEmpty(_replacePath))
				return;

			var actions = new List<Action>();
			var bindings = AnimationUtility.GetCurveBindings(_animationClip);
			foreach (var binding in bindings)
			{
				var curve = AnimationUtility.GetEditorCurve(_animationClip, binding);
				var cb = binding;
				// Replace Path
				if (binding.path.StartsWith(_paths[_selectIndex]))
				{
					cb.path = binding.path.Replace(_paths[_selectIndex], _replacePath);
				}

				actions.Add(() =>
				{
					AnimationUtility.SetEditorCurve(_animationClip, cb, curve);
				});
			}

			_animationClip.ClearCurves();
			foreach (var action in actions)
			{
				action.Invoke();
			}

			EditorUtility.SetDirty(_animationClip);
			AssetDatabase.SaveAssets();

			CachePath();
		}
	}
}
