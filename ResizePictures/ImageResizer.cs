using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResizePictures
{
    public class ImageResizer
    {

        public static Size GetSizeToResizeTo(Size originalSize, Size finalSize)
        {
            var originalAspectRatio = GetAspectRatio(originalSize);
            var newAspectRatio = GetAspectRatio(finalSize);

            if (originalAspectRatio < newAspectRatio)   // Height dominates
            {
                return new Size(finalSize.Width, (int)Math.Round(finalSize.Width / originalAspectRatio));
            }
            else if (originalAspectRatio > newAspectRatio)  //width dominates
            {
                return new Size( (int)Math.Round(finalSize.Height * originalAspectRatio), finalSize.Height);
            }
            else
            {
                return finalSize;
            }
        }

        public static Rectangle GetCroppedRectangle(Size inputSize, Size finalSize)
        {
            var originalAspectRatio = GetAspectRatio(inputSize);
            var newAspectRatio = GetAspectRatio(finalSize);

            if (originalAspectRatio < newAspectRatio)   // Height dominates
            {
                var y = (int)Math.Round((double)(inputSize.Height - finalSize.Height) / 2);
                var x = 0;
                return new Rectangle(new Point(x, y), finalSize);
            }
            else if (originalAspectRatio > newAspectRatio)  //width dominates
            {
                var x = (int)Math.Round((double)(inputSize.Width - finalSize.Width) / 2);
                var y = 0;
                return new Rectangle(new Point(x, y), finalSize);
            }
            else
            {
                return new Rectangle(new Point(0, 0), finalSize);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static decimal GetAspectRatio(Size size)
        {
            return (decimal)size.Width / (decimal)size.Height;
        }
    }
}
