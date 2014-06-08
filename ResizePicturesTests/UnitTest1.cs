using System;
using ResizePictures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace ResizePicturesTests
{
    [TestClass]
    public class ImageResizerTests
    {
        [TestMethod]
        public void TestAspectRatio()
        {
            int h = 10;
            int w = 20;
            var expectedAr = 2;
            
            Size size = new Size(w,h);
            var returnedAr = ImageResizer.GetAspectRatio(size);
            Assert.AreEqual(expectedAr, returnedAr);
        }
        [TestMethod]
        public void TestSizeToResizeToHightDominates()
        {
            Size originalSize = new Size(6384, 1556);
            Size newSize = new Size(650,250);

            Size resultingSize = ImageResizer.GetSizeToResizeTo(originalSize, newSize);
            Size expectedSize = new Size(1026, 250);
            Assert.AreEqual(resultingSize, expectedSize);
        }

        [TestMethod]
        public void TestSizeToResizeToWidthDominates()
        {
            Size originalSize = new Size(3264, 1836);
            Size newSize = new Size(650, 250);

            Size resultingSize = ImageResizer.GetSizeToResizeTo(originalSize, newSize);
            Size expectedSize = new Size(650, 366);
            Assert.AreEqual(resultingSize, expectedSize);
        }

        [TestMethod]
        public void TestGetCroppedRectangleCenterHeight()
        {
            Size inputSize = new Size(650, 366);
            Size finalSize = new Size(650, 250);
            Rectangle expectedRectangle = new Rectangle(new Point(0, 58), finalSize);
            Rectangle actualRectangle = ImageResizer.GetCroppedRectangle(inputSize, finalSize);
            Assert.AreEqual(expectedRectangle, actualRectangle);
        }

        [TestMethod]
        public void TestGetCroppedRectangleCenterWidth()
        {
            Size inputSize = new Size(1026, 250);
            Size finalSize = new Size(650, 250);
            Rectangle expectedRectangle = new Rectangle(new Point(188, 0), finalSize);
            Rectangle actualRectangle = ImageResizer.GetCroppedRectangle(inputSize, finalSize);
            Assert.AreEqual(expectedRectangle, actualRectangle);
        }
    }
}
