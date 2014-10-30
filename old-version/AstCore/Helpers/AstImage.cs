using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Web;

namespace AstCore.Helpers
{
    /// <summary>
    ///     Represents the ImageHelper class.
    /// </summary>
    [Serializable]
    public class AstImage
    {
        #region Private Variables

        private const int MaxCatalogItemLargeWidth = 450;

        private const int MaxCatalogItemMediumWidth = 200;

        private const int MaxCatalogItemSmallWidth = 50;

        private const string DataPath = "~/img/uploaded/";

        /// <summary>
        ///     No Image File Name
        /// </summary>
        private const string NoImageFileName = "noimage.gif";

        /// <summary>
        ///     Is Already checked
        /// </summary>
        private static bool _isAlreadyChecked;

        private static string ImagePath
        {
            get { return DataPath + "catalog/"; }
        }

        private static string OriginalImagePath
        {
            get { return DataPath + "catalog/original/"; }
        }

        #endregion

        /// <summary>
        ///     Get the large size image relative path.
        /// </summary>
        /// <param name="imageFileName">Image file name.</param>
        /// <returns>Returns the large size image relative path.</returns>
        public static string GetImageHttpPathLarge(string imageFileName)
        {
            return GetRelativeImageUrl(MaxCatalogItemLargeWidth, imageFileName, false);
        }

        /// <summary>
        ///     Get the small medium size image relative path.
        /// </summary>
        /// <param name="imageFileName">Image file name.</param>
        /// <returns>Returns the medium size image relative path.</returns>
        public static string GetImageHttpPathMedium(string imageFileName)
        {
            return GetRelativeImageUrl(MaxCatalogItemMediumWidth, imageFileName, false);
        }

        /// <summary>
        ///     Get the small image relative path.
        /// </summary>
        /// <param name="imageFileName">Image file name.</param>
        /// <returns>Returns the small image relative path.</returns>
        public static string GetImageHttpPathSmall(string imageFileName)
        {
            return GetRelativeImageUrl(MaxCatalogItemSmallWidth, imageFileName, false);
        }

        /// <summary>
        ///     Get Image By size
        /// </summary>
        /// <param name="imageSize">Image file size</param>
        /// <param name="imageFileName">Image file name.</param>
        /// <returns>Returns the image by specified size</returns>
        public string GetImageBySize(int imageSize, string imageFileName)
        {
            return GetRelativeImageUrl(imageSize, imageFileName, false);
        }

        public string GetStoreLogoBySize(Size imageSize, string imageFileName)
        {
            string resizedImageDirectory = string.Format("{0}images/catalog/{1}/", DataPath, imageSize.Width);

            if (!File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(resizedImageDirectory, imageFileName))))
            {
                return ResizeImage(imageFileName, imageSize.Height, imageSize.Width, resizedImageDirectory);
            }

