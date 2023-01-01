using GatewayMicroservices.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GatewayMicroservices.Srvices
{
    public class SettingService
    {
        private readonly IDbContextFactory<DbAppContext> _dbContextFactory;

        public SettingService(IDbContextFactory<DbAppContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<T> GetValueByKey<T>(string key)
        {
            using (var _context = _dbContextFactory.CreateDbContext())
            {
                if (await _context.Setting.AnyAsync(d => d.Key == key))
                {
                    var res = await _context.Setting.FirstAsync(d => d.Key == key);
                    return JsonSerializer.Deserialize<T>(res.Value);
                }
            }
            return default;
        }
    }
}
