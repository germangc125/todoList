using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoList.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDoItemPage : ContentPage
    {
        WebAPIService webAPIService;

        public ToDoItemPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);  // Hide nav bar
        }



        async void SaveTask(object sender, EventArgs e)
        {
            webAPIService = new WebAPIService();
            TodoItem item = new TodoItem();
            item.Name = this.txt_tarea.Text;
            string result = await webAPIService.SaveTodoItem(item);
            await DisplayAlert("Exito", result, "OK");
            this.txt_tarea.Text = "";
        }
        
    }
}