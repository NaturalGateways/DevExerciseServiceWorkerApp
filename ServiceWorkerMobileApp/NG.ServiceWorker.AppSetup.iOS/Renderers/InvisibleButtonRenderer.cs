using System;
using System.Linq;
using System.Windows.Input;

using Foundation;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NG.ServiceWorker.UI.InvisibleButton), typeof(NG.ServiceWorker.iOS.Renderers.InvisibleButtonRenderer))]

namespace NG.ServiceWorker.iOS.Renderers
{
    public class InvisibleButtonRenderer : ViewRenderer<UI.InvisibleButton, InvisibleButtonUIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<UI.InvisibleButton> e)
        {
            if (e.NewElement != null && this.Control == null)
            {
                SetNativeControl(new InvisibleButtonUIView(e.NewElement));
            }
        }
    }

    public class InvisibleButtonUIView : UIView
    {
        /// <summary>The renderer.</summary>
        private UI.InvisibleButton m_formsView = null;

        /// <summary>The current touch.</summary>
        private UITouch m_touchDown = null;

        /// <summary>Constructor.</summary>
        public InvisibleButtonUIView(UI.InvisibleButton formsView)
        {
            m_formsView = formsView;
        }

        /// <summary>Touch began.</summary>
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            if (m_touchDown == null && touches.Count == 1)
            {
                m_touchDown = (UITouch)touches.AnyObject;
                m_formsView.ShowPressed = true;
            }
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            if (m_touchDown != null && touches.ToArray().Any(x => object.ReferenceEquals(x, m_touchDown)))
            {
                CoreGraphics.CGPoint location = m_touchDown.LocationInView(this);
                if (location.X < 0 || location.Y < 0 || this.Bounds.Width < location.X || this.Bounds.Height < location.Y)
                {
                    m_touchDown = null;
                    m_formsView.ShowPressed = false;
                }
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (m_touchDown != null && touches.ToArray().Any(x => object.ReferenceEquals(x, m_touchDown)))
            {
                ICommand buttonCommand = m_formsView.Command;
                if (buttonCommand != null)
                {
                    buttonCommand.Execute(null);
                }
                m_touchDown = null;
                m_formsView.ShowPressed = false;
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            if (m_touchDown != null && touches.ToArray().Any(x => object.ReferenceEquals(x, m_touchDown)))
            {
                m_touchDown = null;
                m_formsView.ShowPressed = false;
            }
        }
    }
}
