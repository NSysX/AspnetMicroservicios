using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDistributedCache _rediCache;

        public BasketRepo(IDistributedCache rediCache)
        {
            this._rediCache = rediCache ?? throw new ArgumentNullException(nameof(rediCache));
        }

        public async Task<ShoppingCart?> ActualizarCanasta(ShoppingCart canasta)
        {
            await this._rediCache.SetStringAsync(canasta.UserName, JsonSerializer.Serialize<ShoppingCart>(canasta));
            return await this.ListarCanasta(canasta.UserName);
        }

        public async Task EliminarCanasta(string userName)
        {
             await this._rediCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart?> ListarCanasta(string userName)
        {
            var basket = await this._rediCache.GetStringAsync(userName);

            if (basket == null) return null;

            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }
    }
}
