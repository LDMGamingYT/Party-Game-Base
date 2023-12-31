using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QrCodeGenerator: MonoBehaviour {
    private RawImage image;
    private readonly static int size = 256;
    private Texture2D texture;

    void Awake() {
        image = GetComponent<RawImage>();
        texture = new Texture2D(size, size);
    }

    public void Encode(string text) {
        Color32[] pixels = new BarcodeWriter {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions {
                Height = size,
                Width = size
            }
        }.Write(text);
        texture.SetPixels32(pixels);
        texture.Apply();
        image.texture = texture; 
    }
}