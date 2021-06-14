using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ListPage : ContentPage
    {

        //public ObservableCollection<TodoItem> TodoItemList = new ObservableCollection<TodoItem>();
        WebAPIService webAPIService;
        private ObservableCollection<TodoItem> TodoItemList;


        public ListPage()
        {
            InitializeComponent();
            webAPIService = new WebAPIService();
            TodoItemList = new ObservableCollection<TodoItem>();
            GetAllTodoItems();


        }
        async void GetAllTodoItems()
        {
            TodoItemList = await webAPIService.GetListItems();


            //this.TodoItemList = new ObservableCollection<TodoItem>();

            //TodoItem todoItem = new TodoItem
            //{
            //    Key = "1",
            //    Name = "Prueba",
            //    IsComplete = false,
            //    Date = DateTime.Now
            //};
            //this.TodoItemList.Add(todoItem);

            //todoItem = new TodoItem
            //{
            //    Key = "2",
            //    Name = "Hacer la cama",
            //    IsComplete = false,
            //    Date = DateTime.Now
            //};
            //this.TodoItemList.Add(todoItem);

            //todoItem = new TodoItem
            //{
            //    Key = "2",
            //    Name = "Hacer la Loza",
            //    IsComplete = false,
            //    Date = DateTime.Now
            //};
            //this.TodoItemList.Add(todoItem);
            this.todoListView.ItemsSource = null;
            this.todoListView.ItemsSource = this.TodoItemList;
        }


        async void AddTask(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new ToDoItemPage()));

        }


        async void Delete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            TodoItem Item = (TodoItem)mi.CommandParameter;

            string result =  await webAPIService.Delete(Item.Key);
            if (result == "Se ha eliminado correctamente")
            {
                GetAllTodoItems();
            }
           await DisplayAlert(result, Item.Name + "", "OK");

        }

        async void Edit(object sender, EventArgs e)
        {
            Page p = new ToDoItemPage();
            await Navigation.PushModalAsync(p);

         }



       async void SelectTask(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                TodoItem item_selected = (TodoItem)e.SelectedItem;
                item_selected.IsComplete = !item_selected.IsComplete;
                string result = await webAPIService.Update(item_selected);
            }

          



            todoListView.ItemsSource = null;
            todoListView.ItemsSource = this.TodoItemList;
            todoListView.SelectedItem = null;
        }

        protected void Refresh(object sender, EventArgs e)
        {
            GetAllTodoItems();
            todoListView.EndRefresh();
        }
    }

}