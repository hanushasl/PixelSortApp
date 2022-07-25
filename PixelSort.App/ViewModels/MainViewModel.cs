using PixelSort.App.Commands;
using PixelSort.App.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            SortPixelsCommand = new RelayCommand(async () => await SortPixels(), CanSortPixels);
        }

        #endregion

        #region Commands

        public ICommand GenerateRandomPixelsCommand { get; }

        public ICommand SortPixelsCommand { get; }
        #endregion

        #region Command Actions

        private bool CanGenerateRandomPixels(object obj)
        {
            return true;
        }

        private void GenerateRandomPixels()
        {
            int w = null != ImageDataSource ? ImageDataSource.PixelWidth : 400;
            int h = null != ImageDataSource ? ImageDataSource.PixelHeight : 300;
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
        
        private bool CanSortPixels(object obj)
        {
            return true;
        }
        private async Task SortPixels()
        {
            // uncomment to simulate longer running operation
            //await Task.Delay(5000);

            var bmp = ImageDataSource;
            Int32Rect r = new Int32Rect(0, 0, bmp.PixelWidth, bmp.PixelHeight);
            int [] pixels = new int[bmp.PixelWidth * bmp.PixelHeight];
            bmp.CopyPixels(r, pixels, bmp.BackBufferStride, 0);
            List<int> resultList = new List<int>();
            for (int row = 0; row < r.Height; ++row)
            {
                List<int> pixelsRow = new ArraySegment<int>(pixels, row * bmp.PixelWidth, bmp.PixelWidth).ToList();
                resultList.AddRange(pixelsRow.OrderBy(x => System.Drawing.Color.FromArgb(x).GetHue()));
            }
            bmp.WritePixels(r, resultList.ToArray(), bmp.BackBufferStride, 0);
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
