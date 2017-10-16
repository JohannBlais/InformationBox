### InformationBox scopes

Starting from version 0.7.0.0, it is possible to define scopes for the InformationBox. It works the same way the TransactionScope does.
Scopes are useful when you use the same parameters for several InformationBoxes within the same scope.
For example, if in a method you have defined several boxes, all using the same background color, you may want to set that color once and for all at the beginning of the method.

**Examples**

_The concept_
```
private void Form1_Load(object sender, EventArgs e)
{
    // declare a new scope
    using (InformationBoxScope scope = new InformationBoxScope(new InformationBoxScopeParameters()))
    {
        // Set some parameters for the scope
        // All boxes in the scope will have the modern look.
        scope.Parameters.Style = InformationBoxStyle.Modern;

        InformationBox.Show("Message");
        InformationBox.Show("Message");
        InformationBox.Show("Message");
    }
    // The scope is now disposed, all InformationBox below this line will have the default parameters
    InformationBox.Show("Message");
}
```

There are times when you do not need/want to use the parameters defined in the current scope. You can deactivate the scopes for a particular InformationBox by passing InformationBoxInitialization.FromParametersOnly into the Show method.

_InformationBoxInitialization.FromParametersOnly_
```
private void Form1_Load(object sender, EventArgs e)
{
    // declare a new scope
    using (InformationBoxScope scope = new InformationBoxScope(new InformationBoxScopeParameters()))
    {
        // Set some parameters for the scope
        // All boxes in the scope will have the modern look.
        scope.Parameters.Style = InformationBoxStyle.Modern;

        InformationBox.Show("Message");
        // This box will NOT use the parameters in the scope
        InformationBox.Show("Message", InformationBoxInitialization.FromParametersOnly);
        InformationBox.Show("Message");
    }
    // The scope is now disposed, all InformationBox below this line will have the default parameters
    InformationBox.Show("Message");
}
```
