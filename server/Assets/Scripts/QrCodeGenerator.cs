using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QrCodeGenerator: MonoBehaviour {
    [SerializeField] private RawImage rawImage;
    [SerializeField] private int size;
    private Texture2D barcodeTexture;

    void Start() {
        barcodeTexture = new Texture2D(size, size);
    }

    private Color32[] Encode(string text) {
        return new BarcodeWriter {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions {
                Height = size,
                Width = size
            }
        }.Write(text);
    }
}