using System;
using System.Windows;
using System.Windows.Browser;

namespace SilverlightClock
{
  public class SilverlightClockApplication : Application
  {
    public SilverlightClockApplication()
    {
      this.Startup += this.Application_Startup;
      this.Exit += this.Application_Exit;
      this.UnhandledException += this.Application_UnhandledException;
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
      this.RootVisual = new MainPage();
    }

    private void Application_Exit(object sender, EventArgs e)
    {

    }

    private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
    {
      if (!System.Diagnostics.Debugger.IsAttached)
      {
        HtmlPage.Window.Alert("Error:\n" +
                               e.ExceptionObject.GetType().ToString() + "\n" +
                               e.ExceptionObject.Message + "\n" +
                               e.ExceptionObject.StackTrace);
      }      
    }
  }
}