            return Path.Combine(resizedImageDirectory, imageFileName);
        }

        #region Helper Methods

        /// <summary>
        ///     Get the relative image path to a resized image. If the resized image does not exist then it will be created. If the image does not exist.
        ///     then the "No Image" image will be returned.
        /// </summary>
        /// <param name="imageSize">Image Size you want to get.</param>
        /// <param name="imageFileName">The name of the image you want to get.</param>
        /// <param name="cropImage">Indicates whether to crop the image.</param>
        /// <param name="imageFileExists">Returns true if image file exists else false.</param>
        /// <returns>Returns the resized image file path.</returns>
        public static string GetImageHttpPath(int imageSize, string imageFileName, bool cropImage, out bool imageFileExists)
        {
            string imageFileFullName = string.Empty;
            string imageFilePath = string.Empty;
            string fileName = string.Empty;

            // Build up a path for our resized image.
            imageFilePath = Path.Combine(ImagePath, imageSize.ToString(CultureInfo.InvariantCulture)) + "/";
            imageFileFullName = Path.Combine(imageFilePath, imageFileName);

            // If file exists and resized image folder and no new image uploaded in original folder then use already resized image.
            if (File.Exists(HttpContext.Current.Server.MapPath(imageFileFullName)))
            {
                imageFileExists = true;
                return imageFileFullName;
            }
            else
            {
                string originalFileName = cropImage ? imageFileName.ToLower().Replace("-swatch.", ".") : imageFileName;


                string orignalImagePath = Path.Combine(OriginalImagePath, originalFileName);


                imageFileExists = false;

                // Check is file exist in Original folder.
                if (File.Exists(HttpContext.Current.Server.MapPath(orignalImagePath)))
                {
                    // Resize the image for the current request image size.
                    fileName = ResizeImage(orignalImagePath, imageSize, imageFilePath);

                    // Crop the image only for swatch.
                    if (cropImage)
                    {
                        string croppedImageFileName =
                            Path.GetFileName(CropImage(orignalImagePath, MaxCatalogItemSmallWidth, MaxCatalogItemSmallWidth,
                                                       fileName.ToLower().Replace("-swatch.", ".")));
                        fileName = Path.Combine(imageFilePath, croppedImageFileName);
                    }
                }
                else
                {
                    // If source file doesn't exist in Original folder then return the Image Not Available file.
                    string noImageRelativePath = string.Empty;


                    noImageRelativePath = Path.Combine(OriginalImagePath, NoImageFileName);
                    if (File.Exists(HttpContext.Current.Server.MapPath(noImageRelativePath)))
                    {
                        // Call the current method recursively with the "Original" folder this.noImageFileName.
                        fileName = GetImageHttpPath(imageSize, NoImageFileName, cropImage, out imageFileExists);

                        if (cropImage)
                        {
                            fileName = Path.Combine(imageFilePath,
                                                    Path.GetFileNameWithoutExtension(fileName) + "-swatch" +
                                                    Path.GetExtension(fileName));
                        }
                    }
                    else
                    {
                        // Image not available in Data/Default/Images and Data/Default/Images/Catalog/Original folders.
                        fileName = imageFileFullName;
                    }
                }


                return fileName;
            }
        }

        /// <summary>
        ///     Resizing the image size and storing it in the respective folder.
        /// </summary>
        /// <param name="relativeImageFilePath">Relative Image file path.</param>
        /// <param name="maxHeight">Maximum Height of the image.</param>
        /// <param name="maxWidth">Maximum width of the image.</param>
        /// <param name="saveToFullPath">Physical path</param>
        /// <returns>Returns the resised image relative path.</returns>
        public string ResizeImage(string relativeImageFilePath, int maxHeight, int maxWidth, string saveToFullPath)
        {
            string returnUrl = string.Empty;
            string fullName = string.Empty;
            string fileName = Path.GetFileName(relativeImageFilePath);


            fullName = Path.Combine(OriginalImagePath, fileName);


            byte[] fileData = File.ReadAllBytes(HttpContext.Current.Server.MapPath(fullName));
            var ms = new MemoryStream(fileData);
            Image sourceImage = Image.FromStream(ms);
            decimal scaleFactor;
            decimal originalProportion = sourceImage.Width / sourceImage.Height;
            decimal resizeProportion = maxWidth / maxHeight;
            if (originalProportion > resizeProportion)
            {
                scaleFactor = Convert.ToDecimal(maxWidth) / Convert.ToDecimal(sourceImage.Width);
            }
            else
            {
                scaleFactor = Convert.ToDecimal(maxHeight) / Convert.ToDecimal(sourceImage.Height);
            }

            var newWidth = (int)Math.Round((sourceImage.Width * scaleFactor));
            var newHeight = (int)Math.Round((sourceImage.Height * scaleFactor));
            var thumbnailBitmap = new Bitmap(newWidth, newHeight);

            Graphics thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            thumbnailGraph.Clear(Color.White);

            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraph.DrawImage(sourceImage, imageRectangle);

            var stream = new MemoryStream();
            Image imageToSave = thumbnailBitmap;
            imageToSave.Save(stream, ImageFormat.Jpeg);
            imageToSave.Dispose();
            stream.Seek(0, SeekOrigin.Begin);
            fileData = new byte[stream.Length];
            stream.Read(fileData, 0, fileData.Length);

            FileHelper.WriteBinaryStorage(fileData, Path.Combine(saveToFullPath, fileName));

            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            sourceImage.Dispose();

            ms.Close();

            return Path.Combine(saveToFullPath, fileName);
        }

        /// <summary>
        ///     Resize the image without losing quality
        /// </summary>
        /// <param name="orignalImagePath">Original Image Path</param>
        /// <param name="maxSize">Maximum file resize size.</param>
        /// <param name="saveToFullPath">Save to physical path.</param>
        /// <returns>Returns the image physical path with file name.</returns>
        public static string ResizeImage(string orignalImagePath, int maxSize, string saveToFullPath)
        {
            string fileName = Path.GetFileName(orignalImagePath);

            string fullName = OriginalImagePath + fileName;

            byte[] bytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath(fullName));

            if (bytes.Length == 0)
            {
                return null;
            }

            var ms = new MemoryStream(bytes);
            Image sourceImage = Image.FromStream(ms);
            decimal scaleFactor;
            if (sourceImage.Width >= sourceImage.Height)
            {
                scaleFactor = Convert.ToDecimal(maxSize) / Convert.ToDecimal(sourceImage.Width);
            }
            else
            {
                scaleFactor = Convert.ToDecimal(maxSize) / Convert.ToDecimal(sourceImage.Height);
            }

            var newWidth = (int)Math.Round((sourceImage.Width * scaleFactor));
            var newHeight = (int)Math.Round((sourceImage.Height * scaleFactor));
            var thumbnailBitmap = new Bitmap(newWidth, newHeight);

            Graphics thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            thumbnailGraph.Clear(Color.White);

            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraph.DrawImage(sourceImage, imageRectangle);

            var stream = new MemoryStream();
            Image imageToSave = thumbnailBitmap;
            imageToSave.Save(stream, ImageFormat.Jpeg);
            imageToSave.Dispose();
            stream.Seek(0, SeekOrigin.Begin);
            bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            FileHelper.WriteBinaryStorage(bytes, Path.Combine(saveToFullPath, fileName));

            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            sourceImage.Dispose();

            ms.Close();

            return Path.Combine(saveToFullPath, fileName);
        }

        /// <summary>
        ///     Method for cropping an image.
        /// </summary>
        /// <param name="orignalImagePath">Original Image path</param>
        /// <param name="width">New width of the image.</param>
        /// <param name="height">New height of the image</param>
        /// <param name="saveToFullPath">Save to full path.</param>
        /// <returns>Returns the image relative path.</returns>
        public static string CropImage(string orignalImagePath, int width, int height, string saveToFullPath)
        {
            string fileName = orignalImagePath;


            // Original image
            byte[] fileData = File.ReadAllBytes(HttpContext.Current.Server.MapPath(fileName));
            var stream = new MemoryStream(fileData);
            Image imgPhoto = Image.FromStream(stream);

            int targetW = width;
            int targetH = height;
            int targetX = 0;
            int targetY = 0;

            int pointX = imgPhoto.Width / 2;
            int pointY = imgPhoto.Height / 2;

            targetX = pointX - (targetW / 2);
            targetY = pointY - (targetH / 2) - 2;

            var bmpPhoto = new Bitmap(targetW, targetH, PixelFormat.Format24bppRgb);
            bmpPhoto.SetResolution(80, 60);

            Graphics gfxPhoto = Graphics.FromImage(bmpPhoto);
            gfxPhoto.CompositingQuality = CompositingQuality.HighQuality;
            gfxPhoto.SmoothingMode = SmoothingMode.HighQuality;
            gfxPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfxPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

            gfxPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, targetW, targetH), targetX, targetY, targetW, targetH,
                               GraphicsUnit.Pixel);

            fileName = Path.Combine(Path.GetDirectoryName(saveToFullPath),
                                    Path.GetFileNameWithoutExtension(saveToFullPath) + "-Swatch" +
                                    Path.GetExtension(fileName));

            stream = new MemoryStream();
            Image imageToSave = bmpPhoto;
            imageToSave.Save(stream, ImageFormat.Jpeg);
            imageToSave.Dispose();
            stream.Seek(0, SeekOrigin.Begin);
            fileData = new byte[stream.Length];
            stream.Read(fileData, 0, fileData.Length);

            FileHelper.WriteBinaryStorage(fileData, fileName);

            // Dispose of all the objects to prevent memory leaks
            imgPhoto.Dispose();
            bmpPhoto.Dispose();
            gfxPhoto.Dispose();

            return fileName;
        }

        /// <summary>
        ///     Resize the mobile splash images. Splash image width hard-coded as 320 and 640.
        /// </summary>
        /// <param name="relativeImageFilePath">Relative Image File Path.</param>
        public void ResizeMobileSplashImage(string relativeImageFilePath)
        {
            string splashImageWidthSmallDir = Path.Combine(DataPath, "images/splash/320");
            string splashImageWidthLargeDir = Path.Combine(DataPath, "images/splash/640");

            ResizeImage(relativeImageFilePath, 320, splashImageWidthSmallDir + "/");
            ResizeImage(relativeImageFilePath, 640, splashImageWidthLargeDir + "/");
        }

        /// <summary>
        ///     Get a resized image relative path.
        /// </summary>
        /// <param name="imageSize">Image Size</param>
        /// <param name="imageFileName">Image file name.</param>
        /// <param name="cropImage">Crop Image</param>
        /// <returns>Returns the resized relative image path.</returns>
        public static string GetRelativeImageUrl(int imageSize, string imageFileName, bool cropImage)
        {
            string returnFileName = string.Empty;
            bool isImageFileExists = true;

            if (string.IsNullOrEmpty(imageFileName))
            {
                imageFileName = NoImageFileName;
            }

            returnFileName = GetImageHttpPath(imageSize, imageFileName, cropImage, out isImageFileExists);

            if (!isImageFileExists && !_isAlreadyChecked)
            {
                // Call the current function recursively to load image on first time.
                returnFileName = GetRelativeImageUrl(imageSize, Path.GetFileName(returnFileName), cropImage);
            }

            // Reset the recursion check to default value.
            _isAlreadyChecked = false;

            return returnFileName;
        }

        #endregion
    }
}