using Cysharp.Threading.Tasks;
using GameFoundation.Scripts.UIModule.Utilities.LoadImage;
using GDK_TrongLe.UniCore.Extension.Unity;
using UnityEngine;
using UnityEngine.UI;

public class TestLoadImage : MonoBehaviour
{
    public  Image           targetImage; // Gán Image từ Inspector
    private LoadImageHelper loadImageHelper => this.GetCurrentContainer().Resolve<LoadImageHelper>();

    private void Start()
    {
        // URL hình ảnh cần tải
        var imageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/70/Example.png";

        // Tải ảnh và gán vào targetImage
        this.LoadImageFromURL(imageUrl).Forget();
    }

    private async UniTaskVoid LoadImageFromURL(string url) { await this.loadImageHelper.LoadSpriteFromUrl(this.targetImage, url); }
}