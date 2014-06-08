using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResizePictures
{
    class Program
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG", ".JPEG",".TIFF" };


        static void Main(string[] args)
        {
            //args = new string[]{@"J:\Brad's Documents\Handziuk.com\Slides\Lake Union sunrise in the fog.jpg"};
            var files = args.Select(s => new FileInfo(s));

            System.Drawing.Size newSize;

            var newSizeFile = files.FirstOrDefault(f => f.Name == "NewSize.txt");
            newSize = GetNewSizeFromFile(newSizeFile);

            //make the destination folder
            var sourceFolder = files.FirstOrDefault(f => f.Directory != null);//pick the first directory
            DirectoryInfo destinationFolder;
            if (sourceFolder != null)
            {
                destinationFolder = Directory.CreateDirectory(Path.Combine(sourceFolder.DirectoryName, "ResizedPictures"));
                //clear out that directory
                foreach (var file in destinationFolder.GetFiles())
                {
                    file.Delete();
                }
                int fileNumber = 1;
                foreach (var file in files)
                {
                    if (ImageExtensions.Contains(file.Extension.ToUpperInvariant()))
                    {
                        using (Bitmap bmp = new Bitmap(file.FullName, false))
                        {
                            // need to find ratio of the two images
                            // pick whether the height or the width is the base dimension
                            // proportion the other one
                            // 
                            var tempResize = ImageResizer.GetSizeToResizeTo(bmp.Size, newSize);
                            using (Bitmap newTemResizedBmp = new Bitmap(bmp, tempResize))
                            {
                                var cropRectangle = ImageResizer.GetCroppedRectangle(tempResize, newSize);
                                using (var finalCroppedBmp = newTemResizedBmp.Clone(cropRectangle, bmp.PixelFormat))
                                {
                                    var savePath = Path.Combine(destinationFolder.FullName, "Slide-" + fileNumber.ToString("00") + "." + System.Drawing.Imaging.ImageFormat.Png.ToString());
                                    finalCroppedBmp.Save(savePath, System.Drawing.Imaging.ImageFormat.Png);
                                    File.Copy(file.FullName, Path.Combine(destinationFolder.FullName, "Slide-" + fileNumber.ToString("00") +"-Lg"+ file.Extension));
                                }
                            }
                        }
                        fileNumber++;
                    }
                }
            }
        }

        

        private static System.Drawing.Size GetNewSizeFromFile(FileInfo newSizeFile)
        {
            System.Drawing.Size newSize=new System.Drawing.Size(650,250);
            if (newSizeFile != null)
            {
                using (var rdr = new StreamReader(newSizeFile.FullName))
                {
                    var lines = rdr.ReadToEnd().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    var strHeight = lines.FirstOrDefault(l => l.Substring(0, 2) == "h:").Replace("h:", "");
                    var strWidth = lines.FirstOrDefault(l => l.Substring(0, 2) == "w:").Replace("w:", "");
                    int height, width;
                    if (int.TryParse(strHeight, out height) && int.TryParse(strWidth, out width))
                    {
                        newSize = new System.Drawing.Size(width, height);
                    }

                }
            }
            return newSize;
        }
    }
}
