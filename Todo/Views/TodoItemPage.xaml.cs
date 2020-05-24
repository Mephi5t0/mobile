using System;
using Todo.Data;
using Todo.Models;
using Xamarin.Essentials;
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

        private async void OnSendClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;

            try
            {
                var message = new EmailMessage
                {
                    Subject = todoItem.Name,
                    Body = todoItem.Notes,
                };

                await Email.ComposeAsync(message).ConfigureAwait(true);
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Warning", "Sorry, your device doesn`t support emails", "OK").ConfigureAwait(true);
            }
            catch (Exception)
            {
                await DisplayAlert("Warning", "Email wasn`t sent, please try later", "OK").ConfigureAwait(true);
            }
        }
    }
}
