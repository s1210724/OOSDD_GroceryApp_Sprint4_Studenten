using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        public Client CurrentClient { get; set; }
        public ObservableCollection<GroceryList> GroceryLists { get; set; }

        private readonly GlobalViewModel _global;
        private readonly IGroceryListService _groceryListService;
        private readonly IAuthService _authService;

        public GroceryListViewModel(IGroceryListService groceryListService, GlobalViewModel global) 
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            GroceryLists = new(_groceryListService.GetAll());
            _global = global;
            CurrentClient = global.Client;
        }

        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, paramater);
        }

        [RelayCommand]
        private void ShowBoughtProducts()
        {
            if (CurrentClient != null && CurrentClient.Role == Role.Admin)
            {
                Shell.Current.GoToAsync(nameof(Views.BoughtProductsView));
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Toegang geweigerd", "Je hebt geen toegang tot deze pagina.", "OK");
            }
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            GroceryLists = new(_groceryListService.GetAll());
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }
    }
}
