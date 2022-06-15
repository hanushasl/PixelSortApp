using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PixelSort.App.Commands;
using PixelSort.App.ViewModels.Base;

namespace PixelSort.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Members
        
        #endregion

        #region Backing Fields
        private WriteableBitmap _writeableBitmap;
        #endregion

        #region Public Properties

        public WriteableBitmap ImageDataSource
        {
            get => _writeableBitmap;
            set
            {
                if (value != _writeableBitmap)
                {
                    SetProperty(ref _writeableBitmap, value);
                }
            }
        }
        #endregion

        #region Constructors

        public MainViewModel()
        {
            GenerateRandomPixelsCommand = new RelayCommand(GenerateRandomPixels, CanGenerateRandomPixels);
        }
        #endregion

        #region Commands
        public ICommand GenerateRandomPixelsCommand { get; }
        #endregion

        #region Command Actions

        private bool CanGenerateRandomPixels(object obj)
        {
            return true;
        }

        private void GenerateRandomPixels()
        {
            int w = null != ImageDataSource ? (int)ImageDataSource.Width : 400;
            int h = null != ImageDataSource ? (int)ImageDataSource.Height : 300;
            var rnd = new Random();
            var bmp = new WriteableBitmap(w, h, 96, 96, PixelFormats.Bgr32, null);
            var data = Enumerable.Range(0, w * h).ToArray();
            for (int x = 0; x < data.Length; x++)
            {
                int color_data = rnd.Next(0, 256) << 16;
                color_data |= rnd.Next(0, 256) << 8;
                color_data |= rnd.Next(0, 256) << 0;
                data[x] = color_data;
            }
            bmp.WritePixels(new Int32Rect(0, 0, w, h), data, bmp.BackBufferStride, 0);
            ImageDataSource = bmp;
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
