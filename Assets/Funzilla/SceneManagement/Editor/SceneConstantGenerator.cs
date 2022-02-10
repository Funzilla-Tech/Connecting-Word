using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace UnityToolbag
{
	public static class UnityConstantsGenerator
	{
		[MenuItem("Tools/Update ProjectConstants.cs")]
		public static void Generate()
		{
			// Try to find an existing file in the project called "UnityConstants.cs"
			string filePath = string.Empty;
			foreach (var file in Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories)) {
				if (Path.GetFileNameWithoutExtension(file) == "ProjectConstants") {
					filePath = file;
					break;
				}
			}

			// If no such file exists already, use the save panel to get a folder in which the file will be placed.
			if (string.IsNullOrEmpty(filePath)) {
				string directory = EditorUtility.OpenFolderPanel("Choose location for file ProjectConstants.cs", Application.dataPath, "");

				// Canceled choose? Do nothing.
				if (string.IsNullOrEmpty(directory)) {
					return;
				}

				filePath = Path.Combine(directory, "ProjectConstants.cs");
			}

			// Write out our file
			using (var writer = new StreamWriter(filePath)) {
				writer.WriteLine("// This file is auto-generated. Modifications are not saved.");
				writer.WriteLine();
				writer.WriteLine("namespace Funzilla");
				writer.WriteLine("{");

				// Write out the Enum
				writer.WriteLine("    public enum SceneID");
				writer.WriteLine("    {");
				for (int i = 0; i < EditorBuildSettings.scenes.Length; i++) {
					string scene = Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[i].path);
						writer.WriteLine("        {0},", scene);
				}
				writer.WriteLine("        END");
				writer.WriteLine("    }");
				writer.WriteLine();

				// Write out layers
				writer.WriteLine("    public static class Layers");
				writer.WriteLine("    {");
				for (int i = 0; i < 32; i++) {
					string layer = UnityEditorInternal.InternalEditorUtility.GetLayerName(i);
					if (!string.IsNullOrEmpty(layer)) {
						writer.WriteLine("        /// <summary>");
						writer.WriteLine("        /// Index of layer '{0}'.", layer);
						writer.WriteLine("        /// </summary>");
						writer.WriteLine("        public const int {0} = {1};", MakeSafeForCode(layer), i);
					}
				}
				writer.WriteLine();
				for (int i = 0; i < 32; i++) {
					string layer = UnityEditorInternal.InternalEditorUtility.GetLayerName(i);
					if (!string.IsNullOrEmpty(layer)) {
						writer.WriteLine("        /// <summary>");
						writer.WriteLine("        /// Bitmask of layer '{0}'.", layer);
						writer.WriteLine("        /// </summary>");
						writer.WriteLine("        public const int {0}Mask = 1 << {1};", MakeSafeForCode(layer), i);
					}
				}
				writer.WriteLine("    }");
				writer.WriteLine();

				// Write out scenes' names
				writer.WriteLine("    public static class SceneNames");
				writer.WriteLine("    {");
				writer.WriteLine("        public const string INVALID_SCENE = \"InvalidScene\";");

				// Scenes' names in an array
				writer.WriteLine("        public static readonly string[] ScenesNameArray = {");
				for (int i = 0; i < EditorBuildSettings.scenes.Length; i++) {
					string scene = Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[i].path);
					if (i == EditorBuildSettings.scenes.Length - 1) {
						writer.WriteLine("            \"{0}\"", scene);
					} else {
						writer.WriteLine("            \"{0}\",", scene);
					}
				}
				writer.WriteLine("        };");

				//write method to get scene name from enum
				writer.WriteLine("        /// <summary>");
				writer.WriteLine("        /// Convert from enum to string");
				writer.WriteLine("        /// </summary>");
				writer.WriteLine("        public static string GetSceneName(SceneID scene) {");
				writer.WriteLine("              int index = (int)scene;");
				writer.WriteLine("              if(index > 0 && index < ScenesNameArray.Length) {");
				writer.WriteLine("                  return ScenesNameArray[index];");
				writer.WriteLine("              } else {");
				writer.WriteLine("                  return INVALID_SCENE;");
				writer.WriteLine("              }");
				writer.WriteLine("        }");

				writer.WriteLine("    }");
				writer.WriteLine();

				// Write static function to get scene name string from enum
				writer.WriteLine("    public static class ExtentionHelpers {");
				writer.WriteLine("        /// <summary>");
				writer.WriteLine("        /// Shortcut to change enum to string");
				writer.WriteLine("        /// </summary>");
				writer.WriteLine("        public static string GetName(this SceneID scene) {");
				writer.WriteLine("              return SceneNames.GetSceneName(scene);");
				writer.WriteLine("        }");
				writer.WriteLine("    }");

				// End of namespace UnityConstants
				writer.WriteLine("}");
				writer.WriteLine();
			}

			// Refresh
			AssetDatabase.Refresh();

			Debug.Log("Project Constants successfully generated");
		}

		private static string MakeSafeForCode(string str)
		{
			str = Regex.Replace(str, "[^a-zA-Z0-9_]", "_", RegexOptions.Compiled);
			if (char.IsDigit(str[0])) {
				str = "_" + str;
			}
			return str;
		}
	}
}