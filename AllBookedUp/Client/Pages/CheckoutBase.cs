using AllBookedUp.Client.Services.CartService;
using AllBookedUp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllBookedUp.Client.Pages
{
    public class CheckoutBase:ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        protected IEnumerable<CartItem> cartItems { get; set; }

        protected int TotalQty { get; set; }

        protected string PaymentDescription { get; set; }

        protected decimal PaymentAmount { get; set; }

        //@inject AuthenticationStateProvider GetAuthenticationStateAsync

        [Inject]
        public AuthenticationStateProvider GetAuthenticationStateAsync { get; set; }

        [Inject]
        public ICartService cartService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authstate = await GetAuthenticationStateAsync.GetAuthenticationStateAsync();
            var user = authstate.User.Identity.Name;

            try
            {
                cartItems = await cartService.GetCartItems(user);
                if (cartItems != null)
                {
                    Guid orderGuid = Guid.NewGuid();

                    PaymentAmount = cartItems.Sum(p => p.Price);
                    TotalQty = cartItems.Count();
                    PaymentDescription = $"O_{user}_{orderGuid}";
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    await Js.InvokeVoidAsync("initPayPalButton");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
