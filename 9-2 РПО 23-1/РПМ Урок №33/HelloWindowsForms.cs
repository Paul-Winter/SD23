using System;
using System.Windows.Forms;

namespace HelloWindowsForms
{
	class FirstForm
	{
		public static void Main()
		{
			// создание формы
			Form firstForm = new Form();
			firstForm.Text = "This is the first Windows Forms Application";
			firstForm.ShowDialog();
		}
	}
}