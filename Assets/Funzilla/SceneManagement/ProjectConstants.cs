// This file is auto-generated. Modifications are not saved.

namespace Funzilla
{
	public enum SceneID
	{
		GameManager,
		Gameplay,
		END
	}

	public static class Layers
	{
		/// <summary>
		/// Index of layer 'Default'.
		/// </summary>
		public const int Default = 0;

		/// <summary>
		/// Index of layer 'TransparentFX'.
		/// </summary>
		public const int TransparentFX = 1;

		/// <summary>
		/// Index of layer 'Ignore Raycast'.
		/// </summary>
		public const int Ignore_Raycast = 2;

		/// <summary>
		/// Index of layer 'Gameplay'.
		/// </summary>
		public const int Gameplay = 3;

		/// <summary>
		/// Index of layer 'Water'.
		/// </summary>
		public const int Water = 4;

		/// <summary>
		/// Index of layer 'UI'.
		/// </summary>
		public const int UI = 5;

		/// <summary>
		/// Index of layer 'Trap'.
		/// </summary>
		public const int Trap = 6;

		/// <summary>
		/// Index of layer 'Sensor'.
		/// </summary>
		public const int Sensor = 7;

		/// <summary>
		/// Index of layer 'Tablet'.
		/// </summary>
		public const int Tablet = 8;

		/// <summary>
		/// Index of layer 'Character'.
		/// </summary>
		public const int Character = 9;

		/// <summary>
		/// Index of layer 'Road'.
		/// </summary>
		public const int Road = 10;

		/// <summary>
		/// Index of layer 'TabletPhysics'.
		/// </summary>
		public const int TabletPhysics = 11;

		/// <summary>
		/// Index of layer 'DeadTablet'.
		/// </summary>
		public const int DeadTablet = 12;

		/// <summary>
		/// Bitmask of layer 'Default'.
		/// </summary>
		public const int DefaultMask = 1 << 0;

		/// <summary>
		/// Bitmask of layer 'TransparentFX'.
		/// </summary>
		public const int TransparentFXMask = 1 << 1;

		/// <summary>
		/// Bitmask of layer 'Ignore Raycast'.
		/// </summary>
		public const int Ignore_RaycastMask = 1 << 2;

		/// <summary>
		/// Bitmask of layer 'Gameplay'.
		/// </summary>
		public const int GameplayMask = 1 << 3;

		/// <summary>
		/// Bitmask of layer 'Water'.
		/// </summary>
		public const int WaterMask = 1 << 4;

		/// <summary>
		/// Bitmask of layer 'UI'.
		/// </summary>
		public const int UIMask = 1 << 5;

		/// <summary>
		/// Bitmask of layer 'Trap'.
		/// </summary>
		public const int TrapMask = 1 << 6;

		/// <summary>
		/// Bitmask of layer 'Sensor'.
		/// </summary>
		public const int SensorMask = 1 << 7;

		/// <summary>
		/// Bitmask of layer 'Tablet'.
		/// </summary>
		public const int TabletMask = 1 << 8;

		/// <summary>
		/// Bitmask of layer 'Character'.
		/// </summary>
		public const int CharacterMask = 1 << 9;

		/// <summary>
		/// Bitmask of layer 'Road'.
		/// </summary>
		public const int RoadMask = 1 << 10;

		/// <summary>
		/// Bitmask of layer 'TabletPhysics'.
		/// </summary>
		public const int TabletPhysicsMask = 1 << 11;

		/// <summary>
		/// Bitmask of layer 'DeadTablet'.
		/// </summary>
		public const int DeadTabletMask = 1 << 12;
	}

	public static class SceneNames
	{
		public const string INVALID_SCENE = "InvalidScene";

		public static readonly string[] ScenesNameArray =
		{
			"GameManager",
			"Gameplay"
		};

		/// <summary>
		/// Convert from enum to string
		/// </summary>
		public static string GetSceneName(SceneID scene)
		{
			int index = (int) scene;
			if (index > 0 && index < ScenesNameArray.Length)
			{
				return ScenesNameArray[index];
			}
			else
			{
				return INVALID_SCENE;
			}
		}
	}

	public static class ExtentionHelpers
	{
		/// <summary>
		/// Shortcut to change enum to string
		/// </summary>
		public static string GetName(this SceneID scene)
		{
			return SceneNames.GetSceneName(scene);
		}
	}
}