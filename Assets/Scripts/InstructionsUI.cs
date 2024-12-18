using UnityEngine;
using UnityEngine.UI;

public class InstructionsUI : MonoBehaviour
{
    private Canvas canvas;
    private GameObject panel;
    private GameObject scrollView;
    private GameObject viewport;
    private GameObject content;
    private Text instructionsText;

    void Start()
    {
        // Create the Canvas
        GameObject canvasObject = new GameObject("InstructionsCanvas");
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasObject.AddComponent<GraphicRaycaster>();

        // Create the Panel
        panel = new GameObject("Panel");
        panel.transform.SetParent(canvas.transform);
        RectTransform panelRT = panel.AddComponent<RectTransform>();
        panel.AddComponent<Image>().color = new Color(0, 0, 0, 0.7f); // Semi-transparent black background
        panelRT.anchorMin = new Vector2(0.2f, 0.2f);
        panelRT.anchorMax = new Vector2(0.8f, 0.8f);
        panelRT.offsetMin = Vector2.zero;
        panelRT.offsetMax = Vector2.zero;

        // Create the Scroll View
        scrollView = new GameObject("ScrollView");
        scrollView.transform.SetParent(panel.transform);
        RectTransform scrollRT = scrollView.AddComponent<RectTransform>();
        scrollRT.anchorMin = new Vector2(0.05f, 0.05f);
        scrollRT.anchorMax = new Vector2(0.95f, 0.95f);
        scrollRT.offsetMin = Vector2.zero;
        scrollRT.offsetMax = Vector2.zero;

        // Add ScrollRect and Mask
        ScrollRect scrollRect = scrollView.AddComponent<ScrollRect>();
        GameObject maskObject = new GameObject("Viewport");
        maskObject.transform.SetParent(scrollView.transform);
        viewport = maskObject;
        RectTransform maskRT = maskObject.AddComponent<RectTransform>();
        maskRT.anchorMin = new Vector2(0, 0);
        maskRT.anchorMax = new Vector2(1, 1);
        maskRT.offsetMin = Vector2.zero;
        maskRT.offsetMax = Vector2.zero;
        maskObject.AddComponent<Mask>().showMaskGraphic = false;
        maskObject.AddComponent<Image>().color = new Color(1, 1, 1, 0.5f);

        scrollRect.viewport = maskRT;

        // Create Content GameObject
        content = new GameObject("Content");
        content.transform.SetParent(viewport.transform);
        RectTransform contentRT = content.AddComponent<RectTransform>();
        contentRT.anchorMin = new Vector2(0, 1);
        contentRT.anchorMax = new Vector2(1, 1);
        contentRT.pivot = new Vector2(0.5f, 1);
        contentRT.sizeDelta = new Vector2(0, 600); // Adjust for content size

        scrollRect.content = contentRT;

        // Add Text Instructions
        GameObject textObject = new GameObject("InstructionsText");
        textObject.transform.SetParent(content.transform);
        RectTransform textRT = textObject.AddComponent<RectTransform>();
        textRT.anchorMin = new Vector2(0, 1);
        textRT.anchorMax = new Vector2(1, 1);
        textRT.pivot = new Vector2(0.5f, 1);
        textRT.anchoredPosition = new Vector2(0, -10);
        textRT.sizeDelta = new Vector2(0, 600);

        instructionsText = textObject.AddComponent<Text>();
        instructionsText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        instructionsText.fontSize = 24;
        instructionsText.alignment = TextAnchor.UpperLeft;
        instructionsText.text = GetInstructionsText();
        instructionsText.color = Color.white;
        instructionsText.supportRichText = true;
    }

    // Method to get the instructions text
    private string GetInstructionsText()
    {
        return "Welcome to Endless Runner!\n\n" +
               "How to Play:\n" +
               "1. Press the Spacebar or Swipe Up to Jump.\n" +
               "2. Avoid obstacles by jumping over them.\n" +
               "3. Collect coins to increase your score.\n" +
               "4. The game speeds up as you progress.\n\n" +
               "Controls:\n" +
               "- PC: Use the Spacebar to jump.\n" +
               "- Mobile: Swipe Up to jump.\n\n" +
               "Tips:\n" +
               "1. Timing is key! Jump too early or too late, and you’ll hit the obstacles.\n" +
               "2. Stay focused as the game gets faster.\n" +
               "3. Aim for high scores and challenge your friends!\n\n" +
               "Good Luck and Have Fun!";
    }
}
