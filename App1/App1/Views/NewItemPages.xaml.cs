using App1.Models;
using App1.ViewModels;
using Syncfusion.XForms.RichTextEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    public partial class NewItemPages : ContentPage
    {
        public Item Item { get; set; }
        public NewItemPages()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModels();

        }
       
        private void RTE_ImageInserted(object sender, Syncfusion.XForms.RichTextEditor.ImageInsertedEventArgs e)
        {
            
           


        }
    }
}