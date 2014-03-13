using System.Windows;
using System.Windows.Controls;

namespace MicroERP.Presentation.Selectors
{
    public class DynamicTemplateSelector : DataTemplateSelector
    {
        public static readonly DependencyProperty TemplatesProperty =
            DependencyProperty.RegisterAttached("Templates", typeof(TemplateCollection), typeof(DynamicTemplateSelector),
            new FrameworkPropertyMetadata(new TemplateCollection(), FrameworkPropertyMetadataOptions.Inherits));

        public static TemplateCollection GetTemplates(UIElement element)
        {
            return (TemplateCollection)element.GetValue(TemplatesProperty);
        }

        public static void SetTemplates(UIElement element, TemplateCollection collection)
        {
            element.SetValue(TemplatesProperty, collection);
        }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            //This should ensure that the item we are getting is in fact capable of holding our property
            //before we attempt to retrieve it.
            if (!(container is UIElement))
                return base.SelectTemplate(item, container);

            //First, we gather all the templates associated with the current control through our dependency property
            TemplateCollection templates = GetTemplates(container as UIElement);
            if (templates == null || templates.Count == 0)
                base.SelectTemplate(item, container);

            //Then we go through them checking if any of them match our criteria
            foreach (var template in templates)
                //In this case, we are checking whether the type of the item
                //is the same as the type supported by our DataTemplate
                if (template.Value.IsInstanceOfType(item))
                    //And if it is, then we return that DataTemplate
                    return template.DataTemplate;

            //If all else fails, then we go back to using the default DataTemplate
            return base.SelectTemplate(item, container);
        }
    }
}