using System;
using System.ComponentModel;
using System.Windows.Input;

using Android.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(NG.ServiceWorker.UI.InvisibleButton), typeof(NG.ServiceWorker.Droid.Renderers.InvisibleButtonRenderer))]

namespace NG.ServiceWorker.Droid.Renderers
{
    public class InvisibleButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<UI.InvisibleButton, InvisibleButtonAndroidView>
    {
        /// <summary>Constructor.</summary>
        public InvisibleButtonRenderer(Context context) : base(context)
        {
            //
        }

        /// <summary>Sets up the native view.</summary>
        protected override void OnElementChanged(ElementChangedEventArgs<UI.InvisibleButton> e)
        {
            // See if we create the native control
            if (e.NewElement != null && this.Control == null)
            {
                SetNativeControl(new InvisibleButtonAndroidView(this.Context, e.NewElement));
            }
        }
    }

    public class InvisibleButtonAndroidView : Android.Views.View
    {
        /// <summary>The renderer.</summary>
        private UI.InvisibleButton m_formsView = null;

        /// <summary>The renderer.</summary>
        private int? m_downActionIndex = null;

        /// <summary>Constructor.</summary>
        public InvisibleButtonAndroidView(Context context, UI.InvisibleButton formsView) : base(context)
        {
            m_formsView = formsView;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                m_downActionIndex = e.ActionIndex;
                m_formsView.ShowPressed = true;
                return true;
            }
            else if (m_downActionIndex.HasValue && m_downActionIndex.Value == e.ActionIndex)
            {
                if (e.Action == MotionEventActions.Move)
                {
                    int[] viewOffset = new int[2];
                    this.GetLocationOnScreen(viewOffset);
                    float viewLeft = e.RawX - viewOffset[0];
                    float viewTop = e.RawY - viewOffset[1];
                    if (this.Left <= viewLeft && viewLeft <= this.Right && this.Top <= viewTop && viewTop <= this.Bottom)
                    {
                        return true;
                    }
                    else
                    {
                        m_downActionIndex = null;
                        m_formsView.ShowPressed = false;
                        return false;
                    }
                }
                else if (e.Action == MotionEventActions.Up)
                {
                    ICommand buttonCommand = m_formsView.Command;
                    if (buttonCommand != null)
                    {
                        buttonCommand.Execute(null);
                    }
                    m_downActionIndex = null;
                    m_formsView.ShowPressed = false;
                    return true;
                }
                else if (e.Action == MotionEventActions.Cancel)
                {
                    m_downActionIndex = null;
                    m_formsView.ShowPressed = false;
                    return true;
                }
            }

            return base.OnTouchEvent(e);
        }
    }
}
