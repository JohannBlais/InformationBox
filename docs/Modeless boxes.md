### InformationBox modeless behavior

Starting from version 0.6.7.0, it is possible to set the InformationBox as modeless. Doing so will allow you to access the parent window while displaying the InformationBox.
It is recommended when you want to show alerts for non critical errors in a time consuming process.

In case of a box asking the user to choose between multiple actions (yes/no, Ok/cancel, etc), it is not a good choice to use a modeless box. But if you really to do so, you will need to obtain the InformationBoxResult for the box. You just have to pass a delegate in the parameters of the Show method.

**Example 1**
This example is taken directly from the designer and demonstrates a simple callback.

_The callback method_
```
/// <summary>
/// Call when a asynchronous InformationBox is closed.
/// </summary>
/// <param name="result">The result.</param>
private static void boxClosed(InformationBoxResult result)
{
    InformationBox.Show(String.Format("I am the result of a modeless box : " + result));
}
```

_The parameter passed to the show method_
```
new AsyncResultCallBack(boxClosed)
```

**Example 2**
For those (like me) who love anonymous methods, here is how you can do with modeless boxes :

_An anonymous method_
```
InformationBox.Show("Message",
                    InformationBoxBehavior.Modeless,
                    (AsyncResultCallBack) delegate(InformationBoxResult result)
                        { MessageBox.Show(result.ToString()); });
```
You just have to cast the delegate into the AsyncResultCallBack type.
