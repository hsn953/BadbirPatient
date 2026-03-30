using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace bbPatientApp.Models
{
    public class Eq5dDataTemplateSelector : DataTemplateSelector
    {
		public DataTemplate StandardTemplate { get; set; }

		public DataTemplate ScaleTemplate { get; set; }

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			//return StandardTemplate;

            return ((Eq5dQuestion)item).QuestionType.Equals("Standard") ? StandardTemplate : ScaleTemplate;
		}
	}
}
