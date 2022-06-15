using System;
using PixelSort.App.Commands;
using Xunit;

namespace PixelSort.App.Tests.Commands
{
    public class RelayCommandTests
  {
    /// <summary>
    /// Test that RelayCommand's CanExecute method throws invalid operation exception
    /// when the CanExecute predicate is null.
    /// </summary>
    [Fact]
    public void RelayCommandCanExecuteThrowsInvalidOperationException()
    {
      // expectations
      Action action = () => { };
      RelayCommand command = new RelayCommand(action, null);

      // act
      var ex = Assert.Throws<InvalidOperationException>(() => command.CanExecute(null));

      // verify
      Assert.Equal("_canExecute", ex.Message);
    }

    /// <summary>
    /// Test that RelayCommand's CanExecute method returns true
    /// when the predicate condition is fulfilled.
    /// </summary>
    [Fact]
    public void RelayCommandCanExecuteReturnsTrue()
    {
      // expectations
      Action action = () => { };
      bool CanExecute(object param) { return true; }
      RelayCommand command = new RelayCommand(action, CanExecute);

      //act
      bool actual = command.CanExecute(null);

      // verify
      Assert.True(actual);
    }

    /// <summary>
    /// Test that RelayCommand's CanExecute method returns false
    /// when the predicate condition is not fulfilled.
    /// </summary>
    [Fact]
    public void RelayCommandCanExecuteReturnsFalse()
    {
      // expectations
      Action action = () => { };
      bool CanExecute(object param) { return false; }
      RelayCommand command = new RelayCommand(action, CanExecute);

      // act
      bool actual = command.CanExecute(null);

      // verify
      Assert.False(actual);
    }

    /// <summary>
    /// Test that RelayCommand gets executed when predicate is true.
    /// </summary>
    [Fact]
    public void RelayCommandExecuteIsCalledOnPredicateTrue()
    {
      // expectations
      bool executed = false;
      Action action = () => { executed = true; };
      bool CanExecute(object param) { return true; }
      RelayCommand command = new RelayCommand(action, CanExecute);

      // act
      command.Execute(null);

      // verify
      Assert.True(executed);
    }

    /// <summary>
    /// Test that RelayCommand gets not executed when predicate is false.
    /// </summary>
    [Fact]
    public void RelayCommandExecuteIsNotCalledOnPredicateFalse()
    {
      // expectations
      bool executed = false;
      Action action = () => { executed = true; };
      bool CanExecute(object param) { return false; }
      RelayCommand command = new RelayCommand(action, CanExecute);

      // act
      command.Execute(null);

      // verify
      Assert.True(executed);
    }
  }
}
