using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2_Lab1
{
    enum LightingLevel { Low, Medium, High };
    enum FlashButton { Off, On, AutoSet };

    class Camera{
        List<Photo> photos;

        public Camera()
        {
            photos = new List<Photo>();
        }

        public void takePhoto(LightingLevel lightingLevel, FlashButton flashButton)
        {
            AbstractCameraMode mode = getConfigs(lightingLevel, flashButton);
            Photo photo = mode.takeAPhoto();
            photos.Add(photo);
        }

        private AbstractCameraMode getConfigs(LightingLevel lightingLevel, FlashButton flashbutt)
        {
            if (flashbutt == FlashButton.On)
                return new AbstractCameraMode(new LightingLevelCameraModeLow());
            else if (flashbutt == FlashButton.Off)
                return new AbstractCameraMode(new LightingLevelCameraModeHigh());
            else
                switch (lightingLevel)
                {
                    case LightingLevel.Low:
                        return new AbstractCameraMode(new LightingLevelCameraModeLow());
                    case LightingLevel.Medium:
                    case LightingLevel.High:
                        return new AbstractCameraMode(new LightingLevelCameraModeHigh());
                    default: return null;
                }
        }
    }

    public class Photo
    {
        private int buffer;
        private bool lit;

        public Photo()
        {
            lit = false;
        }

        public void setLitProp()
        {
            //there's photo lights
            lit = true;
        }
    }

    interface ICameraMode
    {
        Photo takeAPhoto();
    }

    class AbstractCameraMode
    {
        ICameraMode cameraMode;

        public AbstractCameraMode(ICameraMode cameraMode)
        {
            this.cameraMode = cameraMode;
        }

        public Photo takeAPhoto()
        {
            return this.cameraMode.takeAPhoto();
        }
    }

    class LightingLevelCameraModeHigh : ICameraMode
    {
        public Photo takeAPhoto()
        {
            Photo photo = new Photo();
            Console.WriteLine("Got a photo. Flash wasn't activated");
            return photo;
        }
    }

    class LightingLevelCameraModeLow : ICameraMode
    {
        public Photo takeAPhoto()
        {
            Photo photo = new Photo();
            Console.WriteLine("Got a photo. Flash was activated");
            photo.setLitProp();
            return photo;
        }
    }
}
