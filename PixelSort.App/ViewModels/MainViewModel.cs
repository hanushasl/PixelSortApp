using System.Windows.Input;
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
            throw new System.NotImplementedException();
        }

        private void GenerateRandomPixels()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
