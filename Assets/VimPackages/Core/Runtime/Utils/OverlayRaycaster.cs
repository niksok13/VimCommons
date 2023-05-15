using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VimPackages.Core.Runtime.Utils
{
    public class OverlayRaycaster : BaseRaycaster
    {
        public override Camera eventCamera { get; }
        protected OverlayRaycaster() {}

        private Canvas _canvas;
        private Canvas canvas => _canvas ??= GetComponent<Canvas>();

        private IList<Graphic> _graphics;
        private IList<Graphic> GetGraphics()
        {
            if (_graphics?.Count > 0) return _graphics;
            _graphics = GraphicRegistry.GetRaycastableGraphicsForCanvas(canvas);
            return _graphics;
        }

        public override int sortOrderPriority
        {
            get
            {
                if (canvas.renderMode == RenderMode.ScreenSpaceOverlay) return canvas.sortingOrder;
                return base.sortOrderPriority;
            }
        }

        public override int renderOrderPriority
        {
            get
            {
                if (canvas.renderMode == RenderMode.ScreenSpaceOverlay) return canvas.rootCanvas.renderOrder;
                return base.renderOrderPriority;
            }
        }

        [NonSerialized] static readonly List<Graphic> s_SortedGraphics = new();

        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            if (canvas is not {enabled: true}) return;
            var canvasGraphics = GetGraphics();
            if (canvasGraphics == null || canvasGraphics.Count == 0) return;

            int displayIndex = canvas.targetDisplay;
            var eventPosition = RelativeMouseAtScaled(eventData.position);
            if (eventPosition != Vector3.zero)
            {
                int eventDisplayIndex = (int)eventPosition.z;
                if (eventDisplayIndex != displayIndex) return;
            }
            else
            {
                eventPosition = eventData.position;
#if UNITY_EDITOR
                if (Display.activeEditorGameViewTarget != displayIndex) return;
                eventPosition.z = Display.activeEditorGameViewTarget;
#endif
            }

            float w = Screen.width;
            float h = Screen.height;
            if (displayIndex > 0 && displayIndex < Display.displays.Length)
            {
                w = Display.displays[displayIndex].systemWidth;
                h = Display.displays[displayIndex].systemHeight;
            }
            var pos = new Vector2(eventPosition.x / w, eventPosition.y / h);
            
            if (pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f) return;
            
            int totalCount = canvasGraphics.Count;
            for (int i = 0; i < totalCount; ++i)
            {
                var graphic = canvasGraphics[i];
                if (!graphic.raycastTarget || graphic.canvasRenderer.cull || graphic.depth == -1) continue;
                if (!RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, eventPosition, null, graphic.raycastPadding)) continue;
                if (graphic.Raycast(eventPosition, null)) s_SortedGraphics.Add(graphic);
            }
            s_SortedGraphics.Sort((g1, g2) => g2.depth.CompareTo(g1.depth));
            
            totalCount = s_SortedGraphics.Count;
            for (int i = 0; i < totalCount; ++i)
            {
                var go = s_SortedGraphics[i].gameObject;
                var tf = go.transform;
                
                var castResult = new RaycastResult
                {
                    gameObject = go,
                    module = this,
                    distance = 0,
                    screenPosition = eventPosition,
                    displayIndex = displayIndex,
                    index = resultAppendList.Count,
                    depth = s_SortedGraphics[i].depth,
                    sortingLayer = canvas.sortingLayerID,
                    sortingOrder = canvas.sortingOrder,
                    worldPosition = tf.position,
                    worldNormal = -tf.forward
                };
                resultAppendList.Add(castResult);
            }
            s_SortedGraphics.Clear();
        }

        private static Vector3 RelativeMouseAtScaled(Vector2 position)
        {
            #if !UNITY_EDITOR && !UNITY_WSA
            // If the main display is now the same resolution as the system then we need to scale the mouse position. (case 1141732)
            if (Display.main.renderingWidth != Display.main.systemWidth || Display.main.renderingHeight != Display.main.systemHeight)
            {
                // The system will add padding when in full-screen and using a non-native aspect ratio. (case UUM-7893)
                // For example Rendering 1920x1080 with a systeem resolution of 3440x1440 would create black bars on each side that are 330 pixels wide.
                // we need to account for this or it will offset our coordinates when we are not on the main display.
                var systemAspectRatio = Display.main.systemWidth / (float)Display.main.systemHeight;

                var sizePlusPadding = new Vector2(Display.main.renderingWidth, Display.main.renderingHeight);
                var padding = Vector2.zero;
                if (Screen.fullScreen)
                {
                    var aspectRatio = Screen.width / (float)Screen.height;
                    if (Display.main.systemHeight * aspectRatio < Display.main.systemWidth)
                    {
                        // Horizontal padding
                        sizePlusPadding.x = Display.main.renderingHeight * systemAspectRatio;
                        padding.x = (sizePlusPadding.x - Display.main.renderingWidth) * 0.5f;
                    }
                    else
                    {
                        // Vertical padding
                        sizePlusPadding.y = Display.main.renderingWidth / systemAspectRatio;
                        padding.y = (sizePlusPadding.y - Display.main.renderingHeight) * 0.5f;
                    }
                }

                var sizePlusPositivePadding = sizePlusPadding - padding;

                // If we are not inside of the main display then we must adjust the mouse position so it is scaled by
                // the main display and adjusted for any padding that may have been added due to different aspect ratios.
                if (position.y < -padding.y || position.y > sizePlusPositivePadding.y ||
                     position.x < -padding.x || position.x > sizePlusPositivePadding.x)
                {
                    var adjustedPosition = position;

                    if (!Screen.fullScreen)
                    {
                        // When in windowed mode, the window will be centered with the 0,0 coordinate at the top left, we need to adjust so it is relative to the screen instead.
                        adjustedPosition.x -= (Display.main.renderingWidth - Display.main.systemWidth) * 0.5f;
                        adjustedPosition.y -= (Display.main.renderingHeight - Display.main.systemHeight) * 0.5f;
                    }
                    else
                    {
                        // Scale the mouse position to account for the black bars when in a non-native aspect ratio.
                        adjustedPosition += padding;
                        adjustedPosition.x *= Display.main.systemWidth / sizePlusPadding.x;
                        adjustedPosition.y *= Display.main.systemHeight / sizePlusPadding.y;
                    }

                    var relativePos = Display.RelativeMouseAt(adjustedPosition);

                    // If we are not on the main display then return the adjusted position.
                    if (relativePos.z != 0)
                        return relativePos;
                }

                // We are using the main display.
                return new Vector3(position.x, position.y, 0);
            }
            #endif
            return Display.RelativeMouseAt(position);
        }
    }
}