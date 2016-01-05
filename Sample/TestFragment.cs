using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ActionSheetCSharp;

namespace Sample
{
    public class TestFragment : ActionSheet
    {

        protected override View OnCreateChildView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.layout1, null);
            Button bt = v.FindViewById<Button>(Resource.Id.button2);
            Button bt1 = v.FindViewById<Button>(Resource.Id.button3);
            Button bt2 = v.FindViewById<Button>(Resource.Id.button4);

            bt.Click += delegate
            {
                Dismiss();
            };
            bt1.Click += delegate
            {
                Dismiss();
            };
            bt2.Click += delegate
            {
                Dismiss();
            };
            return v;
        }


    }
}