using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using Android.Views.Animations;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Content.Res;

namespace ActionSheetCSharp
{
    public abstract class ActionSheet : Fragment, Android.Views.View.IOnClickListener
    {
        private const String ARG_CANCEL_BUTTON_TITLE = "cancel_button_title";
        private const String ARG_OTHER_BUTTON_TITLES = "other_button_titles";
        private const String ARG_CANCELABLE_ONTOUCHOUTSIDE = "cancelable_ontouchoutside";

        private const int TRANSLATE_DURATION = 200;
        private const int ALPHA_DURATION = 300;

        private bool mDismissed = true;
        private IActionSheetListener mListener;
        private View mView;
        private LinearLayout mPanel;
        private ViewGroup mGroup;
        private View mBg;

        public void Show(FragmentManager manager, String tag)
        {
            if (!mDismissed)
            {
                return;
            }
            mDismissed = false;
            FragmentTransaction ft = manager.BeginTransaction();
            ft.Add(this, tag);
            ft.AddToBackStack(null);
            ft.Commit();
        }

        public void Dismiss()
        {
            if (mDismissed)
            {
                return;
            }
            mDismissed = true;
            FragmentManager.PopBackStack();
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            ft.Remove(this);
            ft.Commit();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            InputMethodManager imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
            if (imm.IsActive)
            {
                View focusView = Activity.CurrentFocus;
                if (focusView != null)
                {
                    imm.HideSoftInputFromWindow(focusView.WindowToken, HideSoftInputFlags.None);
                }
            }
            mView = CreateView(inflater, container, savedInstanceState);
            mGroup = (ViewGroup)Activity.Window.DecorView;

            mGroup.AddView(mView);
            mBg.StartAnimation(CreateAlphaInAnimation());
            mPanel.StartAnimation(CreateTranslationInAnimation());
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private Animation CreateTranslationInAnimation()
        {
            TranslateAnimation an = new TranslateAnimation(Dimension.RelativeToSelf, 0,
                Dimension.RelativeToSelf, 0, Dimension.RelativeToSelf, 1, Dimension.RelativeToSelf, 0);
            an.Duration = TRANSLATE_DURATION;
            return an;
        }

        private Animation CreateAlphaInAnimation()
        {
            AlphaAnimation an = new AlphaAnimation(0, 1);
            an.FillAfter = true;
            an.Duration = ALPHA_DURATION;
            return an;
        }

        private Animation CreateTranslationOutAnimation()
        {
            TranslateAnimation an = new TranslateAnimation(Dimension.RelativeToSelf, 0,
                Dimension.RelativeToSelf, 0, Dimension.RelativeToSelf, 0, Dimension.RelativeToSelf, 1);
            an.Duration = TRANSLATE_DURATION;
            an.FillAfter = true;
            return an;
        }

        private Animation CreateAlphaOutAnimation()
        {
            AlphaAnimation an = new AlphaAnimation(1, 0);
            an.Duration = ALPHA_DURATION;
            an.FillAfter = true;
            return an;
        }

        private View CreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            FrameLayout parent = new FrameLayout(Activity);
            parent.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent);
            mBg = new View(Activity);
            mBg.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent);
            mBg.SetBackgroundColor(Color.Argb(160, 0, 0, 0));
            mBg.SetOnClickListener(this);

            mPanel = new LinearLayout(Activity);
            FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(
                FrameLayout.LayoutParams.MatchParent, FrameLayout.LayoutParams.WrapContent);
            param.Gravity = GravityFlags.Bottom;
            mPanel.LayoutParameters = param;
            mPanel.Orientation = Android.Widget.Orientation.Vertical;
            View child = OnCreateChildView(inflater, container, savedInstanceState);
            
            parent.AddView(mBg);
            parent.AddView(mPanel);
            mPanel.AddView(child);
            return parent;
        }

        protected abstract View OnCreateChildView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState);

        public void SetListener(IActionSheetListener listener)
        {
            this.mListener = listener;
        }

        public override void OnDestroyView()
        {
            mPanel.StartAnimation(CreateTranslationOutAnimation());
            mBg.StartAnimation(CreateAlphaOutAnimation());
            mView.PostDelayed(() =>
            {
                mGroup.RemoveView(mView);
            }, ALPHA_DURATION);
            if (mListener != null)
            {
                mListener.OnDismiss(this);
            }
            base.OnDestroyView();
        }

        public void OnClick(View v)
        {
            Dismiss();
        }

        public static Builder CreateBuilder(Context context,
            FragmentManager fragmentManager)
        {
            return new Builder(context, fragmentManager);
        }

        public class Builder
        {
            private Context mContext;
            private FragmentManager mFragmentManager;
            private String mTag;
            private IActionSheetListener mListener;

            public Builder(Context context, FragmentManager fragmentManager)
            {
                mContext = context;
                mFragmentManager = fragmentManager;
            }

            public Builder SetTag(String tag)
            {
                mTag = tag;
                return this;
            }

            public Builder SetListener(IActionSheetListener listener)
            {
                mListener = listener;
                return this;
            }

            public ActionSheet Show()
            {
                ActionSheet actionSheet = (ActionSheet)Fragment.Instantiate(mContext, "ActionSheet", null);
                actionSheet.Show(mFragmentManager, mTag);
                return actionSheet;
            }
        }
    }
}