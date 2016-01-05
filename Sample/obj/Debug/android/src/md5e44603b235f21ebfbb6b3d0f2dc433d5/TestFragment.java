package md5e44603b235f21ebfbb6b3d0f2dc433d5;


public class TestFragment
	extends md55c82f15d7562512285a34f772e9d287c.ActionSheet
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Sample.TestFragment, Sample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TestFragment.class, __md_methods);
	}


	public TestFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TestFragment.class)
			mono.android.TypeManager.Activate ("Sample.TestFragment, Sample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
