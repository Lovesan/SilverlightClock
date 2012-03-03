using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Browser;

namespace SilverlightClock
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {          
          InitializeComponent();
          LayoutRoot.Loaded += delegate(object sender, RoutedEventArgs e)
          {
            DateTime dt = System.DateTime.Now;
            double hAngle = ((double)dt.Hour) / 12 * 360 + dt.Minute / 2;
            double mAngle = ((double)dt.Minute) / 60 * 360;
            double sAngle = ((double)dt.Second) / 60 * 360;
            hourAnimation.From = hAngle;
            hourAnimation.To = hAngle + 360;
            minuteAnimation.From = mAngle;
            minuteAnimation.To = mAngle + 360;
            secondAnimation.From = sAngle;
            secondAnimation.To = sAngle + 360;
            clockStoryboard.Begin();
          };          
        }

        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Shapes.Rectangle secondHand;
        
        internal System.Windows.Media.RotateTransform secondHandTransform;
        
        internal System.Windows.Shapes.Rectangle minuteHand;
        
        internal System.Windows.Media.RotateTransform minuteHandTransform;
        
        internal System.Windows.Shapes.Rectangle hourHand;
        
        internal System.Windows.Media.RotateTransform hourHandTransform;
        
        internal System.Windows.Media.Animation.Storyboard clockStoryboard;
        
        internal System.Windows.Media.Animation.DoubleAnimation hourAnimation;
        
        internal System.Windows.Media.Animation.DoubleAnimation minuteAnimation;
        
        internal System.Windows.Media.Animation.DoubleAnimation secondAnimation;
        
        private bool _contentLoaded;
        
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            System.Windows.Application.LoadComponent(this, new System.Uri("MainPage.xaml", System.UriKind.Relative));            
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.secondHand = ((System.Windows.Shapes.Rectangle)(this.FindName("secondHand")));
            this.secondHandTransform = ((System.Windows.Media.RotateTransform)(this.FindName("secondHandTransform")));
            this.minuteHand = ((System.Windows.Shapes.Rectangle)(this.FindName("minuteHand")));
            this.minuteHandTransform = ((System.Windows.Media.RotateTransform)(this.FindName("minuteHandTransform")));
            this.hourHand = ((System.Windows.Shapes.Rectangle)(this.FindName("hourHand")));
            this.hourHandTransform = ((System.Windows.Media.RotateTransform)(this.FindName("hourHandTransform")));
            this.clockStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("clockStoryboard")));
            this.hourAnimation = ((System.Windows.Media.Animation.DoubleAnimation)(this.FindName("hourAnimation")));
            this.minuteAnimation = ((System.Windows.Media.Animation.DoubleAnimation)(this.FindName("minuteAnimation")));
            this.secondAnimation = ((System.Windows.Media.Animation.DoubleAnimation)(this.FindName("secondAnimation")));   
            _contentLoaded = true;
        }
    }
}
