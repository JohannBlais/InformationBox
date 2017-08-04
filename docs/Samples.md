## Samples

InformationBox is very easy to use, it is even simpler than the original MessageBox. With MessageBox, if you would like to specify only which buttons you want to display and which is the default one, you still have to provide a value for the icon because there is no constructor suiting your need. With InformationBox, it is not the case, you can simply provide what you need in the order you want.

For example :
{{
InformationBox.Show("My Text", InformationBoxButtons.YesNo, InformationBoxDefaultButton.Button2);
}}
This could also be written as :
{{
InformationBox.Show("My Text", InformationBoxDefaultButton.Button2, InformationBoxButtons.YesNo);
}}

The only thing necessary is that the text for the InformationBox be the first parameter. It allows the InformationBox to differenciate between the title and the text.