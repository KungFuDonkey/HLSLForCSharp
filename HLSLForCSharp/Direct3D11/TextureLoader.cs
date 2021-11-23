using System.Drawing;
using System.Drawing.Imaging;
using SharpDX.Direct3D11;
using SharpDX;
namespace HLSLForCSharp.Direct3D11
{
    /// <summary>
    /// Loads textures, and transfroms them into Texture2D objects
    /// </summary>
    public class TextureLoader
    {
        public static Texture2D CreateTexture2DFromBitmapFile(string path)
        {
            Bitmap bitmap = new Bitmap(path);
            Texture2D tex = CreateTexture2DFromBitmap(DeviceManager.device, bitmap);
            bitmap.Dispose();
            return tex;
        }
        public static Texture2D CreateTexture2DFromBitmapFile(Device device, string path)
        {
            Bitmap bitmap = new Bitmap(path);
            Texture2D tex = CreateTexture2DFromBitmap(device, bitmap);
            bitmap.Dispose();
            return tex;
        }
        public static Texture2D CreateTexture2DFromBitmap(Bitmap bitmap)
        {
            return CreateTexture2DFromBitmap(DeviceManager.device, bitmap);
        }
        public static Texture2D CreateTexture2DFromBitmap(Device device, Bitmap bitmap)
        {
            bool dispose = false;
            if(bitmap.PixelFormat != PixelFormat.Format32bppArgb)
            {
                bitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb);
                dispose = true;
            }
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Texture2D returnData = new Texture2D(device, new Texture2DDescription()
            {
                Width = bitmap.Width,
                Height = bitmap.Height,
                ArraySize = 1,
                BindFlags = BindFlags.ShaderResource,
                Usage = ResourceUsage.Immutable,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1,0),
            }, new DataRectangle(data.Scan0,data.Stride));

            bitmap.UnlockBits(data);

            if (dispose)
            {
                bitmap.Dispose();
            }
            return returnData;
        }
    }
}
