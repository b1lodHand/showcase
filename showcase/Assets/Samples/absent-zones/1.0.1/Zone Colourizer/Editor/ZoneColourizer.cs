using com.absence.zonesystem.internals;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace com.absence.zonesystem.editor.samples
{
    /// <summary>
    /// The static class responsible for colorizing the zones created by the <see cref="ZoneCreationHandler"/>.
    /// </summary>
    [InitializeOnLoad]
    public static class ZoneColourizer
    {
        public static readonly float DefaultColorAlpha = 0.5f;

        public static readonly List<Color> Colors = new()
        {
            Color.magenta,
            Color.blue,
            Color.cyan,
            Color.green,
            Color.red,
            Color.yellow,
        };

        static ZoneColourizer()
        {
            ZoneCreationHandler.OnZoneCreation -= OnZoneCreation;
            ZoneCreationHandler.OnZoneCreation += OnZoneCreation;
        }

        private static void OnZoneCreation(IZone zone)
        {
            ZoneGizmoData gizmoData = zone.GizmoData;
            gizmoData.GizmoColor = GetRandomColor();
        }

        public static Color GetRandomColor()
        {
            int randomIndex = Random.Range(0, Colors.Count);
            return Colors[randomIndex].WithAlpha(DefaultColorAlpha);
        }

        private static Color WithAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
    }
}
