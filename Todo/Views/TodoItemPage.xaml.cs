using System;
using Todo.Data;
using Todo.Models;
using Xamarin.Forms;

namespace Todo.Views
{
    public partial class TodoItemPage : ContentPage
    {
        public TodoItemPage()
        {
            InitializeComponent();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await TodoItemDatabase.SaveItemAsync(todoItem).ConfigureAwait(true);
            await Navigation.PopAsync().ConfigureAwait(true);
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await TodoItemDatabase.DeleteItemAsync(todoItem).ConfigureAwait(true);
            await Navigation.PopAsync().ConfigureAwait(true);
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync().ConfigureAwait(true);
        }
    }
}
